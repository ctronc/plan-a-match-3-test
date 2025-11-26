using UnityEngine;

public class Block : MonoBehaviour
{
    private int colorType;
    private SpriteRenderer spriteRenderer;
    private int x;
    private int y;

    public void Initialize(int colorType, int x, int y, Sprite sprite)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        this.colorType = colorType;
        this.x = x;
        this.y = y;
        spriteRenderer.sprite = sprite;
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
