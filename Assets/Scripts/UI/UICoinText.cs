using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICoinText : MonoBehaviour
{
    [SerializeField] private Text _text = null;

    public void UpdateText(int count)
    {
        _text.text = count.ToString();
    }
}
