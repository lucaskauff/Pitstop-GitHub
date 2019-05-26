using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class PrologueManager : MonoBehaviour
    {
        SceneLoader sceneLoader;

        [SerializeField] float prologueTimer = 35;
        [SerializeField] string nextScene = null;

        private void Awake()
        {
            StartCoroutine(PrologueIsPlaying());
        }

        private void Start()
        {
            sceneLoader = GameManager.Instance.sceneLoader;
        }

        IEnumerator PrologueIsPlaying()
        {
            yield return new WaitForSeconds(prologueTimer);
            sceneLoader.LoadNewScene(nextScene);
        }
    }
}