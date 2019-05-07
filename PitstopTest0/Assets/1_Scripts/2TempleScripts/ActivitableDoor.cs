using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class ActivitableDoor : MonoBehaviour
    {
        //Animator myAnim;

        [SerializeField] GameObject doorCollision = default;
        [SerializeField] Animator door1 = default;
        [SerializeField] Animator door2 = default;
        [SerializeField] PressurePlateBehaviour[] neededPressurePlatesToOpen = default;

        private bool allPressurePlatesOk = false;

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

            door1.SetBool("DoorOpen", allPressurePlatesOk);
            door2.SetBool("DoorOpen", allPressurePlatesOk);
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