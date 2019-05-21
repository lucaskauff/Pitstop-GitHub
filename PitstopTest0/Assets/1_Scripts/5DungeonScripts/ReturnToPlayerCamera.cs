using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class ReturnToPlayerCamera : MonoBehaviour
    {
        [SerializeField] ActivatableAltar associatedAltar = default;

        public void DoTheCameraBlend()
        {
            associatedAltar.ReturnToPlayerCamera();            
        }
    }
}