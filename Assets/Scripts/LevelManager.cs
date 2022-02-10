using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _levels = null;

    private GameObject _currentLevel = null;
    private PlayerController _player = null;

    public PlayerController Player => _player;

    public void InstantiateLevel(int index)
    {
        if (_currentLevel != null)
        {
            Destroy(_currentLevel);
        }

        index = index / _levels.Count >= 1 ? index % _levels.Count : index;

        _currentLevel = Instantiate(_levels[index], transform);
        _player = _currentLevel.GetComponentInChildren<PlayerController>();
    }
}
