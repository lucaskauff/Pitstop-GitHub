using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Pitstop
{
    public class UpdateNextSceneName : MonoBehaviour
    {
        SceneLoader sceneLoader;

        [SerializeField] TextMeshProUGUI nextSceneName = default;

        private void Start()
        {
            sceneLoader = GameManager.Instance.sceneLoader;

            nextSceneName.text = sceneLoader.nextScene;
        }
    }
}