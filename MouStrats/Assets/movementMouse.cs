using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class movementMouse : MonoBehaviour
{
    
    public Rigidbody rigidPlayer;


    public NavMeshAgent agent;

    public MouseAI AI;

    public GameObject mainCam;
    Vector3 direction { get; set; }

    [HideInInspector]
    public bool isRunning;
    public bool isGrounded = false;

    const float maxStamina = 6;
    const float minStamina = 0;
    const float staminaBuffer = 3;

    [Space(10)]
    public float walkSpeed = 5;
    public float runSpeed = 25;
    public float jumpForce = 250;
    public float stamina = 6;
    float horizontal;
    float vertical;
    float moveAmount;
    float camVertical;
    float camHorizontal;
    float timer;
    float delta;


    void Start()
    {
       
    }   
    void Update()
    {
        delta = Time.deltaTime;
        GetInput();
        GetLookRotation();
        if (isGrounded)
            RunManager();
        else
            isRunning = false;
        StaminaManager();
        JumpManager();


        if (!isGrounded)
            OrienterEnAir();
    }
    void GetInput()
    {
        float delta = Time.deltaTime;
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

       

        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Math.Abs(vertical));
    }
    void RunManager()
    {
        if (Input.GetButton("Run"))
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
    }
    void StaminaManager()
    {
        float delta = Time.deltaTime;


        if (isRunning && stamina > minStamina)
        {
            stamina -= delta;
            timer = 0;
        }
        else if (timer < staminaBuffer)
        {
            isRunning = false;
            timer += delta;
        }
        else if (stamina < maxStamina)
        {
            stamina += delta / 2;
        }
        else
            timer = 0;
    }
    void JumpManager()
    {
        if (Input.GetButton("Jump") && isGrounded)
        {
            // rigidPlayer.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            rigidPlayer.velocity = new Vector3(0, jumpForce, 0);
        }
    }

       
    void GetLookRotation()
    {
        float delta = Time.deltaTime;
        Vector3 targetDir = mainCam.transform.forward * vertical;
        targetDir += mainCam.transform.right * horizontal;
        targetDir.Normalize();

        targetDir.y = 0;
        if (targetDir == Vector3.zero)
            targetDir = transform.forward;

        Quaternion tr = Quaternion.LookRotation(targetDir);
        Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tr, delta * 10);

        transform.rotation = targetRotation;

        if(moveAmount < 0)
        {
            rigidPlayer.drag = 0;        
        }
        else
        {
            rigidPlayer.drag = 4;
        }
    }
    private void FixedUpdate()
    { 
        if (isRunning)
            direction = transform.forward * runSpeed;
        else
            direction = transform.forward * walkSpeed;

        if (moveAmount > 0)
        {
            rigidPlayer.velocity = Vector3.Lerp(rigidPlayer.velocity, direction, 0.2f);
        }

       

    }
    private void OrienterEnAir()
    {
        //transform.rotation = Quaternion.LookRotation(rigidPlayer.velocity);
        //transform.eulerAngles.Set(rigidPlayer.velocity.y * 100, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
        {
            isGrounded = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        isGrounded = false;
    }

}
