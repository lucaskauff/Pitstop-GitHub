using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectZaynPassingThrough : MonoBehaviour
{
    Animator myAnim;

    private void Start()
    {
        myAnim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            myAnim.SetTrigger("Pastro");
        }
    }
}