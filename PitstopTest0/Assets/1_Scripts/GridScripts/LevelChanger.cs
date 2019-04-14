using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class LevelChanger : MonoBehaviour
    {
        //GameManager
        SceneLoader sceneLoader;

        //My Components
        [SerializeField]
        Animator myAnim = default;

        //Serializable
        [SerializeField]
        string nextSceneName = null;
        [SerializeField]
        PlayerControllerIso playerControllerIso = default;

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