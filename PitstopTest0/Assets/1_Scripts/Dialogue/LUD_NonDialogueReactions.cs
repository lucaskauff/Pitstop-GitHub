using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class LUD_NonDialogueReactions : MonoBehaviour
    {
        

        private bool wasGoToEastTriggered = false;
        private bool isArrivedToEast = false;

        public List<Transform> pointOfThePathToEast = new List<Transform>();
        private int indexInPointOfThPath = 0;
        [SerializeField] private float WalkingSpeedOfTheNative = 5f;
        [SerializeField] float distanceMaxToTriggeredArrival = 1f;



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
            Debug.Log("Native is Offended");
        }


        public void Repeat()
        {
            Debug.Log("Repeat last sentence");
        }

        public void ShowTheWay()
        {
            if (!wasGoToEastTriggered)
            {
                Debug.Log("Go to East");
                StartCoroutine("DelayBeforeStartingMovingToEast");

                //go to other point with pathfinding
                //wait for the player to enter the zone 
                //activate the ShowWhereIsEllya

                
            }

            
        }

        IEnumerator DelayBeforeStartingMovingToEast()
        {
            yield return new WaitForSeconds(FindObjectOfType<LUD_NativeHeartheSentence>().delayBeforeExclamationDisapperance + FindObjectOfType<LUD_DialogueAppearance>().delay);   //attend que la boîte de dialogue disparaisse
            wasGoToEastTriggered = true;
        }

        public void ShowWhereIsEllya()
        {
            Debug.Log("Says where is Ellya");
        }

    }
}
