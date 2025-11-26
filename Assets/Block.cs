using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Block : MonoBehaviour, IPointerClickHandler
{
    private int colorType;
    private Image image;
    private int x;
    private int y;
    private Grid grid;

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

    public void SetGrid(Grid grid)
    {
        this.grid = grid;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (grid != null)
        {
            grid.OnBlockClicked(this);
        }
    }
}
