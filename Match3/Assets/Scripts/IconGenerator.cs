using UnityEngine;
using UnityEngine.UI;

public class IconGenerator : MonoBehaviour
{
    [SerializeField] private Sprite _icon;
    [SerializeField] private Color[] _color;
    public Color[] Color => _color;

    private void Awake()
    {
        Image image = GetComponent<Image>();
        image.sprite = _icon;
        image.color = _color[Random.Range(0,_color.Length)];
    }
}
