using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
   [SerializeField] private GameObject[] levels;
   private GameObject currentLevel;
    
   private PlayerController playerController;

   public PlayerController Player => playerController;
   
   public void InstantiateLevel(int index)
   {
      if (currentLevel != null)
      {
         Destroy(currentLevel);
      }

     index = index / levels.Length >= 1 ? index % levels.Length : index;
     currentLevel = Instantiate(levels[index], transform);
     playerController = currentLevel.GetComponentInChildren<PlayerController>();
     
   }
}
