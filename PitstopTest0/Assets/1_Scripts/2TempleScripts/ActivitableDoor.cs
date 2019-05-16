using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class ActivitableDoor : MonoBehaviour
    {
        [SerializeField] Animator door1 = default;
        [SerializeField] Animator door2 = default;
        [SerializeField] PressurePlateBehaviour[] neededPressurePlatesToOpen = default;

        private bool allPressurePlatesOk = false;

        private void Update()
        {
            CheckPressurePlates();

            door1.SetBool("DoorOpen", allPressurePlatesOk);
            if (door2 != null)
            {
                door2.SetBool("DoorOpen", allPressurePlatesOk);
            }
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