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
    float movementSpeed;
    float lookSensitivity;
    float clampRotation;
    float rightLeftInput = 0;
    float forwardBackInput = 0;
    float upDownRotation = 0;
    float leftRightRotation = 0;
    // Start is called before the first frame update
    void Start()
    {
        movementSpeed = playerData.moveSpeed;
        lookSensitivity = playerData.lookSens;
        clampRotation = playerData.clampRotation;
    }
    private void FixedUpdate()
    {
        Vector3 xAxisVelocity = new Vector3(0, 0, 0);
        Vector3 yAxisVelocity = new Vector3(0, 0, 0);
        if (rightLeftInput > 0)
        {
            xAxisVelocity = playerTransform.right;
        }
        else if (rightLeftInput < 0)
        {
            xAxisVelocity = -playerTransform.right;
        }
        if (forwardBackInput > 0)
        {
            yAxisVelocity = playerTransform.forward;
        }
        else if (forwardBackInput < 0)
        {
            yAxisVelocity = -playerTransform.forward;
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
        upDownRotation = Mathf.Clamp(upDownRotation, -60, 60);
        playerTransform.eulerAngles = new Vector3(playerTransform.rotation.x,leftRightRotation * lookSensitivity, playerTransform.rotation.z);
        cameraTransform.localRotation = Quaternion.Euler(new Vector3(upDownRotation,0,0));
    }
    public void HandleShoot()
    {
        Debug.Log("Shooting");
        pistol.Shoot();
    }
    public void HandleAim()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
