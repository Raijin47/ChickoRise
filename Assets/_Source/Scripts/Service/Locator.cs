using System;
using UnityEngine;

[Serializable]
public class Locator
{
    [SerializeField] private InputHandler _input;

    public InputHandler Input => _input;
}