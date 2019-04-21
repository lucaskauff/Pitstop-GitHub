using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionWithTree : MonoBehaviour
{
    Animator myAnim;

    private void Start()
    {
        myAnim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy")
        {
            myAnim.SetTrigger("Collision");
        }
    }
}