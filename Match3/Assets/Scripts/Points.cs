using UnityEngine;
using UnityEngine.UI;

public class Points : MonoBehaviour
{
    [SerializeField] private Text _pointsCount;
    [SerializeField] private Board _board;
    [SerializeField] private int _pointForBall;
    public int EarnedPoints => _points;
    private int _points;

    private void Start()
    {
        _board.TilesMarked += EarnPoints;
    }

    private void EarnPoints()
    {
        int multiplier = _board.TilesToInteract.Count;
        _points += _board.TilesToInteract.Count * _pointForBall * multiplier;       
        _pointsCount.text = _points.ToString();
    }
}
