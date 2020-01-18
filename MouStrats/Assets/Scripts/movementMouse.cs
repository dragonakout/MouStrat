using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class movementMouse : MonoBehaviour
{

    Rigidbody rigidPlayer;

    public GameObject mainCam;
    Vector3 direction { get; set; }

    bool isRunning;
    bool isGrounded = false;

    const float maxStamina = 6;
    const float minStamina = 0;
    const float staminaBuffer = 3;
    const float sensibiliteX = 50.0f;
    const float sensibiliteY = 25.0f;
    const float gravityMult = 5;

    [Space(10)]
    public float walkSpeed = 5;
    public float runSpeed = 25;
    public float jumpForce = 25;
    public float stamina = 6;
    float horizontal;
    float vertical;
    float moveAmount;
    float camVertical;
    float camHorizontal;
    float timer;

    GameObject staminaUI { get; set; }

    [SerializeField]
    Vector3 posCam;
    [SerializeField]
    float camFollow;

    void Start()
    {
        
        rigidPlayer = GetComponent<Rigidbody>();
        Physics.gravity *= gravityMult;
        staminaUI = GameObject.Find("Stamina");
    }

   
    void Update()
    {
        GetInput();
        CameraManager();
        GetLookRotation();
        RunManager();
        StaminaManager();
        JumpManager();

        if(!isGrounded)
            OrienterEnAir();
    }

    void GetInput()
    {
        float delta = Time.deltaTime;
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        camVertical = Input.GetAxis("Mouse Y") * sensibiliteY * delta; 
        camHorizontal = Input.GetAxis("Mouse X") * sensibiliteX * delta; 

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
        staminaUI.GetComponent<Slider>().value = stamina;
    }

    void JumpManager(){
        if (Input.GetButton("Jump") && isGrounded)
        {
            rigidPlayer.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }


    }

    void CameraManager()
    {
        mainCam.transform.RotateAround(transform.position, Vector3.up, camHorizontal);      
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
        float delta = Time.deltaTime;
        if (isRunning)
            direction = transform.forward * runSpeed;
        else
            direction = transform.forward * walkSpeed;

        if (moveAmount > 0)
        {
            rigidPlayer.velocity = Vector3.Lerp(rigidPlayer.velocity, direction, 0.2f);
        }

        

        mainCam.transform.position = Vector3.Lerp(mainCam.transform.position, transform.position - mainCam.transform.forward * 10, camFollow * delta);
    }

    private void OrienterEnAir()
    {
        transform.rotation = Quaternion.LookRotation(transform.forward + rigidPlayer.velocity * 10);
        //transform.eulerAngles.Set(rigidPlayer.velocity.y * 100, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
        {
            isGrounded = true;

            //transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isGrounded = false;
    }

  //  private void OnDrawGizmosSelected()
  //  {
  //      Gizmos.color = Color.blue;
  //      Gizmos.DrawLine(this.transform.position, this.transform.position + rigidPlayer.velocity * 10);
  //  }


}
