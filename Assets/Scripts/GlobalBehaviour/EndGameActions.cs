using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameActions : MonoBehaviour
{
    [SerializeField] private EndLevelUIActions _endGameUI;

    #region Enable/Disable

    private void OnEnable()
    {
        FieldOfViewHandler.LoseGame += OnLoseGame;
        WinTrigger.WinGame += OnWinGame;
        ButtonsHandler.RestartLevel += OnRestartGame;
    }

    private void OnDisable()
    {
        FieldOfViewHandler.LoseGame -= OnLoseGame;
        WinTrigger.WinGame -= OnWinGame;
        ButtonsHandler.RestartLevel -= OnRestartGame;
    }

    #endregion

    private void OnLoseGame()
    {
        GlobalSettings.worldTime = 0;

        _endGameUI.OnLoseGameUI();
    }

    private void OnWinGame()
    {
        GlobalSettings.worldTime = 0;

        _endGameUI.OnWinGameUI();
    }

    private void OnRestartGame()
    {
        SceneManager.LoadScene(0);
    }
}