using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Tilemaps;

public class IsometricStaticObject : MonoBehaviour
{
    public float floorHeight;

    private float spriteLowerBound;
    private float spriteHalfWidth;
    private readonly float tan30 = Mathf.Tan(Mathf.PI / 5);

    void Start()
    {
        SpriteRenderer spriteRend = GetComponent<SpriteRenderer>();
        spriteLowerBound = spriteRend.bounds.size.y * 0.5f;
        spriteHalfWidth = spriteRend.bounds.size.x * 0.5f;

        //TilemapRenderer tileRend = GetComponent<TilemapRenderer>();

        /*if (spriteRend != null)
        {
            spriteLowerBound = spriteRend.bounds.size.y * 0.5f;
            spriteHalfWidth = spriteRend.bounds.size.x * 0.5f;
        }
        else
        {
            spriteLowerBound = tileRend.bounds.size.y * 0.5f;
            spriteHalfWidth = tileRend.bounds.size.x * 0.5f;
        }*/
    }

    #if UNITY_EDITOR
    void LateUpdate()
    {
        if (!Application.isPlaying)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, (transform.position.y - spriteLowerBound + floorHeight) * tan30);
        }
    }
    #endif

    void OnDrawGizmos()
    {
        Vector3 floorHeightPos = new Vector3(transform.position.x, transform.position.y - spriteLowerBound + floorHeight, transform.position.z);

        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(floorHeightPos + Vector3.left * spriteHalfWidth, floorHeightPos + Vector3.right * spriteHalfWidth);
    }
}