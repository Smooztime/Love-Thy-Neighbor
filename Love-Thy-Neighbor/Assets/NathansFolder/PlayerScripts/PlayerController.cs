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
    bool canUpgrade = false;

    public bool upgradeMenuOpen = false;

    [SerializeField] FreezeManager freezeManager;
    float timeFreezeBuffer = 1;
    // Start is called before the first frame update
    void Start()
    {
        if (playerData == null || playerTransform == null || playerRigidBody == null || cameraTransform == null || pistol == null || upgradeMenuScript == null || freezeManager == null)
        {
            Debug.LogError("PlayerController: One or more required components are not assigned in the Inspector.");
            enabled = false;
            return;
        }

        movementSpeed = playerData.moveSpeed;
        lookSensitivity = playerData.lookSens;
        clampRotation = playerData.clampRotation;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void FixedUpdate()
    {
        if (playerTransform == null || playerRigidBody == null)
        {
            return;
        }

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
        if (playerTransform == null)
        {
            return;
        }
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
        if (playerTransform == null)
        {
            return;
        }
        upDownRotation += -input.y * lookSensitivity;
        leftRightRotation += input.x;
        upDownRotation = Mathf.Clamp(upDownRotation, -60 + temporaryKickRotation, 60 + temporaryKickRotation);
        playerTransform.eulerAngles = new Vector3(playerTransform.rotation.x, leftRightRotation * lookSensitivity, playerTransform.rotation.z);
    }
    public void HandleShoot()
    {
        if (pistol == null)
        {
            return;
        }
        timeFreezeBuffer = TimeUnfrozenFromShooting;
        pistol.mouseIsDown = true;
        pistol.Shoot();
        pistol.ShotGunFire();
    }
    public void StopShoot()
    {
        if (pistol == null)
        {
            return;
        }
        pistol.mouseIsDown = false;
    }
    public void Reload()
    {
        if (pistol == null)
        {
            return;
        }
        timeFreezeBuffer = TimeUnfrozenFromShooting;
        pistol.Reload();
    }
    public void GunKickUp(float angleToMoveUp)
    {
        temporaryKickRotation = angleToMoveUp;
    }
    public void HandleUpgradeMenu()
    {
        if (upgradeMenuScript == null)
        {
            return;
        }

        upgradeMenuOpen = !upgradeMenuOpen;
        if (upgradeMenuOpen && canUpgrade)
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
        if (cameraTransform == null || freezeManager == null)
        {
            return;
        }
        cameraTransform.localRotation = Quaternion.Euler(new Vector3(Mathf.Clamp(upDownRotation - temporaryKickRotation, -60, 60), 0, 0));
        if (temporaryKickRotation > 0)
        {
            temporaryKickRotation = pistol != null ? pistol.Kick : 0;
        }
        else
        {
            temporaryKickRotation = 0;
        }

        if (timeFreezeBuffer <= 0)
        {
            freezeManager.TimeIsFrozen = true;
        }
        else
        {
            freezeManager.TimeIsFrozen = false;
            timeFreezeBuffer -= Time.deltaTime;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "UpgradeCoffin")
        {
            canUpgrade = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "UpgradeCoffin")
        {
            canUpgrade = false;
        }
    }
}
