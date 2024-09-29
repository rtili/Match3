using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public Board Board { set { _board = value; } }
    private Board _board;

    public void DestroyTile()
    {
        Vector2Int position = _board.FindTilePosition(gameObject);

        if (position != Vector2Int.one * -1)
        {
            GameObject icon = gameObject.transform.GetChild(0).gameObject;
            Image image = icon.GetComponent<Image>();
            Color initialColor = image.color;

            _board.MarkNeighbours(position.x, position.y, initialColor);
        }
    }
}
