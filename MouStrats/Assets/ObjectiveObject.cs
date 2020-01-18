using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectiveObject : MonoBehaviour
{
    private Collider collider;
    private bool isCollected;

    private void OnTriggerEnter(Collider other)
    {
        if (other is LevelManager)
        {
            isCollected = true;
        }   
    }
}
