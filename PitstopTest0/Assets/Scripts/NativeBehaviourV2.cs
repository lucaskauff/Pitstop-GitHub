using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NativeBehaviourV2 : MonoBehaviour
{
    [SerializeField]
    float moveSpeed;

    public GameObject[] interestPoints;
    Transform whereToGo;
    bool hasToMove = false;

    private void Update()
    {
        if (hasToMove)
        {
            transform.position = Vector2.MoveTowards(transform.position, whereToGo.position, moveSpeed * Time.deltaTime);

            if (transform.position == whereToGo.position)
            {
                whereToGo = null;
                hasToMove = false;
            }
        }
    }

    public void ModifyTarget(Transform target)
    {
        whereToGo = target;
        hasToMove = true;
    }
}