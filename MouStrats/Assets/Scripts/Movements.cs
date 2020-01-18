using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Movements : MonoBehaviour
{
    const float sensibiliteX = 50.0f;
    const float sensibiliteY = 25.0f;

    Transform mainCam { get; set; }
    Rigidbody rigidJoueur;

    public float acceleration = 8;
    public float walkSpeed = 10;
    float runSpeed;

    float delta;

    Vector3 inputDirecion;
    Vector3 currentVelocity;
    Vector3 targetVelocity;
    Vector3 dirHoriz;
    Vector3 dirVert;
    Vector3 ancienneDir;
    Vector3 anciennePos;
    Vector3 directionAvant;

    //Vector3 ancienneRot { get; set; }

    [SerializeField]
    const float clipY = 90;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = FindObjectsOfType<Camera>().FirstOrDefault(c => c.name.StartsWith("Main")).transform;
        rigidJoueur = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        delta = Time.deltaTime;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        dirHoriz = mainCam.transform.right * horizontal;
        dirVert = mainCam.transform.forward * vertical;


        inputDirecion = new Vector3(horizontal, 0 , vertical);


        targetVelocity = inputDirecion * walkSpeed * delta;


        float deplacementVertical = Input.GetAxis("Mouse Y") * sensibiliteY * Time.deltaTime; //* Screen.dpi;
        float deplacementHorizontal = Input.GetAxis("Mouse X") * sensibiliteX * Time.deltaTime; // / Screen.dpi;

        Debug.Log(deplacementVertical);
        Debug.Log(deplacementHorizontal);

        mainCam.transform.RotateAround(transform.position, Vector3.up, deplacementHorizontal);


        Vector3 targetDir = mainCam.forward * vertical;
        targetDir += mainCam.right * horizontal;
        targetDir.Normalize();

        targetDir.y = 0;
        if (targetDir == Vector3.zero)
            targetDir = transform.forward;

        Quaternion tr = Quaternion.LookRotation(targetDir);
        Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tr, delta * 10);



        // mainCam.transform.RotateAround(joueur.transform.position, -mainCam.transform.right, deplacementVertical);

        //if (!((mainCam.transform.rotation.eulerAngles.x > 20 && deplacementVertical < 0) || (mainCam.transform.rotation.eulerAngles.x < -20 && deplacementVertical > 0)))
        //    mainCam.transform.RotateAround(joueur.transform.position, -mainCam.transform.right, deplacementVertical);

    }

    void FixedUpdate()
    {
        currentVelocity = rigidJoueur.velocity + 0.01f * Vector3.up;

        Vector3 accelerationVelocity = Vector3.Lerp(currentVelocity, targetVelocity, acceleration * delta);

        directionAvant = rigidJoueur.transform.position - mainCam.position;

        //rigidJoueur.AddForce(accelerationVelocity.magnitude*1000 * (dirHoriz + dirVert));

        rigidJoueur.velocity = accelerationVelocity.magnitude * 10f * (dirHoriz + dirVert);
             
    }
}
