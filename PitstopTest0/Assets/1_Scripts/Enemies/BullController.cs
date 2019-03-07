using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullController : MonoBehaviour
{
    //Public
    public GameObject walkZone;
    public GameObject target;

    //SerializedField
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

    //Private
    private Rigidbody2D myRb;

    bool isIdling = true;
    bool isWalking;
    Vector2 minWalkPoint;
    Vector2 maxWalkPoint;
    float walkCounter;
    float waitCounter;
    int WalkDirection;
    bool hasWalkZone;
    Vector3 originPos;

    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();

        waitCounter = waitTime;
        walkCounter = walkTime;
        originPos = transform.position;

        ChooseDirection();

        if (walkZone != null)
        {
            minWalkPoint = walkZone.GetComponent<BoxCollider2D>().bounds.min;
            maxWalkPoint = walkZone.GetComponent<BoxCollider2D>().bounds.max;
            hasWalkZone = true;
        }
    }

    void Update()
    {
        if (target != null)
        {
            isIdling = false;
        }

        if (isIdling)
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
            StartCoroutine(Rush());

            if (rushTime <= 0)
            {
                StopCoroutine(Rush());
                
                HeadingBackHome();
            }
        }
    }

    public void ChooseDirection()
    {
        WalkDirection = Random.Range(0, 4);
        isWalking = true;
        walkCounter = walkTime;
    }

    public void HeadingBackHome()
    {        
        transform.position = Vector2.MoveTowards(transform.position, originPos, walkSpeed * Time.deltaTime);

        if (transform.position == originPos)
        {
            target = null;
            rushTime = 2;
            isIdling = true;
        }
    }

    IEnumerator Rush()
    {
        while (rushTime > 0)
        {            
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, rushSpeed * Time.deltaTime);
            yield return new WaitForSeconds(1);
            rushTime--;
        }        
    }
}