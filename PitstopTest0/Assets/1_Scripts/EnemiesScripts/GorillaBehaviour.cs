using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class GorillaBehaviour : MonoBehaviour
    {
        [Header("My components")]
        [SerializeField] Rigidbody2D myRb = default;
        //waiting for anims
        //[SerializeField] Animator myAnim = default;

        //Serializable
        [SerializeField] float viewRangeRad = default;
        [SerializeField] int damageDealing = 1;
        [SerializeField] float walkSpeed = 1;
        [SerializeField] float walkTime = 1;
        [SerializeField] float waitTime = 1;
        [SerializeField] float stunTime = 3;
        [SerializeField] float repulsionDuration = 1;

        //Public
        public bool canMove = true;
        public GameObject walkZone;
        public Transform viewRange;
        public GameObject target;
        public GameObject player;
        public bool isFleeing = false;
        public float rushSpeed = 3;
        public float rushTime = 2;
        public bool isArrived;

        //Private
        //Walk variables
        bool isWalking;
        Vector2 minWalkPoint;
        Vector2 maxWalkPoint;
        float walkCounter;
        float waitCounter;
        int WalkDirection;
        bool hasWalkZone;

        //
        bool col = false;
        public Vector3 targetPos;
        bool followTargetCheck = false;
        float originalRushTime;

        //
        bool stunCheck = false;
        float originalStunTime;

        //
        public bool isBeingRepulsed = false;

        void Start()
        {
            viewRange.localScale *= viewRangeRad;
            originalRushTime = rushTime;
            originalStunTime = stunTime;

            waitCounter = waitTime;
            walkCounter = walkTime;

            ChooseDirection();

            if (walkZone != null)
            {
                minWalkPoint = walkZone.GetComponent<CompositeCollider2D>().bounds.min;
                maxWalkPoint = walkZone.GetComponent<CompositeCollider2D>().bounds.max;
                hasWalkZone = true;
            }
        }

        void Update()
        {
            if (!canMove)
            {
                return;
            }
            else if (isBeingRepulsed)
            {
                StartCoroutine(WaitDuringRepulsion());
            }
            else if (isArrived)
            {
                myRb.velocity = Vector2.zero;

                if (!stunCheck)
                {
                    followTargetCheck = false;
                    if (!isFleeing)
                    {
                        target = null;
                    }
                    col = false;

                    StopAllCoroutines();
                    rushTime = originalRushTime;

                    StartCoroutine(Stunned());
                    stunCheck = true;
                }

                if (stunTime <= 0)
                {
                    stunCheck = false;
                    StopCoroutine(Stunned());
                    stunTime = originalStunTime;

                    isArrived = false;
                }
            }
            else
            {
                if (target == null)
                {
                    if (isWalking)
                    {
                        walkCounter -= Time.deltaTime;

                        switch (WalkDirection)
                        {
                            case 0:
                                if (hasWalkZone && transform.position.y > maxWalkPoint.y)
                                {
                                    isWalking = false;
                                    waitCounter = waitTime;
                                }
                                else
                                {
                                    myRb.velocity = new Vector2(0, walkSpeed);
                                }
                                break;

                            case 1:
                                if (hasWalkZone && transform.position.x > maxWalkPoint.x)
                                {
                                    isWalking = false;
                                    waitCounter = waitTime;
                                }
                                else
                                {
                                    myRb.velocity = new Vector2(walkSpeed, 0);
                                }
                                break;

                            case 2:
                                if (hasWalkZone && transform.position.y < minWalkPoint.y)
                                {
                                    isWalking = false;
                                    waitCounter = waitTime;
                                }
                                else
                                {
                                    myRb.velocity = new Vector2(0, -walkSpeed);
                                }
                                break;

                            case 3:
                                if (hasWalkZone && transform.position.x < minWalkPoint.x)
                                {
                                    isWalking = false;
                                    waitCounter = waitTime;
                                }
                                else
                                {
                                    myRb.velocity = new Vector2(-walkSpeed, 0);
                                }
                                break;
                        }

                        if (walkCounter < 0)
                        {
                            isWalking = false;
                            waitCounter = waitTime;
                        }
                    }
                    else
                    {
                        waitCounter -= Time.deltaTime;

                        myRb.velocity = Vector2.zero;

                        if (waitCounter < 0)
                        {
                            ChooseDirection();
                        }
                    }
                }
                else
                {
                    if (!followTargetCheck)
                    {
                        StartCoroutine(RushTimeDecount());
                        targetPos = target.transform.position;
                        followTargetCheck = true;
                    }

                    transform.position = Vector2.MoveTowards(transform.position, targetPos, rushSpeed * Time.deltaTime);

                    if (col || this.transform.position == targetPos || rushTime <= 0)
                    {
                        isArrived = true;
                        //isFleeing = false;
                    }
                }
            }
        }

        public void ChooseDirection()
        {
            WalkDirection = Random.Range(0, 4);
            isWalking = true;
            walkCounter = walkTime;
        }

        public void OnCollisionEnter2D(Collision2D other)
        {
            col = true;

            //check if there will be native vs enemies situations
            if ((other.gameObject.tag == "Player" || other.gameObject.name == "Native") && !stunCheck && !isFleeing)
            {
                player.GetComponent<PlayerHealthManager>().HurtPlayer(damageDealing);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "ObjectApple")
            {
                StopCoroutine(WaitDuringRepulsion());
                isBeingRepulsed = true;
            }
        }

        IEnumerator RushTimeDecount()
        {
            while (rushTime > 0)
            {
                yield return new WaitForSeconds(1);
                rushTime--;
            }
        }

        IEnumerator Stunned()
        {
            while (stunTime > 0)
            {
                yield return new WaitForSeconds(1);
                stunTime--;
            }
        }

        IEnumerator WaitDuringRepulsion()
        {
            yield return new WaitForSeconds(repulsionDuration);
            isBeingRepulsed = false;
        }
    }
}