using UnityEngine;
using UnityEngine.UI;

public class Block : MonoBehaviour
{
    private int colorType;
    private Image image;
    private int x;
    private int y;

    public void Initialize(int colorType, int x, int y, Sprite sprite)
    {
        image = GetComponent<Image>();

        this.colorType = colorType;
        this.x = x;
        this.y = y;
        image.sprite = sprite;
    }

    public int GetColorType()
    {
        return colorType;
    }

    public Vector2Int GetPosition()
    {
        return new Vector2Int(x, y);
    }
}
