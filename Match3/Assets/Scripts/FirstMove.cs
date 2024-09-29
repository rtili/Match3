using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstMove : MonoBehaviour
{
    [SerializeField] private GameObject[] _tiles;
    [SerializeField] private Board _board;
    private List<Tween> _tweens = new ();

    private void Start()
    {
        ShowMove();
        _board.TilesMarked += EndAnimation;
    }

    private void ShowMove()
    {
        foreach (var tile in _tiles)
        {
            GameObject icon = tile.transform.GetChild(0).gameObject;
            Image image = icon.GetComponent<Image>();
            image.color = Color.red;

            Tween tween = StartAnimation(icon);
            _tweens.Add(tween);
        }
    }

    private Tween StartAnimation(GameObject obj)
    {
        Vector3 original = obj.transform.localScale;
        Vector3 scale = original / 2;
        return obj.transform.DOScale(scale, 1)
            .SetEase(Ease.InOutSine)
            .SetLoops(10, LoopType.Yoyo);
    }

    private void EndAnimation()
    {
        foreach (var tween in _tweens)
        {
            tween.Kill(true);
        }
        _tweens.Clear();
        _board.TilesMarked -= EndAnimation;
    }
}
