using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MoveTiles : MonoBehaviour
{
    [SerializeField] private Board _board;
    [SerializeField] private IconGenerator _iconGenerator;

    private void Start()
    {
        _board.TilesMarked += ChangeTilesIcons;
    }

    private void ChangeTilesIcons()
    {
        Sequence sequence = DOTween.Sequence();

        foreach (var tile in _board.TilesToInteract)
        {
            GameObject icon = tile.transform.GetChild(0).gameObject;
            Image image = icon.GetComponent<Image>();
            sequence.Join(image.DOFade(0, 0.5f));
        }
        sequence.OnComplete(() => StartCoroutine(FillTilesWithFall()));
    }

    private IEnumerator FillTilesWithFall()
    {
        int rows = _board.Tiles.GetLength(0);
        int columns = _board.Tiles.GetLength(1);

        for (int col = 0; col < columns; col++)
        {
            for (int row = rows - 1; row >= 0; row--)
            {
                GameObject tile = _board.Tiles[row, col];
                Image image = tile.transform.GetChild(0).GetComponent<Image>();

                if (image.color.a == 0)
                {
                    yield return StartCoroutine(FallDown(row, col));
                }
            }
        }
    }

    private IEnumerator FallDown(int row, int col)
    {
        for (int upperRow = row - 1; upperRow >= 0; upperRow--)
        {
            GameObject upperTile = _board.Tiles[upperRow, col];
            Image upperImage = upperTile.transform.GetChild(0).GetComponent<Image>();

            if (upperImage.color.a > 0)
            {
                UpdateTileColor(row, col, upperImage.color);
                UpdateTileColor(upperRow, col, new Color(0, 0, 0, 0));
                yield return new WaitForSeconds(0.1f);
                break;
            }
        }

        if (_board.Tiles[row, col].transform.GetChild(0).GetComponent<Image>().color.a == 0)
        {
            UpdateTileColor(row, col, GetRandomColor());
        }
    }

    private void UpdateTileColor(int row, int col, Color color)
    {
        GameObject tile = _board.Tiles[row, col];
        Image image = tile.transform.GetChild(0).GetComponent<Image>();
        image.color = color;
    }

    private Color GetRandomColor()
    {
        return _iconGenerator.Color[Random.Range(0, _iconGenerator.Color.Length)];
    }
}
