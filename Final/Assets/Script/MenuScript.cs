using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour {
    /// <summary>
    /// Load main screen
    /// </summary>
    public void StartGame()
    {
        Application.LoadLevel("Main");
    }

    /// <summary>
    /// Application quit
    /// </summary>
    public void EndGame()
    {
        Application.Quit();
    }
}
