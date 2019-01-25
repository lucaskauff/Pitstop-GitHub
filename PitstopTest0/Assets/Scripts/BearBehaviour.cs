using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearBehaviour : MonoBehaviour
{
    [SerializeField]
    float lineOfSightRad = 2.5f;

    Rigidbody2D myRb;
    CircleCollider2D lineOfSight;

    private void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
        lineOfSight = GetComponent<CircleCollider2D>();
        lineOfSight.radius = lineOfSightRad;
    }
}