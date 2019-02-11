using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearBehaviour : MonoBehaviour
{
    [SerializeField]
    float dmgDealing;
    [SerializeField]
    float throwSpeed;

    public Vector2 targetPos;
    public bool isFired = false;

    private void Update()
    {
        if(isFired)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPos, throwSpeed * Time.deltaTime);
        }
    }
}