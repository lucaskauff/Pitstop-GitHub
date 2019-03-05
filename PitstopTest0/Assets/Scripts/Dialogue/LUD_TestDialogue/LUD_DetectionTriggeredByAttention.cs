using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LUD_DetectionTriggeredByAttention : MonoBehaviour
{
    
    
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "AttentionZone")
        {
            GetComponentInParent<LUD_NativeHeartheSentence>().isCaptivated = true;
            GetComponentInParent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "AttentionZone")
        {
            GetComponentInParent<LUD_NativeHeartheSentence>().isCaptivated = false;
            GetComponentInParent<SpriteRenderer>().color = new Color(0.3f, 0.3f, 0.3f);

        }
    }
}
