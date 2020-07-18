using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Max
{
    public class Initialization : MonoBehaviour
    {
        #region References
        [SerializeField] private Image switchImage = default;
        [SerializeField] private Image pcBackgroundImage = default;
        [SerializeField] private Animator logoImage = default;
        [SerializeField] private DialogueManager dialogueManager = default;
        #endregion

        #region Variables
        public Dialogue initText = default;

        [SerializeField] private string nextSceneName = default;

        [SerializeField] private float[] initTimers = default;

        [Header("Colors")]
        [SerializeField] private Color switchColorOff = default; //not used
        [SerializeField] private Color switchColorOn = default;
        [SerializeField] private Color pcBackgroundColorOff = default; //not used
        [SerializeField] private Color pcBackgroundColorOn = default;

        private int initPhase = 0;
        private float t;
        #endregion

        #region Callbacks
        private void Start()
        {
            StartCoroutine(InitRoutine(initPhase));
        }

        public IEnumerator InitRoutine(int initPhase)
        {
            t = 0;
            while (t < initTimers[initPhase])
            {
                t += Time.deltaTime;
                yield return null;
            }            
            switch (initPhase)
            {
                case 0:
                    switchImage.color = switchColorOn;
                    EndOfPhase();
                    break;

                case 1:
                    pcBackgroundImage.color = pcBackgroundColorOn;
                    EndOfPhase();
                    break;

                case 2:
                    logoImage.SetTrigger("Turn on");
                    break;

                case 3:
                    dialogueManager.StartDialogue(initText);
                    break;
            }
        }
        #endregion

        #region Utilities
        public void EndOfPhase()
        {
            initPhase++;
            StartCoroutine(InitRoutine(initPhase));
        }
        #endregion
    }
}