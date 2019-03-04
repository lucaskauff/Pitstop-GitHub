using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivitableDoor : MonoBehaviour
{
    Animator myAnim;

    [SerializeField]
    GameObject doorCollision;
    [SerializeField]
    PressurePlateBehaviour[] neededPressurePlatesToOpen;

    private bool allPressurePlatesOk = false;

    private void Start()
    {
        myAnim = GetComponent<Animator>();
    }

    private void Update()
    {
        CheckPressurePlates();

        if (allPressurePlatesOk)
        {
            doorCollision.SetActive(false);
        }
        else
        {
            doorCollision.SetActive(true);
        }

        myAnim.SetBool("DoorOpen", allPressurePlatesOk);
    }

    void CheckPressurePlates()
    {
        foreach (var pressurePlate in neededPressurePlatesToOpen)
        {
            if (!pressurePlate.plateDown)
            {
                allPressurePlatesOk = false;
                return;
            }

            allPressurePlatesOk = true;
        }
    }
}