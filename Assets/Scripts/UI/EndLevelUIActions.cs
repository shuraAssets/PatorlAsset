using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelUIActions : MonoBehaviour
{
    [Header("Lose Game Elements")]
    [SerializeField] private GameObject _loseGamePanel;

    [Header("Win Game Elements")]
    [SerializeField] private GameObject _winGamePanel;

    private void OnEnable()
    {
        ButtonsHandler.CloseAllPanel += OnCloseAllPanel;
    }

    private void OnDisable()
    {
        ButtonsHandler.CloseAllPanel -= OnCloseAllPanel;
    }

    public void OnLoseGameUI()
    {
        _loseGamePanel.SetActive(true);
    }

    public void OnWinGameUI()
    {
        _winGamePanel.SetActive(true);
    }

    public void OnCloseAllPanel()
    {
        _winGamePanel.SetActive(false);
        _loseGamePanel.SetActive(false);
    }
}
