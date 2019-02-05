using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Tilemaps;

public class PlayerControllerIso : MonoBehaviour
{
    public float moveSpeed;

    SpriteRenderer myRend;
    Rigidbody2D myRb;
    Animator myAnim;

    public List<string> levels = new List<string>(2);

    Vector2 moveInput;
    public bool canMove = true;
    bool isMoving = false;
    Vector2 lastMove = new Vector2(1, 0);

    void Start()
    {
        myRend = GetComponent<SpriteRenderer>();
        myRb = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
    }

    void Update()
    {
        isMoving = false;

        if (!canMove)
        {
            myRb.velocity = Vector2.zero;
            return;
        }

        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical") / 2).normalized;

        if(moveInput != Vector2.zero)
        {
            myRb.velocity = new Vector2(moveInput.x * moveSpeed, moveInput.y * moveSpeed);
            isMoving = true;
            lastMove = moveInput;
        }
        else
        {
            myRb.velocity = Vector2.zero;
        }

        myAnim.SetBool("IsMoving", isMoving);
        myAnim.SetFloat("LastMoveX", lastMove.x);
        myAnim.SetFloat("LastMoveY", lastMove.y);
        myAnim.SetFloat("MoveX", moveInput.x);
        myAnim.SetFloat("MoveY", moveInput.y);
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