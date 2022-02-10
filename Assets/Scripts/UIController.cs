using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject failPanel;
    [SerializeField] private GameObject winPanel;

    private GameObject _currentPanel;

    private void DisableCurrentPanel()
    {
        if (_currentPanel != null)
            _currentPanel.SetActive(false);
    }

    public void ShowMenuPanel()
    {
        DisableCurrentPanel();
        _currentPanel = menuPanel;
        _currentPanel.SetActive(true);
    }
    
    public void ShowGamePanel()
    {
        DisableCurrentPanel();
        _currentPanel = gamePanel;
        _currentPanel.SetActive(true);
    }
    
    public void ShowFailPanel()
    {
        DisableCurrentPanel();
        _currentPanel = failPanel;
        _currentPanel.SetActive(true);
    }
    
    public void ShowWinPanel()
    {
        DisableCurrentPanel();
        _currentPanel = winPanel;
        _currentPanel.SetActive(true);
    }
}
