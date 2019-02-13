using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricDynamicObject : MonoBehaviour
{
    [SerializeField]
    private float floorHeight;

    private SpriteRenderer myRend;
    private float spriteHalfHeight;
    private float spriteHalfWidth;
    private readonly float tan30 = Mathf.Tan(Mathf.PI / 5);

    void Start()
    {
        myRend = GetComponent<SpriteRenderer>();

        spriteHalfWidth = myRend.bounds.size.x * 0.5f;
        spriteHalfHeight = myRend.bounds.size.y * 0.5f;
    }

    void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, (transform.position.y - spriteHalfHeight + floorHeight) * tan30);
    }

    void OnDrawGizmos()
    {
        Vector3 floorHeightPos = new Vector3(transform.position.x, transform.position.y - spriteHalfHeight + floorHeight, transform.position.z);

        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(floorHeightPos + Vector3.left * spriteHalfWidth, floorHeightPos + Vector3.right * spriteHalfWidth);
    }
}