using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject levelManager;
    [SerializeField] GameObject gameStateManager;
    [SerializeField] GameObject mouseManager;

    private List<GameObject> persistentObjects;
    // Start is called before the first frame update
    void Start()
    {
        InitPersistentObjects();
    }

    void Update()
    {
        if (getCurrentScene().name == "MainMenu") {
            return;
        }
        CheckPauseMenu();
    }

    private void InitPersistentObjects()
    {
        persistentObjects = new List<GameObject>();
    }

    private Scene getCurrentScene()
    {
        return SceneManager.GetActiveScene();
    }


    private void CheckPauseMenu() {
    }
}
