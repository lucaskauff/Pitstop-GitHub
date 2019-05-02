using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Pitstop
{
    public class testScriptTM : MonoBehaviour
    {
        [SerializeField] private PrevizContact previzContact = default;
        [SerializeField] private Transform previzContactTrans = default;
        [SerializeField] private Tilemap tilemap = default;
        [SerializeField] private Vector2Int pos1 = default;
        [SerializeField] private Vector2Int pos2 = default;
        public bool DebugZone = false;

        void Update()
        {
            TestPreviz();
            Test();
        }

        void TestPreviz()
        {
            previzContact.objectShootable = tilemap.IsInZone(previzContactTrans.position, pos1, pos2);
        }

        void Test()
        {
            if(Input.GetMouseButtonDown(0))
            {
                DebugZone = tilemap.IsInZone(Camera.main.ScreenToWorldPoint(Input.mousePosition), pos1, pos2);
            }
        }
    }
}