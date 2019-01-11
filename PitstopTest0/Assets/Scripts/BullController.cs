using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullController : MonoBehaviour
{
    //Public
    public Collider2D walkZone;
    public bool isWalking;

    //SerializedField
    [SerializeField]
    float moveSpeed = 1;
    [SerializeField]
    float walkTime = 1;
    [SerializeField]
    float waitTime = 1;

    //Private
    private Rigidbody2D myRb;

    private Vector2 minWalkPoint;
    private Vector2 maxWalkPoint;

    private float walkCounter;
    private float waitCounter;

    private int WalkDirection;
    private bool hasWalkZone;

    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();

        waitCounter = waitTime;
        walkCounter = walkTime;

        ChooseDirection();

        if (walkZone != null)
        {
            minWalkPoint = walkZone.bounds.min;
            maxWalkPoint = walkZone.bounds.max;
            hasWalkZone = true;
        }
    }

    void Update()
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
                        myRb.velocity = new Vector2(0, moveSpeed);
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
                        myRb.velocity = new Vector2(moveSpeed, 0);
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
                        myRb.velocity = new Vector2(0, -moveSpeed);
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
                        myRb.velocity = new Vector2(-moveSpeed, 0);
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

    public void ChooseDirection()
    {
        WalkDirection = Random.Range(0, 4);
        isWalking = true;
        walkCounter = walkTime;
    }
}