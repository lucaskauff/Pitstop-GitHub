using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Tilemaps;

public class PlayerControllerIso : MonoBehaviour
{
    InputManager inputManager;
    
    //My components
    Rigidbody2D myRb;
    Animator myAnim;

    public float moveSpeed;

    public float dashSpeed;
    public float dashTime;
    private float dashRate = 0;
    //public float startDashTime;

    //public List<string> levels = new List<string>(2);

    Vector2 moveInput;
    public bool canMove = true;
    bool isMoving = false;
    Vector2 lastMove = new Vector2(1, 0);

    void Start()
    {
        inputManager = GameManager.Instance.inputManager;

        myRb = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();

        //dashTime = startDashTime;
    }

    void Update()
    {
        isMoving = false;

        if (!canMove)
        {
            myRb.velocity = Vector2.zero;
            return;
        }

        moveInput = new Vector2(inputManager.horizontalInput, inputManager.verticalInput / 2).normalized;

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

        if (inputManager.dashKey)
        {
            Dash();            
        }

        //Infos to animator
        myAnim.SetBool("IsMoving", isMoving);
        myAnim.SetFloat("LastMoveX", lastMove.x);
        myAnim.SetFloat("LastMoveY", lastMove.y);
        myAnim.SetFloat("MoveX", moveInput.x);
        myAnim.SetFloat("MoveY", moveInput.y);
    }

    void Dash()
    {

    }

    void DashTest()
    {
        if (dashTime <= 0)
        {
            //dashTime = startDashTime;
            myRb.velocity = Vector2.zero;
        }
        else
        {
            dashTime -= Time.deltaTime;
            myRb.velocity = lastMove * dashSpeed;
        }
    }
}