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

    public bool col = false;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (isFired)
        {
            if (gameObject.tag != "ObjectRoot")
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPos, projectileSpeed * Time.deltaTime);
            }
        }

        if (col || new Vector2(transform.position.x, transform.position.y) == targetPos)
        {
            isArrived = true;
            isFired = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (isFired && other.gameObject.name != "Zayn")
        {
            col = true;
        }
    }
}