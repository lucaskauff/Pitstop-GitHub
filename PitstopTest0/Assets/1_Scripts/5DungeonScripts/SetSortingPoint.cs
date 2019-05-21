using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class SetSortingPoint : MonoBehaviour
    {
        [SerializeField] SpriteRenderer myRenderer = default;
        [SerializeField] int newSortingOrder = -1;
        [SerializeField] ActivatableAltar associatedAltar = default;

        private void Update()
        {
            if (associatedAltar.doorIsOpened)
            {
                myRenderer.sortingOrder = newSortingOrder;
            }
        }
    }
}