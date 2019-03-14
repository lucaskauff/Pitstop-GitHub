using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SceneLoader))]
[RequireComponent(typeof(InputManager))]
//[RequireComponent(typeof(UIManager))]
public class GameManager : Singleton<GameManager>
{
    protected GameManager() { }

    public SceneLoader sceneLoader;
    public InputManager inputManager;
    //public UIManager uIManager;

    void Awake()
    {
        sceneLoader = GetComponent<SceneLoader>();
        inputManager = GetComponent<InputManager>();
        //uIManager = GetComponent<UIManager>();
    }
}