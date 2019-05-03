using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class ActivitableDoor : MonoBehaviour
    {
        //Animator myAnim;

        [SerializeField]
        GameObject doorCollision = default;
        [SerializeField]
        GameObject door1 = default;
        [SerializeField]
        GameObject door2 = default;
        [SerializeField]
        PressurePlateBehaviour[] neededPressurePlatesToOpen = default;

        private bool allPressurePlatesOk = false;

        private void Start()
        {
            //myAnim = GetComponent<Animator>();
        }

        private void Update()
        {
            CheckPressurePlates();

            if (allPressurePlatesOk)
            {
                doorCollision.SetActive(false);
                door1.SetActive(false);
                door2.SetActive(false);
            }
            else
            {
                doorCollision.SetActive(true);
                door1.SetActive(true);
                door2.SetActive(true);
            }

            //myAnim.SetBool("DoorOpen", allPressurePlatesOk);
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
}