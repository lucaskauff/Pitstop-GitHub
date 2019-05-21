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

        public AudioSource soundOfOpeningDoor;
        public AudioSource soundOfClosingDoor;

        private void Update()
        {
            CheckPressurePlates();


            //if(soundOfOpeningDoor.isPlaying) soundOfOpeningDoor.Play();

            
            if (allPressurePlatesOk) soundOfOpeningDoor.Play();
            else if (!allPressurePlatesOk) soundOfClosingDoor.Play();
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