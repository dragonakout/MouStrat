using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScriptObjetRamassable : MonoBehaviour
{
    const float deltaRotation = 5.0f;
    const float frequence = Mathf.PI;
    const float amplitude = 0.05f;
    const float threshold = 5;

    GameObject joueur;

    float temps;
    // Start is called before the first frame update
    void Start()
    {
        joueur = GameObject.FindGameObjectsWithTag("Player").FirstOrDefault(m => m.GetComponentInChildren<movementMouse>().isActiveAndEnabled);
    }

    // Update is called once per frame
    void Update()
    {
        temps += Time.deltaTime;
        transform.Rotate(Vector3.up, deltaRotation * Time.deltaTime);
        transform.position += amplitude * Mathf.Sin(temps*frequence) * Vector3.up;
        if (Vector3.Distance(this.transform.position, joueur.transform.position) < threshold)
        ApprocherObjetDeJoueur();


    }

    private void ApprocherObjetDeJoueur()
    {
        throw new NotImplementedException();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            FaireAction();
            //Jouer son
            Destroy(gameObject);
        }
 
    }

    private void FaireAction()
    {
        Debug.Log("L'objet " + this.name + " a été ramassé");
    }
}
