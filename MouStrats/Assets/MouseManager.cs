using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{


    public Camera cam;
    public Transform target;

    public movementMouse[] allMouse;

    public movementMouse currentMouse;
    int whichMouse;

    [SerializeField]
    float camFollow;
    float camVertical;
    float camHorizontal;
    const float sensibiliteX = 50.0f;
    const float sensibiliteY = 25.0f;


    void Start()
    {     
        for (int i = 0; i < allMouse.Length; i++)
        {
            if(target == null)
            {
                target = allMouse[i].transform;
                allMouse[i].agent.enabled = false;
                allMouse[i].AI.enabled = false;
                allMouse[i].rigidPlayer.isKinematic = false;
                currentMouse = allMouse[i];
                whichMouse = 0;
            }
            else
            {
             
                allMouse[i].agent.enabled = true;
                allMouse[i].AI.enabled = true;
                allMouse[i].rigidPlayer.isKinematic = true;
                allMouse[i].enabled = false;
            }
        }
    }

    void Update()
    {
        CameraManager();

        if (Input.GetButtonDown("SwitchMouse"))
        {
            SwitchMouse();
        }
    }

    void SwitchMouse()
    {
        currentMouse.agent.enabled = true;
        currentMouse.AI.enabled = true;
        currentMouse.rigidPlayer.isKinematic = true;
        currentMouse.enabled = false;
       
        whichMouse++;
        if(whichMouse > allMouse.Length - 1)
        {
            whichMouse = 0;
            
        }
        currentMouse = allMouse[whichMouse];
        target = currentMouse.transform;
        currentMouse.agent.enabled = false;
        currentMouse.AI.enabled = false;
        currentMouse.enabled = true;
        currentMouse.rigidPlayer.isKinematic = false;

    }
           
    private void FixedUpdate()
    {
        cam.transform.position = Vector3.Lerp(cam.transform.position, target.position - cam.transform.forward * 15 + cam.transform.up * 4, camFollow * Time.fixedDeltaTime);
    }

    void CameraManager()
    {
        camVertical = Input.GetAxis("Mouse Y") * sensibiliteY * Time.deltaTime;
        camHorizontal = Input.GetAxis("Mouse X") * sensibiliteX * Time.deltaTime;

        cam.transform.RotateAround(target.position, Vector3.up, camHorizontal);
    }
}
