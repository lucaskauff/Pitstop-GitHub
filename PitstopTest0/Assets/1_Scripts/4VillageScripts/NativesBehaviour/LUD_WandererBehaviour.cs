using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class LUD_WandererBehaviour : MonoBehaviour
    {
        [Header("Timers")]
        public float timeBetweenTwoWalkInTheForest = 15f;
        public float timeSpentInTheForest = 10f;

        [Header("Points of the Behaviour")]
        public Transform pointInTheForest;
        public List<Transform> pointsOfReturningPathToButcher;
        int indexInPointsOfReturningPath;
        

        [Header("Booleans")]
        [SerializeField] bool goInTheForestActivated = false;
        [SerializeField] bool isHuntingInTheForest = false;
        [SerializeField] bool isReturningFromHunt = false;
        

        [Header("Specific Values")]
        [SerializeField] float distanceToBeConsideredArrived = 1f;
        [SerializeField] float walkingSpeedOfTheNative = 3f;


        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(TimeBeforeGoingInTheForest());
        }

        // Update is called once per frame
        void Update()
        {

            if (goInTheForestActivated && GetComponent<LUD_NativeMovement>().indexInPointOfThPath == 1)
            {
                GetComponent<LUD_NativeMovement>().canMove = false;
                isHuntingInTheForest = true;
                GetComponent<BoxCollider2D>().enabled = false;

                StartCoroutine(TimeBeforeReturningFromHunt());
                goInTheForestActivated = false;
            }

            if(isHuntingInTheForest)
            {
                Vector3 relativePositionToCurrentObjective = pointInTheForest.position - this.transform.position;

                if (relativePositionToCurrentObjective.magnitude >= distanceToBeConsideredArrived)
                {
                    relativePositionToCurrentObjective = relativePositionToCurrentObjective.normalized;
                    relativePositionToCurrentObjective.z = 0f;

                    this.transform.position += relativePositionToCurrentObjective * walkingSpeedOfTheNative * Time.deltaTime * GetComponent<LUD_NativeMovement>().speedMultiplier;
                }
                
            }

            if (isReturningFromHunt)
            {
                Vector3 relativePositionToCurrentObjective = pointsOfReturningPathToButcher[indexInPointsOfReturningPath].position - this.transform.position;
                relativePositionToCurrentObjective = relativePositionToCurrentObjective.normalized;
                relativePositionToCurrentObjective.z = 0f;

                this.transform.position += relativePositionToCurrentObjective * walkingSpeedOfTheNative * Time.deltaTime * GetComponent<LUD_NativeMovement>().speedMultiplier;

                if ((pointsOfReturningPathToButcher[indexInPointsOfReturningPath].position - this.transform.position).magnitude <= GetComponent<LUD_NativeMovement>().distanceMaxToTriggeredArrival)
                {
                    if (indexInPointsOfReturningPath < pointsOfReturningPathToButcher.Count - 1)
                    {
                        indexInPointsOfReturningPath++;
                    }
                    else
                    {
                        indexInPointsOfReturningPath = 0;
                        isReturningFromHunt = false;
                        GetComponent<BoxCollider2D>().enabled = true;

                        GetComponent<LUD_NativeMovement>().canMove = true;
                        GetComponent<LUD_NativeMovement>().indexInPointOfThPath = 4;

                        StartCoroutine(TimeBeforeGoingInTheForest());
                        
                    }
                }
            }
        }

        IEnumerator TimeBeforeGoingInTheForest()
        {

            yield return new WaitForSeconds(timeBetweenTwoWalkInTheForest);

            goInTheForestActivated = true;
        }

        IEnumerator TimeBeforeReturningFromHunt()
        {
            yield return new WaitForSeconds(timeSpentInTheForest);
            isHuntingInTheForest = false;
            isReturningFromHunt = true;
        }
    }
}
