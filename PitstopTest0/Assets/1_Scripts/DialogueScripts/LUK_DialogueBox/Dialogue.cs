using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    [System.Serializable]
    public class Dialogue
    {
        //public string name;

        /*
        [TextArea(3, 10)]
        public string[] sentences;
        */

        public SpecificDialoguePart[] allDialoguePart;
    }

    [System.Serializable]
    public class SpecificDialoguePart
    {
        public string speaker;

        [TextArea(3, 10)]
        public string sentence;

    }
}