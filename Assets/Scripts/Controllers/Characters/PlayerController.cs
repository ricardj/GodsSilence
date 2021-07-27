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
    AvatarAnimationController characterAnimationController;

    public float playerSpeed = 0.4f;
    public bool playerControlsEnabled;


    [Header("Partner instructions")]
    public KeyCode callPartner;
    PartnerModeManager partnerManager;

    Camera mainCamera;
    Rigidbody2D rb;
    Vector2 mousePos;

    [Header("Weapons")]
    public WeaponInventorySO weaponInventory;
    public WeaponContainer currentWeapon;

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
            characterAnimationController.SetState<Walking>();
        else
            characterAnimationController.SetState<Idle>();
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
        WeaponContainer previousWeapon = currentWeapon;
        int previousWeaponIndex = weaponIndex;
        SelectWeaponThroughMouse();
        SelectWeaponThroughKeyboard();

        //ACTUAL WEAPON CHANGE
        if (weaponIndex != previousWeaponIndex)
        {
            currentWeapon.weaponConfiguration = weaponInventory.GetWeaponByIndex(weaponIndex);
            characterAnimationController.SetWeaponType(currentWeapon.weaponConfiguration);

            partnerManager.SetPartnerWeaponByIndex(weaponIndex);

            if (previousWeapon != null)
                previousWeapon.SetWeaponShooting(false);
        }


    }

    private void SelectWeaponThroughKeyboard()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            weaponIndex = 0;
        if (Input.GetKeyDown(KeyCode.Alpha2))
            weaponIndex = 1;
        if (Input.GetKeyDown(KeyCode.Alpha3))
            weaponIndex = 2;
        if (Input.GetKeyDown(KeyCode.Alpha4))
            weaponIndex = 3;
        if (Input.GetKeyDown(KeyCode.Alpha5))
            weaponIndex = 4;
        if (Input.GetKeyDown(KeyCode.Alpha6))
            weaponIndex = 5;
        if (Input.GetKeyDown(KeyCode.Alpha7))
            weaponIndex = 6;
    }

    private void SelectWeaponThroughMouse()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            if (weaponIndex < weaponInventory.weaponConfigurations.Count)
            {
                weaponIndex += 1;
            }

        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            if (weaponIndex > 0)
            {
                weaponIndex -= 1;
            }

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
        if (collision.gameObject.tag == "Bullet")
        {
            GameManager.instance.ReloadScene();
        }
    }
}
