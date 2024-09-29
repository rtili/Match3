using System;
using UnityEngine;
using UnityEngine.UI;

public class Moves : MonoBehaviour
{
    [SerializeField] private Text _movesCount;
    [SerializeField] private Board _board;
    [SerializeField] private int _startMoves;
    public Action MovesEnded;
    private int _moves;
  
    private void Start()
    {
        _board.TilesMarked += CalculateMoves;
        _movesCount.text = _startMoves.ToString();
        _moves = _startMoves;
    }

    private void CalculateMoves()
    {
        if (_board.TilesToInteract.Count >= 3)
            _moves += _board.TilesToInteract.Count - 1;
        else
            _moves -= 1;
        if (_moves <= 0)
            MovesEnded?.Invoke();
        _movesCount.text = _moves.ToString();
    }
}
