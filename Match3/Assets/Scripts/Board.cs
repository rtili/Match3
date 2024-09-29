using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    public List<GameObject> TilesToInteract { get; private set; } = new();
    public Action TilesMarked;
    public GameObject[,] Tiles => _tiles;

    private GameObject[,] _tiles;
    private HashSet<GameObject> _processedTiles = new();

    private void Start()
    {
        SetGrid();
    }

    private void SetGrid()
    {
        int rows = transform.childCount;
        int columns = transform.GetChild(0).childCount;
        _tiles = new GameObject[rows, columns];

        for (int row = 0; row < rows; row++)
        {
            Transform rowTransform = transform.GetChild(row);
            for (int column = 0; column < columns; column++)
            {
                Transform tileTransform = rowTransform.GetChild(column);
                _tiles[row, column] = tileTransform.gameObject;
                tileTransform.GetComponent<Tile>().Board = this;
            }
        }
    }

    public Vector2Int FindTilePosition(GameObject tile)
    {
        for (int row = 0; row < _tiles.GetLength(0); row++)        
            for (int column = 0; column < _tiles.GetLength(1); column++)           
                if (_tiles[row, column] == tile)               
                    return new Vector2Int(row, column);     
        return Vector2Int.one * -1;
    }

    public GameObject[] GetNeighbours(int row, int column)
    {
        List<GameObject> neighbours = new();

        if (row > 0)
            neighbours.Add(_tiles[row - 1, column]);
        if (row < _tiles.GetLength(0) - 1)
            neighbours.Add(_tiles[row + 1, column]);
        if(column > 0)
            neighbours.Add(_tiles[row, column - 1]);
        if(column < _tiles.GetLength(1) - 1)
            neighbours.Add(_tiles[row, column + 1]);

        return neighbours.ToArray();
    }

    public void MarkNeighbours(int row, int column, Color targetColor)
    {
        Queue<Vector2Int> tilesToCheck = new ();
        tilesToCheck.Enqueue(new Vector2Int(row, column));

        while (tilesToCheck.Count > 0)
        {
            Vector2Int currentTilePosition = tilesToCheck.Dequeue();
            GameObject currentTile = _tiles[currentTilePosition.x, currentTilePosition.y];

            if (_processedTiles.Contains(currentTile))
                continue;

            GameObject currentTileIcon = currentTile.transform.GetChild(0).gameObject;
            Image currentTileImage = currentTileIcon.GetComponent<Image>();

            if (ColorsAreSimilar(currentTileImage.color, targetColor))
            {
                _processedTiles.Add(currentTile);
                TilesToInteract.Add(currentTile);

                GameObject[] neighbours = GetNeighbours(currentTilePosition.x, currentTilePosition.y);
                foreach (GameObject neighbour in neighbours)
                {
                    GameObject neighbourIcon = neighbour.transform.GetChild(0).gameObject;
                    Image neighbourImage = neighbourIcon.GetComponent<Image>();

                    if (ColorsAreSimilar(neighbourImage.color, targetColor))
                    {
                        Vector2Int neighbourPosition = FindTilePosition(neighbour);
                        tilesToCheck.Enqueue(neighbourPosition);
                    }
                }
            }
        }

        TilesMarked?.Invoke();
        _processedTiles.Clear();
        TilesToInteract.Clear();
    }

    private bool ColorsAreSimilar(Color color1, Color color2)
    {
        string color1String = ColorUtility.ToHtmlStringRGBA(color1);
        string color2String = ColorUtility.ToHtmlStringRGBA(color2);

        return color1String == color2String;
    }
}
