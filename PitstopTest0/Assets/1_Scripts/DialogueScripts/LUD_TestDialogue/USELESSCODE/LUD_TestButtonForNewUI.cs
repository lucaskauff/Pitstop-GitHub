using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LUD_TestButtonForNewUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public void WhenClickedSendMessage ()
    {
        Debug.Log("Button " + this.name +  " has been pressed");
    }
}
