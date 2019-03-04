using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LUD_DetectionTriggeredByAttention : MonoBehaviour
{
    [SerializeField]
    GameObject exclamationPoint;

    
    void Start()
    {
        exclamationPoint.SetActive(false);
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "AttentionZone")
        {
            GetComponentInParent<LUD_NativeHeartheSentence>().isCaptivated = true;
            exclamationPoint.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "AttentionZone")
        {
            GetComponentInParent<LUD_NativeHeartheSentence>().isCaptivated = false;
            exclamationPoint.SetActive(false);
        }
    }
}
