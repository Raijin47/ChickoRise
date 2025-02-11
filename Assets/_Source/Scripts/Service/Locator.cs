using System;
using UnityEngine;

[Serializable]
public class Locator
{
    [SerializeField] private InputHandler _input;
    [SerializeField] private Transform _player;
    [SerializeField] private Factory _factory;
    [SerializeField] private ChunkHandler _chunk;
    public InputHandler Input => _input;
    public Transform Player => _player;
    public Factory Factory => _factory;
    public ChunkHandler Chunk => _chunk;
}