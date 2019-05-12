using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class ChangingStartingPoint : MonoBehaviour
    {
        [SerializeField] PlayerControllerIso playerControllerIso = default;
        [SerializeField] int associatedIndex = 0;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player" && associatedIndex > PlayerControllerIso.savingPointIndex)
            {
                playerControllerIso.IncrementSavingPoint(associatedIndex);
            }
        }
    }
}