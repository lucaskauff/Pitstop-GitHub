using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Pitstop
{
    public class testScriptTM : MonoBehaviour
    {
        [SerializeField] private PrevizContact previzContact = default;
        [SerializeField] private Tilemap tilemap = default;
        [SerializeField] private Vector2Int pos1 = default;
        [SerializeField] private Vector2Int pos2 = default;

        void Update()
        {
            TestPreviz();
        }

        void TestPreviz()
        {
            previzContact.objectShootable = tilemap.IsInZone(previzContact.transform.position, pos1, pos2);
        }        
    }
}