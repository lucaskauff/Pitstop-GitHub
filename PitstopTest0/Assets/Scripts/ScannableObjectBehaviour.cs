using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScannableObjectBehaviour : MonoBehaviour
{
    public Sprite associatedIcon;
    public bool isScannable = true;
    public bool isFired = false;
    public bool isArrived = false;
    public float projectileSpeed = 3;
    public Vector2 targetPos;

    private bool col = false;

    private void Update()
    {
        if (isFired)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPos, projectileSpeed * Time.deltaTime);
        }

        if (col || new Vector2(transform.position.x, transform.position.y) == targetPos)
        {
            isArrived = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isFired && other.gameObject.name != "Player")
        {
            col = true;
        }
    }
}