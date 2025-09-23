using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations;

[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    CharacterController controller;
    [SerializeField] private float defaultSpeed = 1.0f;
    [SerializeField] private float lookSpeed = 0.2f;

    Vector3 movement;
    float curRot;

    WorldData worldData;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        worldData = FindAnyObjectByType<WorldData>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        Look();

        
    }

    private void Jump()
    {
        if (controller.isGrounded)
        {
           

            if (ControllerScan.Instance.jumped == true)
            {
                Debug.Log("a");
                movement.y = worldData.JumpHeight;
            } else
            {
                movement.y = -0.1f;
            }

        }
        else
        {
            Debug.Log(movement.y);
            movement.y += worldData.Gravity * Time.deltaTime;
        }
    }

    private void Move()
    {
        Vector2 input = ControllerScan.Instance.moveInput;

        Vector3 inputDir = new Vector3(input.x, 0, input.y);
        Vector3 worldDir = transform.TransformDirection(inputDir);
        worldDir.Normalize();

        Jump();

        movement.x = worldDir.x * defaultSpeed;
        movement.z = worldDir.z * defaultSpeed;
        //Debug.Log(movement.x + " " + movement.z);
        controller.Move(movement * Time.deltaTime);
    }


    private void Look()
    {
        Vector2 input = ControllerScan.Instance.camInput;

        transform.Rotate(0, input.x*lookSpeed, 0);

        curRot += input.y * -lookSpeed;
        curRot = Mathf.Clamp(curRot, -70, 70);
       Camera.main.transform.localRotation = Quaternion.Euler(curRot,0,0);
    }
}
