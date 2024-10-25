using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        PlayerControls playerControls = new PlayerControls();
        playerControls.Movement.WASD.performed += (var) => playerController.HandleMovement(var.ReadValue<Vector2>());
        playerControls.Movement.WASD.canceled += (var) => playerController.CancelMove();
        playerControls.Combat.ShootUse.performed += (var) => playerController.HandleShoot();
        playerControls.Combat.ShootUse.canceled += (var) => playerController.StopShoot();
        playerControls.Combat.Reload.performed += (var) => playerController.Reload();
        playerControls.Combat.Aim.performed += (var) => playerController.HandleAim();
        playerControls.Look.Look.performed += (var) => playerController.HandleMouse(var.ReadValue<Vector2>());
        playerControls.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
