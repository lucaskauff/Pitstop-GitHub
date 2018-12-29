using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;

    SpriteRenderer myRend;
    Rigidbody2D myRb;

    public List<string> levels = new List<string>(2);

    Vector2 moveInput;

    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
        myRend = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical") / 2).normalized;

        if(moveInput != Vector2.zero)
        {
            myRb.velocity = new Vector2(moveInput.x * moveSpeed, moveInput.y * moveSpeed);
        }
        else
        {
            myRb.velocity = Vector2.zero;
        }        
    }

    private void LateUpdate()
    {
        //myRend.sortingOrder = -(int)(transform.position.y * 100);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Stairs")
        {
            int stairsLevel = levels.IndexOf(collision.gameObject.GetComponent<TilemapRenderer>().sortingLayerName);

            if ((collision.gameObject.name == "StairsLvl1L" && (moveInput.y > 0 || moveInput.x < 0)) || (collision.gameObject.name == "StairsLvl1R" && (moveInput.y > 0 || moveInput.x > 0)))
            {                
                myRend.sortingLayerName = levels[stairsLevel + 1];
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Stairs" && moveInput != Vector2.zero)
        {
            GetComponent<CinemachineImpulseSource>().GenerateImpulse();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Stairs")
        {
            int stairsLevel = levels.IndexOf(collision.gameObject.GetComponent<TilemapRenderer>().sortingLayerName);

            if ((collision.gameObject.name == "StairsLvl1L" && (moveInput.y < 0 || moveInput.x > 0)) || (collision.gameObject.name == "StairsLvl1R" && (moveInput.y < 0 || moveInput.x < 0)))
            {
                myRend.sortingLayerName = levels[stairsLevel];
            }
        }
    }
}