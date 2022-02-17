using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject[] levels;

    private CharacterController _character;
    private GameObject _currentLevel;

    public CharacterController Character => _character;

    public GameObject CurrentLevel
    {
        get => _currentLevel;
        set => _currentLevel = value;
    }

    public void InstantiateLevel(int index)
    {
        if (CurrentLevel != null)
        {
            Destroy(CurrentLevel);
        }

        index = index / levels.Length >= 1 ? index % levels.Length : index;
        _currentLevel = Instantiate(levels[index], transform);
        _character = GetComponentInChildren<CharacterController>();
        
    }

    public void DisableCurrentLevel()
    {
        CurrentLevel.SetActive(false);
    }
}
