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

        //Animation variables
        [SerializeField] Animator myAnim = default;
        public Vector2 moveInput;
        public Vector2 lastMove;
        public bool isMoving = false;

        void Update()
        {
            Animations();

            if (GetComponent<LUD_NativeHeartheSentence>().isCaptivated)
            {

                if (speedMultiplier > 0) speedMultiplier -= speedDropOfSpeedMultiplier * Time.deltaTime;
                if (speedMultiplier < 0) speedMultiplier = 0;
                isMoving = false;

            }
            else
            {
                if (speedMultiplier < 1) speedMultiplier += speedDropOfSpeedMultiplier * Time.deltaTime;
                if (speedMultiplier > 1) speedMultiplier = 1;
                isMoving = true;

            }
            

            Vector3 relativePositionToCurrentObjective = pointsOfThePath[indexInPointOfThPath].position - this.transform.position;
            relativePositionToCurrentObjective = relativePositionToCurrentObjective.normalized;
            relativePositionToCurrentObjective.z = 0f;

            if (canMove)
            {
                this.transform.position += relativePositionToCurrentObjective * walkingSpeedOfTheNative * Time.deltaTime * speedMultiplier;
                moveInput = relativePositionToCurrentObjective;
                lastMove = moveInput;

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
            else
            {
                //isMoving = false;
            }
        }

        public void Animations()
        {
            myAnim.SetBool("IsMoving", isMoving);
            myAnim.SetFloat("LastMoveX", lastMove.x);
            myAnim.SetFloat("LastMoveY", lastMove.y);
            myAnim.SetFloat("MoveX", moveInput.x);
            myAnim.SetFloat("MoveY", moveInput.y);
        }
    }
}
