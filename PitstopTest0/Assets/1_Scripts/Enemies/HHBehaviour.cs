using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class HHBehaviour : MonoBehaviour
    {
        //SerializedField
        [SerializeField]
        float viewRangeRad;
        [SerializeField]
        int damageDealing = 1;
        [SerializeField]
        float walkSpeed = 1;
        [SerializeField]
        float rushSpeed = 3;
        [SerializeField]
        float walkTime = 1;
        [SerializeField]
        float waitTime = 1;
        [SerializeField]
        float rushTime = 2;
        [SerializeField]
        float rushRatio = 1.25f;
        [SerializeField]
        float stunTime = 3;

        //Public
        public GameObject walkZone;
        public GameObject viewRange;
        public GameObject target;
        public GameObject player;

        //Private
        Rigidbody2D myRb;

        public bool isArrived;
        bool isWalking;
        Vector2 minWalkPoint;
        Vector2 maxWalkPoint;
        float walkCounter;
        float waitCounter;
        int WalkDirection;
        bool hasWalkZone;

        bool col = false;
        Vector3 targetPos;
        bool followTargetCheck = false;
        float originalRushTime;
        bool stunCheck = false;
        float originalStunTime;

        void Start()
        {
            myRb = GetComponent<Rigidbody2D>();

            viewRange.transform.localScale *= viewRangeRad;
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
            if (isArrived)
            {
                myRb.velocity = Vector2.zero;

                if (!stunCheck)
                {
                    followTargetCheck = false;
                    target = null;
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
                    viewRange.SetActive(true);

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
                        //targetPos = (target.transform.position - this.transform.position) * rushRatio;
                        targetPos = target.transform.position;
                        followTargetCheck = true;
                    }

                    transform.position = Vector2.MoveTowards(transform.position, targetPos, rushSpeed * Time.deltaTime);

                    if (col || this.transform.position == targetPos || rushTime <= 0)
                    {
                        isArrived = true;
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

            //condition not perfect !!
            if (other.gameObject.name == "Zayn" || other.gameObject.name == "Native")
            {
                player.GetComponent<PlayerHealthManager>().HurtPlayer(damageDealing);
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

        IEnumerator RushTimeDecount()
        {
            while (rushTime > 0)
            {
                yield return new WaitForSeconds(1);
                rushTime--;
            }
        }
    }
}