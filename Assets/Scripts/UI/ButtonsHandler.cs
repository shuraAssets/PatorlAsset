using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsHandler : MonoBehaviour
{
    public delegate void ButtonPressed();
    public static ButtonPressed NextLevel;
    public static ButtonPressed RestartLevel;
    // public static ButtonPressed ExitToMainMenu;
    public static ButtonPressed CloseAllPanel;

    public void LoadNextlevel()
    {
        NextLevel?.Invoke();
        CloseAllPanel?.Invoke();
    }

    public void RestartCurrentLevel()
    {
        RestartLevel?.Invoke();
        CloseAllPanel?.Invoke();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
