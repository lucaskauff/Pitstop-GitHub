using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //SerializedField
    [SerializeField]
    float moveSpeed = 5;

    //Private
    Rigidbody2D myRb;
    Animator myAnim;
    Vector2 moveInput;
    float moveSpeedX, moveSpeedY;
    Vector2 lastMove;
    bool playerMoving = false;

    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
    }

    void Update()
    {
        moveSpeedX = moveSpeed;
        moveSpeedY = moveSpeed / 2;

        playerMoving = false;

        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical") / 2).normalized;

        if (moveInput != Vector2.zero)
        {
            //version classique
            myRb.velocity = new Vector2(moveInput.x * moveSpeedX, moveInput.y * moveSpeedX);

            //version iso
            //myRb.velocity = new Vector2(moveInput.x * moveSpeedX, moveInput.y * moveSpeedY);

            lastMove = moveInput;
            playerMoving = true;
        }
        else
        {
            myRb.velocity = Vector2.zero;
        }

        myAnim.SetBool("PlayerMoving", playerMoving);
        myAnim.SetFloat("MoveX", moveInput.x);
        myAnim.SetFloat("MoveY", moveInput.y);
        myAnim.SetFloat("LastMoveX", lastMove.x);
        myAnim.SetFloat("LastMoveY", lastMove.y);
    }
}