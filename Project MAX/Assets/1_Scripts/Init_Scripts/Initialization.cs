using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Max
{
    public class Initialization : MonoBehaviour
    {
        #region References
        [SerializeField] private Image pcBackgroundImage = default;
        [SerializeField] private Image switchImage = default;
        [SerializeField] private Animator logoImage = default;
        #endregion

        #region Variables
        [SerializeField] private string nextSceneName = default;

        [Header("Colors")]
        [SerializeField] private Color pcBackgroundColorOff = default;
        [SerializeField] private Color pcBackgroundColorOn = default;
        [SerializeField] private Color switchColorOn = default;
        [SerializeField] private Color switchColorOff = default;
        #endregion

        #region Callbacks
        private void Start()
        {
            
        }
        #endregion

        #region Utilities

        #endregion
    }
}