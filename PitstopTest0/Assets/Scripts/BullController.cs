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

    bool isWalking;
    Vector2 minWalkPoint;
    Vector2 maxWalkPoint;
    float walkCounter;
    float waitCounter;
    int WalkDirection;
    bool hasWalkZone;

    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();

        waitCounter = waitTime;
        walkCounter = walkTime;

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
            StartCoroutine(Rush());            
        }
        
        if (rushTime <= 0)
        {
            StopCoroutine(Rush());            
            rushTime = 2;
            HeadingBackHome();
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
        target = walkZone;
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, rushSpeed * Time.deltaTime);
    }

    IEnumerator Rush()
    {
        Debug.Log(rushTime);
        yield return new WaitForSeconds(rushTime);
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, rushSpeed * Time.deltaTime);
        rushTime--;
    }
}