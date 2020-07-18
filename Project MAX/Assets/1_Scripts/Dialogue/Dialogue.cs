using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Max
{
    [System.Serializable]
    public class Dialogue
    {
        [TextArea(1, 12)]
        public string[] sentences;
    }
}