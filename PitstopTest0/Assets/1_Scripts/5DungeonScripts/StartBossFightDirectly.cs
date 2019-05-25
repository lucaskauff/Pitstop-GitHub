using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class StartBossFightDirectly : MonoBehaviour
    {
        [SerializeField] TheBeastBehaviour theBeastBehaviour = default;
        [SerializeField] GameObject dialogueTriggerOnEntering = default;
        //[SerializeField] Animator energeticBarrierAnimator = default;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                theBeastBehaviour.startBossFightDirectly = true;
                Destroy(dialogueTriggerOnEntering);
                Destroy(gameObject);
            }
        }
    }
}