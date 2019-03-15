using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    [System.Serializable]
    public class ScanData
    {
        public Sprite associatedIcon;
        public bool scannable;
        public bool fired;
        public bool arrived;
        public ScannableObjects type;

        public ScanData(ScannableObjects _type, Sprite _sprite)
        {
            scannable = true;
            fired = false;
            arrived = false;
            type = _type;
        }
    }
}