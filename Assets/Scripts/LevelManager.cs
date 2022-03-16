using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject[] levels;

    private CharacterController _character;
    private GameObject _currentLevel;
    private int _currentLevelIndex;

    public CharacterController Character => _character;

    public GameObject CurrentLevel
    {
        get => _currentLevel;
        set => _currentLevel = value;
    }

    public void InstantiateLevel(int index)
    {
        _currentLevelIndex = index;
        if (CurrentLevel != null)
        {
            Destroy(CurrentLevel);
        }

        index = index / levels.Length >= 1 ? index % levels.Length : index;
        _currentLevel = Instantiate(levels[index], transform);
        _character = GetComponentInChildren<CharacterController>();
        
    }

    public void ReloadLevel()
    {
        InstantiateLevel(_currentLevelIndex);
    }

    public void NextLevel()
    {
        InstantiateLevel(_currentLevelIndex + 1);
    }

    public void DisableCurrentLevel()
    {
        CurrentLevel.SetActive(false);
    }
}
