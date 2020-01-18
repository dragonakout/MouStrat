using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// Gère tous les évènements dépassant le cadre d'une scène. Persistent d'une scène à l'autre
public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject gameStateManager;
    [SerializeField] GameObject mouseManager;
    [SerializeField] GameObject inGameResourceManager;
    private static GameManager instance = null;

    private List<GameObject> persistentManagers;
    public static GameManager getInstance()
    {
        // YOLO, ne devrait pas être null
        return instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        DontDestroyOnLoad(this);
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
        persistentManagers = new List<GameObject>();
        persistentManagers.Add(gameStateManager);
        persistentManagers.Add(mouseManager);
        persistentManagers.Add(inGameResourceManager);

        for (int i = 0; i < persistentManagers.Count; i++) {
            DontDestroyOnLoad(persistentManagers[i]);
        }
    }

    private Scene getCurrentScene()
    {
        return SceneManager.GetActiveScene();
    }


    private void CheckPauseMenu() {
        // TODO : PAUSE MENU
    }

}
