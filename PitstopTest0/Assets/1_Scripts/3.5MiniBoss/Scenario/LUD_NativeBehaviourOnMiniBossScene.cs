using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class LUD_NativeBehaviourOnMiniBossScene : MonoBehaviour
    {
        [Header("Fly Away")]
        public Transform whereTheNativeFlyAway;
        [SerializeField] float maxDistanceTotriggerArrival = 0.1f;
        [SerializeField] float speedOfEscape = 4f;
        [SerializeField] float timeBeforeEscaping = 0.5f;
        [SerializeField] bool isFlyingAway = false;

        [Header("Return to center")]
        public bool isReturningFromForest = false;
        public Transform whereTheNativeComeBack;
        [SerializeField] float speedOfReturning = 4f;
        public DialogueTrigger dialogueToTriggerInTheCenter;


        // Update is called once per frame
        void Update()
        {
            if (isFlyingAway)
            {
                Vector3 relativePositionOfArrival = whereTheNativeFlyAway.position - transform.position;


                relativePositionOfArrival.z = 0f;
                Vector3 normalizedRelativePositionOfArrival = relativePositionOfArrival.normalized;


                this.transform.position += normalizedRelativePositionOfArrival * Time.deltaTime * speedOfEscape;

                //Debug.Log("whereTheNativeFlyAway.position - this.transform.position).magnitude" + (whereTheNativeFlyAway.position - this.transform.position).magnitude);
                if (relativePositionOfArrival.magnitude <= maxDistanceTotriggerArrival)
                {
                    isFlyingAway = false;
                }
            }

            else if (isReturningFromForest)
            {
                Vector3 relativePositionOfArrival = whereTheNativeComeBack.position - transform.position;


                relativePositionOfArrival.z = 0f;
                Vector3 normalizedRelativePositionOfArrival = relativePositionOfArrival.normalized;


                this.transform.position += normalizedRelativePositionOfArrival * Time.deltaTime * speedOfReturning;

                if (relativePositionOfArrival.magnitude <= maxDistanceTotriggerArrival)
                {
                    isReturningFromForest = false;
                    GetComponent<Collider2D>().enabled = true;
                    dialogueToTriggerInTheCenter.TriggerDialogueDirectly();

                }
            }

        }

        IEnumerator WaitBeforeFlyingAway()
        {
            yield return new WaitForSeconds(timeBeforeEscaping);
            isFlyingAway = true;
            GetComponent<Collider2D>().enabled = false;
        }
    }
}
