﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Tilemaps;

namespace Pitstop
{
    public class PlayerControllerIso : MonoBehaviour
    {
        SceneLoader sceneLoader;
        InputManager inputManager;

        //My components
        Rigidbody2D myRb;
        Animator myAnim;

        //Public
        public bool canMove = true;
        public float isometricRatio = 2;

        //Serializable
        [SerializeField]
        float moveSpeed;
        [SerializeField]
        float dashSpeed;
        [SerializeField]
        float dashTime = 1;

        //Private
        Vector2 moveInput;
        public bool isMoving = false;
        Vector2 lastMove = new Vector2(1, 0);
        float dashRate = 0;

        void Start()
        {
            canMove = true;

            sceneLoader = GameManager.Instance.sceneLoader;
            inputManager = GameManager.Instance.inputManager;

            myRb = GetComponent<Rigidbody2D>();
            myAnim = GetComponent<Animator>();

            transform.position = sceneLoader.activeStartingPoint.transform.position;
        }

        void Update()
        {
            isMoving = false;

            if (!canMove)
            {
                myRb.velocity = Vector2.zero;
                myAnim.SetBool("IsMoving", isMoving);
                return;
            }

            moveInput = new Vector2(inputManager.horizontalInput, inputManager.verticalInput / isometricRatio).normalized;

            if (moveInput != Vector2.zero)
            {
                myRb.velocity = new Vector2(moveInput.x * moveSpeed, moveInput.y * moveSpeed);
                isMoving = true;
                lastMove = moveInput;
            }
            else
            {
                myRb.velocity = Vector2.zero;
            }

            if (inputManager.dashKey && Time.time > dashRate)
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
            dashRate = Time.time + dashTime;
            myRb.velocity = lastMove * dashSpeed;
        }
    }
}