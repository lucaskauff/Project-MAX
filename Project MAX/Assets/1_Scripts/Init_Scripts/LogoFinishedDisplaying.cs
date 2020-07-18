using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Max
{
    public class LogoFinishedDisplaying : MonoBehaviour
    {
        [SerializeField] private Initialization initScript = default;

        public void LogoFinishDisplay()
        {
            initScript.EndOfPhase();
        }
    }
}