using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    private const string DATA_KEY = "GAMEDATA";

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

        //return PlayerPrefs.HasKey(DATA_KEY) != null
        //    ? JsonUtility.FromJson<GameData>(PlayerPrefs.GetString(DATA_KEY))
        //    : new GameData();
    }

    public void SaveData(GameData data)
    {
        PlayerPrefs.SetString(DATA_KEY, JsonUtility.ToJson(data));
    }
}

[Serializable]
public class GameData
{
    public int Coins = 0;
    public int Level = 0;
}
