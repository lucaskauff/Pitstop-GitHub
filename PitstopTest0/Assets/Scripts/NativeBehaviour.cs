using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NativeBehaviour : MonoBehaviour
{
    public string whereToShoot;

    public Transform[] spearSpawners;
    public GameObject spear;

    [SerializeField]
    float throwRange;

    Vector2 nativePos;
    GameObject cloneSpear;

    void Start()
    {
        nativePos = transform.position;
    }

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
            cloneSpear = (GameObject)Instantiate(spear, spearSpawners[1].position, spear.transform.rotation);
            cloneSpear.GetComponent<SpearBehaviour>().targetPos = new Vector2(nativePos.x + throwRange, nativePos.y);
            cloneSpear.GetComponent<SpearBehaviour>().isFired = true;
            whereToShoot = null;
        }

        if (whereToShoot == "SlotW")
        {
            cloneSpear = (GameObject)Instantiate(spear, spearSpawners[3].position, spear.transform.rotation);
            cloneSpear.transform.Rotate(new Vector3(0, -180, 0));
            cloneSpear.GetComponent<SpearBehaviour>().targetPos = new Vector2(nativePos.x - throwRange, nativePos.y);
            cloneSpear.GetComponent<SpearBehaviour>().isFired = true;
            whereToShoot = null;
        }
    }
}