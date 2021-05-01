using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    float horizontalMove;
    float verticalMove;
    Vector2 totalMove;
    
    [SerializeField]
    CharacterAnimationController characterAnimationController;

    public float playerSpeed = 0.4f;
    public bool playerControlsEnabled;


    [Header("Partner instructions")]
    public KeyCode callPartner;
    PartnerModeManager partnerManager;

    Camera mainCamera;
    Rigidbody2D rb;
    Vector2 mousePos;

    [Header("Weapons")]
    public WeaponInventory weaponInventory;
    Weapon currentWeapon;
    WeaponType currentWeaponType;
    int weaponIndex = 0;

    private void Start()
    {
        partnerManager = GameManager.instance.partnerModeManager;
        totalMove = Vector2.zero;
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
    }


    private void Update()
    {
        if (!playerControlsEnabled)
            return;
        MovePlayer();
        ControlPartner();
        ChooseWeapon();
        ShootWeapon();
    }

    public void MovePlayer()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");
       
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        if (horizontalMove + verticalMove != 0)
            characterAnimationController.Walk();
        else
            characterAnimationController.Idle();
    }

    public void ControlPartner()
    {
        if (Input.GetKeyDown(callPartner))
        {
            partnerManager.FollowTarget(gameObject);
        }
    }

    public void ChooseWeapon()
    {
        Weapon previousWeapon = currentWeapon;
        WeaponType previousWeaponType = currentWeaponType;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            if(weaponIndex < (int)WeaponType.TOTAL_STATES)
            {
                weaponIndex += 1;
                currentWeaponType = (WeaponType)weaponIndex;
            }
            
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            if(weaponIndex > 0)
            {
                weaponIndex -= 1;
                currentWeaponType = (WeaponType)weaponIndex;
            }
           
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
            currentWeaponType = WeaponType.UNARMED;
        if (Input.GetKeyDown(KeyCode.Alpha2))
            currentWeaponType = WeaponType.MACHINE_GUN;
        if (Input.GetKeyDown(KeyCode.Alpha3))
            currentWeaponType = WeaponType.MINIGUN;
        if (Input.GetKeyDown(KeyCode.Alpha4))
            currentWeaponType = WeaponType.PISTOL;
        if (Input.GetKeyDown(KeyCode.Alpha5))
            currentWeaponType = WeaponType.DUAL_PISTOL;
        if (Input.GetKeyDown(KeyCode.Alpha6))
            currentWeaponType = WeaponType.LASER1;
        if (Input.GetKeyDown(KeyCode.Alpha7))
            currentWeaponType = WeaponType.LASER2;

        //ACTUAL WEAPON CHANGE
        if (currentWeaponType != previousWeaponType)
        {
            characterAnimationController.SetWeaponType(currentWeaponType);
            currentWeapon = weaponInventory.GetWeapon(currentWeaponType);

            partnerManager.SetPartnerWeapon(currentWeaponType);

            if (previousWeapon != null)
                previousWeapon.SetWeaponShooting(false);                
        }
           
            
    }

    public void ShootWeapon()
    {
        if (currentWeapon != null)
        {
            if (Input.GetMouseButton(0))
                currentWeapon.SetWeaponShooting(true);
            else
                currentWeapon.SetWeaponShooting(false);
        }
            
    }

    private void FixedUpdate()
    {
        //characterController.Move(new Vector3(horizontalMove , 0, verticalMove) * playerSpeed * Time.fixedDeltaTime);
        totalMove.x = horizontalMove;
        totalMove.y = verticalMove;
        rb.MovePosition(totalMove.normalized * playerSpeed * Time.fixedDeltaTime + rb.position);
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 270;
        rb.rotation = angle;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            GameManager.instance.ReloadScene();
        }
    }
}
