using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class HoleScript : MonoBehaviour
    {
        SceneLoader sceneLoader;

        private void Start()
        {
            sceneLoader = GameManager.Instance.sceneLoader;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.name == "Zayn")
            {
                sceneLoader.ReloadScene();
            }
        }
    }
}