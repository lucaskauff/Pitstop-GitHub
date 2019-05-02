using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Pitstop
{
    public class GetPosTM : MonoBehaviour
    {
        [SerializeField] private Tilemap tilemap;

        void Update()
        {
            if (Input.GetMouseButtonDown(0)) GetPos();
        }

        private void GetPos()
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log("World pos: "+mousePos+"\n Tilemap pos: "+tilemap.WorldToCell(mousePos));
        }
    }
}