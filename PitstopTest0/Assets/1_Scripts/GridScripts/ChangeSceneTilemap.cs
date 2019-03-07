using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class ChangeSceneTilemap : MonoBehaviour
    {
        SceneLoader sceneLoader;

        [SerializeField]
        string nextSceneName;

        private void Start()
        {
            sceneLoader = GameManager.Instance.sceneLoader;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.name == "Zayn")
            {
                sceneLoader.LoadNewScene(nextSceneName);
            }
        }
    }
}