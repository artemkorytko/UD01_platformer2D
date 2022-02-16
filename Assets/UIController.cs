using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
   [SerializeField] private GameObject startPanel;
   [SerializeField] private GameObject gamePanel;
   [SerializeField] private GameObject fallPanel;
   [SerializeField] private GameObject winPanel;


   private GameObject _currenPanel;

   private void DisableCurrentPanel()
   {
      _currenPanel?.SetActive(false);
   }

   public void ShowStartScreen()
   {
      DisableCurrentPanel();
      _currenPanel = startPanel;
      _currenPanel.SetActive(true);
   }
   public void ShowGameScreen()
   {
      DisableCurrentPanel();
      _currenPanel = gamePanel;
      _currenPanel.SetActive(true);
   }
   public void ShowFallScreen()
   {
      DisableCurrentPanel();
      _currenPanel = fallPanel;
      _currenPanel.SetActive(true);
   }
   public void ShowWinScreen()
   {
      DisableCurrentPanel();
      _currenPanel = winPanel;
      _currenPanel.SetActive(true);
   }
}
