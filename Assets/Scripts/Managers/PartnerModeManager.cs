using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.PostProcessing;

public class PartnerModeManager : MonoBehaviour
{


    [Header("Command tags")]
    public string groundTag = "Ground";
    public string enemyTag = "Enemy";


    [Header("Settings")]
    public GameObject partner;

    public float cameraSpeed = 4f;
    public float signalCommandMousePressingTime = 0.3f;

    List<Command> commands;

    Camera mainCamera;
    PartnerController partnerController;

    [Header("Partner mode controls")]
    public KeyCode partnerModeKey;
    public KeyCode restartPartnerCommandsKey;
    public KeyCode signalPartnerKey;



    [Header("Marking prefabs")]
    public GameObject locationMarkingPrefab;
    public GameObject attackMarkingPrefab;
    public GameObject locationWaitMarkingPrefab;
    public GameObject attackWaitMarkingPrefab;
    List<GameObject> markingList;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        mainCamera = Camera.main;
        partnerController = partner.GetComponent<PartnerController>();
        commands = new List<Command>();
        markingList = new List<GameObject>();
    }


    bool partnerMode;
    bool isWaitCommand;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(partnerModeKey))
        {
            GameManager.instance.guiManager.ToogleParnterMode();
            partnerMode = !partnerMode;
            if (partnerMode)
            {
                Time.timeScale = 0;
                ActivatePartnerModeCamera();

            }
            else
            {
                Time.timeScale = 1;
                DeactivatePartnerModeCamera();
                StartCoroutine(StartCommandSequence());
            }

        }

        if (partnerMode)
        {

            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                bool isWaitCommand = Input.GetMouseButtonDown(1);

               
                Vector3 ray = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hitInfo = Physics2D.Raycast(ray, Vector3.zero);
                if (hitInfo.collider)
                {
                    Debug.Log("So far so good");
                    Command newCommand = null;


                    if (hitInfo.transform.gameObject.tag == groundTag)
                    {
                        newCommand = new CommandGoTo(partnerController, hitInfo.point);
                        newCommand.commandType = isWaitCommand ? CommandType.GOTO_WAIT : CommandType.GOTO;
                    }

                    if (hitInfo.transform.gameObject.tag == enemyTag)
                    {
                        newCommand = new CommandGoTo(partnerController, hitInfo.point);
                        newCommand.commandType = isWaitCommand ? CommandType.ATTACK_WAIT : CommandType.ATTACK;
                    }


                    //We add the command to the list
                    if (newCommand != null)
                    {
                        markingList.Add(ShowClickEffect(newCommand.commandType, new Vector3(hitInfo.point.x, hitInfo.point.y, -3f)));
                        commands.Add(newCommand);
                    }
                }
            }

            if (Input.GetKeyDown(restartPartnerCommandsKey))
                ResetCommands();

            //Now we controll the camera
            CameraControl();
        }
    }

    #region  camera_control
    float horizontalMove;
    float verticalMove;

    ICinemachineCamera cachedVirtualCamera;
    Vector3 totalMovement = Vector3.zero;
    public void CameraControl()
    {

        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");
        totalMovement.x = horizontalMove;
        totalMovement.y = verticalMove;
        if (cachedVirtualCamera != null)
            cachedVirtualCamera.VirtualCameraGameObject.transform.position += (totalMovement.normalized) * Time.unscaledDeltaTime * cameraSpeed;
        else
            Debug.LogError("No cached virtual camera");
    }

    public void ActivatePartnerModeCamera()
    {
        GameManager.instance.cameraManager.SetPartnerCamera();
        cachedVirtualCamera = GameManager.instance.cameraManager.GetPartnerCamera();
        
    }

    public void DeactivatePartnerModeCamera()
    {
        GameManager.instance.cameraManager.SetPlayerCamera();
    }
    #endregion


    GameObject ShowClickEffect(CommandType commandType, Vector3 newPosition)
    {
        GameObject markingElement = null;
        switch (commandType)
        {
            case CommandType.GOTO:
                markingElement = Instantiate(locationMarkingPrefab, newPosition, Quaternion.identity);
                break;
            case CommandType.ATTACK:
                markingElement = Instantiate(attackMarkingPrefab, newPosition, Quaternion.identity);
                break;
            case CommandType.ATTACK_WAIT:
                markingElement = Instantiate(attackWaitMarkingPrefab, newPosition, Quaternion.identity);
                break;
            case CommandType.GOTO_WAIT:
                markingElement = Instantiate(locationWaitMarkingPrefab, newPosition, Quaternion.identity);
                break;
        }

        return markingElement;

    }



    public IEnumerator StartCommandSequence()
    {
        for (int i = 0; i < commands.Count; i++)
        {
            Command currentCommand = commands[i];

            if (CheckIsWaitCommand(currentCommand))
            {
                //We stop the partner controller
                partnerController.followTarget = false;

                yield return new WaitUntil(() => Input.GetKeyDown(signalPartnerKey) || Input.GetKeyDown(restartPartnerCommandsKey));
                if (!Input.GetKeyDown(restartPartnerCommandsKey))
                {
                    StartCoroutine(currentCommand.ActivateCommand());
                    StartCoroutine(currentCommand.UpdateCommandCallback());
                    yield return new WaitUntil(() => currentCommand.isCommandCompleted);
                    Destroy(markingList[i]);
                }
            }else
            {
                StartCoroutine(currentCommand.ActivateCommand());
                StartCoroutine(currentCommand.UpdateCommandCallback());
                yield return new WaitUntil(() => currentCommand.isCommandCompleted);
                Destroy(markingList[i]);
            } 
        }

        yield return null;
        ResetCommands();
    }

    

    public void ResetCommands()
    {
        for (int i = 0; i < markingList.Count; i++)
        {
            if (markingList[i] != null)
                Destroy(markingList[i]);
        }
        markingList.Clear();
        commands.Clear();
    }
    bool CheckIsWaitCommand(Command command)
    {
        return command.commandType == CommandType.ATTACK_WAIT || command.commandType == CommandType.GOTO_WAIT;
    }


    #region direct_commands
    public void SetPartnerWeapon(WeaponConfigurationSO weaponConfiguration)
    {
        partnerController.SetWeapon(weaponConfiguration);
    }
    public void SetPartnerWeaponByIndex(int index)
    {
        partnerController.SetWeaponByIndex(index);
    }
    public void FollowTarget(GameObject followTarget)
    {
        ResetCommands();
        partnerController.target = followTarget;
        partnerController.followTarget = true;
    }
    #endregion


}
