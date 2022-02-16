using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
   private const string DATA_KEY = "GAME_DATA";

   public GameData LoadData()
   {
      if (PlayerPrefs.HasKey(DATA_KEY))
      {
         return JsonUtility.FromJson<GameData>(PlayerPrefs.GetString(DATA_KEY));
      }
      else
      {
         return new GameData();
      }
   }

   public void SaveData(GameData gameData)
   {
      PlayerPrefs.SetString(DATA_KEY,JsonUtility.ToJson(gameData));
   }
}

[Serializable]
public class GameData
{
   public int Coins = 0;
   public int Level = 0;
}