using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearBehaviour : MonoBehaviour
{
    //[SerializeField] float dmgDealing = 0;
    [SerializeField]
    float throwSpeed = 1;

    public Vector2 targetPos = default;
    public bool isFired = false;

    private void Update()
    {
        if(isFired)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPos, throwSpeed * Time.deltaTime);
        }
    }
}