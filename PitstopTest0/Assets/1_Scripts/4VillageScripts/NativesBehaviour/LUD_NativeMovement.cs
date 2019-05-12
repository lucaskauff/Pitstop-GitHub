using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class LUD_NativeMovement : MonoBehaviour
    {
        public bool canMove = true;

        public List<Transform> pointsOfThePath = new List<Transform>();
        public int indexInPointOfThPath = 0;

        [SerializeField] private float walkingSpeedOfTheNative = 1f;
        public float speedMultiplier = 1f;
        [SerializeField] float speedDropOfSpeedMultiplier = 1f; 

        public float distanceMaxToTriggeredArrival = 0.2f;



        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (GetComponent<LUD_NativeHeartheSentence>().isCaptivated)
            {

                if (speedMultiplier > 0) speedMultiplier -= speedDropOfSpeedMultiplier * Time.deltaTime;
                if (speedMultiplier < 0) speedMultiplier = 0;

            }
            else
            {
                if (speedMultiplier < 1) speedMultiplier += speedDropOfSpeedMultiplier * Time.deltaTime;
                if (speedMultiplier > 1) speedMultiplier = 1;
            }

            Vector3 relativePositionToCurrentObjective = pointsOfThePath[indexInPointOfThPath].position - this.transform.position;
            relativePositionToCurrentObjective = relativePositionToCurrentObjective.normalized;
            relativePositionToCurrentObjective.z = 0f;

            if (canMove)
            {
                this.transform.position += relativePositionToCurrentObjective * walkingSpeedOfTheNative * Time.deltaTime * speedMultiplier;

                if ((pointsOfThePath[indexInPointOfThPath].position - this.transform.position).magnitude <= distanceMaxToTriggeredArrival)
                {
                    if (indexInPointOfThPath < pointsOfThePath.Count - 1)
                    {
                        indexInPointOfThPath++;
                    }
                    else
                    {
                        indexInPointOfThPath = 0;
                    }
                }
            }


        }
    }
}
