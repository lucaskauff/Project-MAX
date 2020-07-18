using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Max
{
    public class DialogueManager : MonoBehaviour
    {
        #region References
        [Header("My components")]
        [SerializeField] private RectTransform myRect = default;
        [SerializeField] private TextMeshProUGUI textField = default;

        [SerializeField] private RectTransform playerCursor = default;
        #endregion

        #region Variables
        [SerializeField] private float letterSpeed = 0;
        [SerializeField] private float cursorDecalX = 8f;
        [SerializeField] private float cursorDecalY = 8f;
        [SerializeField] private float stillTimer = 0.5f;

        Queue<string> sentences;
        Vector2 originalCursorPos;
        int sentencesDisplayed = 0;
        int cursorLine = 0;
        int topLine = 0;
        int bottomLine = 12;
        public List<int> carPerLine = new List<int>();
        public int cursorCar = 0;
        float t = 0;
        #endregion

        #region Callbacks
        private void Start()
        {
            sentences = new Queue<string>();
            originalCursorPos = playerCursor.localPosition;
        }

        private void Update()
        {
            PlayerCursorMovement();
        }

        public void StartDialogue(Dialogue dialogue)
        {
            sentencesDisplayed = 0;
            cursorLine = 0;
            topLine = 0;
            bottomLine = 11;
            carPerLine.Clear();
            cursorCar = 0;

            sentences.Clear();
            foreach (var sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence + Environment.NewLine);
            }

            playerCursor.gameObject.SetActive(true);
            playerCursor.GetComponent<Animator>().SetBool("Blink", false);
            playerCursor.localPosition = originalCursorPos;

            textField.text = "";
            DisplayNextSentence();
        }

        public void DisplayNextSentence()
        {
            if (sentences.Count == 0)
            {
                EndDialogue();
                return;
            }

            string sentence = sentences.Dequeue();

            StopAllCoroutines(); //to display letter by letter
            StartCoroutine(TypeSentence(sentence));
        }

        public void EndDialogue()
        {
            playerCursor.GetComponent<Animator>().SetBool("Blink", true);
        }

        public void CloseDialogue()
        {
            //add text masking effect
        }

        IEnumerator TypeSentence(string sentence)
        {
            carPerLine.Add(0);

            foreach (char letter in sentence.ToCharArray())
            {
                playerCursor.localPosition = new Vector2(playerCursor.localPosition.x + cursorDecalX, playerCursor.localPosition.y);
                textField.text += letter;
                carPerLine[sentencesDisplayed]++;
                yield return new WaitForSeconds(letterSpeed);
            }

            carPerLine[sentencesDisplayed] -= 1;
            sentencesDisplayed++;

            playerCursor.localPosition = new Vector2(originalCursorPos.x, playerCursor.localPosition.y - cursorDecalY);
            cursorLine++;

            if (sentencesDisplayed > bottomLine)
            {
                myRect.localPosition = new Vector2(myRect.localPosition.x, myRect.localPosition.y + cursorDecalY);
                topLine++;
                bottomLine++;
            }

            DisplayNextSentence();
        }
        #endregion

        #region Utilities
        private void PlayerCursorMovement()
        {
            //up input
            if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (cursorLine > 0)
                {
                    playerCursor.localPosition = new Vector2(originalCursorPos.x, playerCursor.localPosition.y + cursorDecalY);
                    cursorLine--;

                    if (cursorLine < topLine)
                    {
                        myRect.localPosition = new Vector2(myRect.localPosition.x, myRect.localPosition.y - cursorDecalY);
                        topLine--;
                        bottomLine--;
                    }
                }

                CursorStayStill();
            }
            //down input
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (cursorLine < sentencesDisplayed)
                {
                    playerCursor.localPosition = new Vector2(originalCursorPos.x, playerCursor.localPosition.y - cursorDecalY);
                    cursorLine++;

                    if (cursorLine > bottomLine)
                    {
                        myRect.localPosition = new Vector2(myRect.localPosition.x, myRect.localPosition.y + cursorDecalY);
                        topLine++;
                        bottomLine++;
                    }
                }

                CursorStayStill();
            }
            
            //right input
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (cursorLine > bottomLine)
                {
                    if (cursorCar <= carPerLine[cursorLine])
                    {
                        playerCursor.localPosition = new Vector2(playerCursor.localPosition.x + cursorDecalX, playerCursor.localPosition.y);
                        cursorCar++;
                    }
                    else
                    {
                        playerCursor.localPosition = new Vector2(originalCursorPos.x, playerCursor.localPosition.y - cursorDecalY);
                        cursorLine++;
                        cursorCar = 0;
                    }
                }

                CursorStayStill();
            }
        }

        void CursorStayStill()
        {
            playerCursor.GetComponent<Animator>().SetBool("Blink", false);
            StartCoroutine(CursorStillRoutine());
        }

        IEnumerator CursorStillRoutine()
        {
            t = 0;
            while (t < stillTimer)
            {
                t += Time.deltaTime;
                yield return null;
            }
            playerCursor.GetComponent<Animator>().SetBool("Blink", true);
        }
        #endregion
    }
}