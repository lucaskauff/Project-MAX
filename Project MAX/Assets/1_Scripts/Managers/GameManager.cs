using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Max
{
    public class GameManager : Singleton<GameManager>
    {
        protected GameManager() { }

        #region References

        #endregion

        #region Variables

        #endregion

        #region Callbacks
        public void Update()
        {
            DebugInputs();
        }
        #endregion

        #region Utilities


        void DebugInputs()
        {
            //Pause game
            if (Input.GetKeyDown(KeyCode.P))
            {
                if (Time.timeScale != 0)
                {
                    Time.timeScale = 0;
                }
                else
                {
                    Time.timeScale = 1;
                }
            }

            //Reload scene
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

            //Quit game
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
        #endregion
    }
}