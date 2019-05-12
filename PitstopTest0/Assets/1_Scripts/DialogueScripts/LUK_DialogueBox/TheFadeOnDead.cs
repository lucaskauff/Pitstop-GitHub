using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class TheFadeOnDead : MonoBehaviour
    {
        SceneLoader sceneLoader;

        private void Start()
        {
            sceneLoader = GameManager.Instance.sceneLoader;
        }

        public void ReloadSceneOnDead()
        {
            sceneLoader.ReloadScene();
        }
    }
}