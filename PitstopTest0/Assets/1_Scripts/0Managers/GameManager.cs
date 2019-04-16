using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    [RequireComponent(typeof(SceneLoader))]
    [RequireComponent(typeof(InputManager))]
    public class GameManager : Singleton<GameManager>
    {
        protected GameManager() { }

        public SceneLoader sceneLoader;
        public InputManager inputManager;

        public bool languageSetToEnglish = true;

        void Awake()
        {
            sceneLoader = GetComponent<SceneLoader>();
            inputManager = GetComponent<InputManager>();
        }
    }
}