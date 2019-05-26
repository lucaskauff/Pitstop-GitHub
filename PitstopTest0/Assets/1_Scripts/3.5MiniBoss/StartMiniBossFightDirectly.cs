using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class StartMiniBossFightDirectly : MonoBehaviour
    {
        [SerializeField] EerickBehaviourTestLUD eerickBehaviour = default;
        [SerializeField] LUD_NativeBehaviourOnMiniBossScene native = default;
        [SerializeField] GameObject triggerDialogueZone = default;
        [SerializeField] LUD_MusicTransition musicTransition = default;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player" && !eerickBehaviour.fightCanStart)
            {
                eerickBehaviour.playerReloadedScene = true;
                StartCoroutine(native.WaitBeforeFlyingAway());
                triggerDialogueZone.SetActive(false);
                StartCoroutine(musicTransition.MusicTransition());
                Destroy(gameObject);
            }
        }
    }
}