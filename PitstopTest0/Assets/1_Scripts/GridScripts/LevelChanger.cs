using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class LevelChanger : MonoBehaviour
    {
        SceneLoader sceneLoader;

        [SerializeField]
        Animator myAnim;

        [SerializeField]
        string nextSceneName;
        [SerializeField]
        PlayerControllerIso playerControllerIso;

        private void Start()
        {
            sceneLoader = GameManager.Instance.sceneLoader;
        }

        public void LevelChanging()
        {
            playerControllerIso.canMove = false;
            myAnim.SetTrigger("FadeOut");
        }

        public void ChangeLevelRightNow()
        {
            sceneLoader.LoadNewScene(nextSceneName);
        }
    }
}