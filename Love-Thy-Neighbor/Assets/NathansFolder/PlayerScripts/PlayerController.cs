using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] Rigidbody playerRigidBody;
    [SerializeField] Transform cameraTransform;
    [SerializeField] PlayerData playerData;
    [SerializeField] Pistol pistol;
    [SerializeField] Upgrade upgradeMenuScript;
    [SerializeField] float TimeUnfrozenFromShooting;
    [SerializeField] float TimeUnfrozenFromMoving;
    float movementSpeed;
    float lookSensitivity;
    float clampRotation;
    float rightLeftInput = 0;
    float forwardBackInput = 0;
    float upDownRotation = 0;
    float leftRightRotation = 0;
    float temporaryKickRotation = 0;

    public bool upgradeMenuOpen = false;

    [SerializeField] FreezeManager freezeManager;
    float timeFreezeBuffer = 1;
    // Start is called before the first frame update
    void Start()
    {
        movementSpeed = playerData.moveSpeed;
        lookSensitivity = playerData.lookSens;
        clampRotation = playerData.clampRotation;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void FixedUpdate()
    {
        Vector3 xAxisVelocity = new Vector3(0, 0, 0);
        Vector3 yAxisVelocity = new Vector3(0, 0, 0);
        if (rightLeftInput > 0)
        {
            xAxisVelocity = playerTransform.right;
            if (timeFreezeBuffer < TimeUnfrozenFromMoving)
            {
                timeFreezeBuffer = TimeUnfrozenFromMoving;
            }
        }
        else if (rightLeftInput < 0)
        {
            xAxisVelocity = -playerTransform.right;
            if (timeFreezeBuffer < TimeUnfrozenFromMoving)
            {
                timeFreezeBuffer = TimeUnfrozenFromMoving;
            }
        }
        if (forwardBackInput > 0)
        {
            yAxisVelocity = playerTransform.forward;
            if (timeFreezeBuffer < TimeUnfrozenFromMoving)
            {
                timeFreezeBuffer = TimeUnfrozenFromMoving;
            }
        }
        else if (forwardBackInput < 0)
        {
            yAxisVelocity = -playerTransform.forward;
            if (timeFreezeBuffer < TimeUnfrozenFromMoving)
            {
                timeFreezeBuffer = TimeUnfrozenFromMoving;
            }
        }
        playerRigidBody.velocity = (xAxisVelocity + yAxisVelocity).normalized * movementSpeed;
        
    }
    public void HandleMovement(Vector2 input)
    {
        
        Debug.Log(playerTransform.forward);
       rightLeftInput = input.x;
        forwardBackInput = input.y;
    }
    public void CancelMove()
    {
        rightLeftInput = 0;
        forwardBackInput = 0;
    }
    public void HandleMouse(Vector2 input)
    {
        upDownRotation += -input.y * lookSensitivity;
        leftRightRotation += input.x;
        upDownRotation = Mathf.Clamp(upDownRotation, -60 + temporaryKickRotation, 60 + temporaryKickRotation);
        playerTransform.eulerAngles = new Vector3(playerTransform.rotation.x,leftRightRotation * lookSensitivity, playerTransform.rotation.z);
    }
    public void HandleShoot()
    {
       // Debug.Log("Shooting");
        timeFreezeBuffer = TimeUnfrozenFromShooting;
        pistol.mouseIsDown = true;
        pistol.Shoot();
        pistol.ShotGunFire();
    }
    public void StopShoot()
    {
        pistol.mouseIsDown = false;
    }
    public void Reload()
    {
        timeFreezeBuffer = TimeUnfrozenFromShooting;
        pistol.Reload();
    }
    public void GunKickUp(float angleToMoveUp)
    {
        temporaryKickRotation = angleToMoveUp;
    }
    public void HandleUpgradeMenu()
    {
        upgradeMenuOpen = !upgradeMenuOpen;
        if(upgradeMenuOpen)
        {
            upgradeMenuScript.OpenUpgradeWindow();
        }
        else
        {
            upgradeMenuScript.CloseUpgradeWindow();
        }
    }
    public void HandleAim()
    {

    }

    // Update is called once per frame
    void Update()
    {
        cameraTransform.localRotation = Quaternion.Euler(new Vector3(Mathf.Clamp(upDownRotation - temporaryKickRotation, -60,60), 0, 0));
        if (temporaryKickRotation > 0)
        {
            temporaryKickRotation = pistol.Kick;
        }
        else
        {
            temporaryKickRotation = 0;
        }
        
        if(timeFreezeBuffer <= 0)
        {
            freezeManager.TimeIsFrozen = true;
        }
        else
        {
            freezeManager.TimeIsFrozen = false;
            timeFreezeBuffer -= Time.deltaTime;
        }
    }
}