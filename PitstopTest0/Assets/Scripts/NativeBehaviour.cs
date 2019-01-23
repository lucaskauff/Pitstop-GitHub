using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NativeBehaviour : MonoBehaviour
{
    public string whereToShoot;

    public Transform spearSpawner;
    public GameObject spear;
    GameObject cloneSpear;

    void Update()
    {
        if(whereToShoot != null)
        {
            Shot();
        }
    }

    void Shot()
    {
        

        if (whereToShoot == "SlotE")
        {
            cloneSpear = (GameObject)Instantiate(spear, spearSpawner.position, spear.transform.rotation);
        }
    }
}