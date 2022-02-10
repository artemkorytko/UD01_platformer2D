using System;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public event Action OnWin;
    public event Action OnFail;
    public event Action OnCoinsCollected;
}
