using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class EpilogueManager : MonoBehaviour
    {
        SceneLoader sceneLoader;

        [SerializeField] float epilogueTimer = 35;
        [SerializeField] string nextScene = null;

        private void Awake()
        {
            StartCoroutine(EpilogueIsPlaying());
        }

        private void Start()
        {
            sceneLoader = GameManager.Instance.sceneLoader;
        }

        IEnumerator EpilogueIsPlaying()
        {
            yield return new WaitForSeconds(epilogueTimer);
            sceneLoader.LoadNewScene(nextScene);
        }
    }
}