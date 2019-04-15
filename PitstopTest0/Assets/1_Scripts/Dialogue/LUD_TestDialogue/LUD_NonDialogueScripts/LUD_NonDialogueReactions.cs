﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class LUD_NonDialogueReactions : MonoBehaviour
    {
        
        [Header("Show the Way")]
        private bool wasGoToEastTriggered = false;
        public bool isArrivedToEast = false;

        public List<Transform> pointOfThePathToEast = new List<Transform>();
        private int indexInPointOfThPath = 0;
        [SerializeField] private float WalkingSpeedOfTheNative = 5f;
        [SerializeField] float distanceMaxToTriggeredArrival = 1f;

        [Header("Native Offended")]
        public bool isOffended = false;



        private void Update()
        {
            if (!isArrivedToEast && wasGoToEastTriggered)
            {
                Vector3 relativePositionToObjective = pointOfThePathToEast[indexInPointOfThPath].position - this.transform.position;
                relativePositionToObjective = relativePositionToObjective.normalized;
                relativePositionToObjective.z = 0f;

                this.transform.position += relativePositionToObjective * WalkingSpeedOfTheNative * Time.deltaTime;

                if ((pointOfThePathToEast[indexInPointOfThPath].position - this.transform.position).magnitude <= distanceMaxToTriggeredArrival)
                {
                    if (indexInPointOfThPath<pointOfThePathToEast.Count-1)
                    {
                        indexInPointOfThPath++;
                    }
                    else
                    {
                        isArrivedToEast = true;
                    }
                }

            }
        }

        


        public void GoDown()
        {
            Debug.Log("GoDown");
        }

        public void NativeOffended()
        {
            StartCoroutine("LaunchTheDesactivcationaAndReturnToNormal");
        }

        IEnumerator LaunchTheDesactivcationaAndReturnToNormal()
        {
            isOffended = true;
            yield return new WaitForSeconds(GetComponent<LUD_NativeHeartheSentence>().delayBeforeExclamationDisapperance + GetComponent<LUD_DialogueAppearance>().delay / 2);
            GetComponentInChildren<LUD_DetectionTriggeredByAttention>().UncaptivationOfTheNative();

            yield return new WaitForSeconds(FindObjectOfType<LUD_NonDialogueManager>().timePassedLockedWhenOffended);
            isOffended = false;
            
            if (GetComponentInChildren<LUD_DetectionTriggeredByAttention>().isThePlayerNear)
            {
                GetComponentInChildren<LUD_DetectionTriggeredByAttention>().CaptivationOfTheNative();
            }
        }


        public void Repeat()
        {
            //Debug.Log("Repeat last sentence"); 
        }


        public void ShowTheWay()
        {
            if (!wasGoToEastTriggered)
            {
                StartCoroutine("DelayBeforeStartingMovingToEast");
            }
            else if (isArrivedToEast)
            {
                StartCoroutine("DelayBeforeStayingWhereIsEllya");
            }
        }

        IEnumerator DelayBeforeStartingMovingToEast()
        {
            string stringReaction = "equal;-1;FALSE;no_reaction;you;me;me;,";
            NativeReaction reaction = new NativeReaction(stringReaction);
            GetComponent<LUD_DialogueAppearance>().ReactionAppearance(reaction.answerWord1, reaction.answerWord2, reaction.answerWord3, true);

            yield return new WaitForSeconds(GetComponent<LUD_NativeHeartheSentence>().delayBeforeExclamationDisapperance + GetComponent<LUD_DialogueAppearance>().delay/2);   //attend que la boîte de dialogue disparaisse
            wasGoToEastTriggered = true;
        }


        public void ShowWhereIsEllya()
        {
            StartCoroutine("DelayBeforeStayingWhereIsEllya");
        }

        IEnumerator DelayBeforeStayingWhereIsEllya()
        {
            string stringReactionEllya = "equal;-1;FALSE;no_reaction;you;east;east;,";
            NativeReaction reactionEllya = new NativeReaction(stringReactionEllya);
            yield return new WaitForSeconds(0.5f);
            GetComponent<LUD_DialogueAppearance>().ReactionAppearance(reactionEllya.answerWord1, reactionEllya.answerWord2, reactionEllya.answerWord3, false);
        }

    }
}