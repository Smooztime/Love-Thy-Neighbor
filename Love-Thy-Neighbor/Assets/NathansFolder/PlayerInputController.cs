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
        playerControls.Combat.ShootUse.performed += (var) => playerController.HandleShoot();
        playerControls.Combat.Aim.performed += (var) => playerController.HandleAim();
        playerControls.Look.Look.performed += (var) => playerController.HandleMouse(var.ReadValue<Vector2>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
