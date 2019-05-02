using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Pitstop
{
    public static class TMPosCheck
    {
        public static bool IsInZone(this Tilemap _tilemap, Vector2 _origin, Vector2Int _pos1, Vector2Int _pos2)
        {
            Vector2Int originInTM = (Vector2Int)_tilemap.WorldToCell(_origin);

            return ((_pos1.x < originInTM.x && _pos1.y < originInTM.y) && (originInTM.x < _pos2.x && originInTM.y < _pos2.y));
        }
    }
}