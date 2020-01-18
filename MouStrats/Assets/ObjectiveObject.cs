using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectiveObject : MonoBehaviour
{
    private Collider collider;
    private bool isCollected;

    private void OnTriggerEnter(Collider other)
    {
        // TODO CHANGE NULL FOR PLAYER'S COLLIDER
        if (other is null)
        {
            isCollected = true;
        }   
    }
}
