using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Block : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private int colorType;
    private Image image;
    private int x;
    private int y;
    private Grid grid;
    private bool isHovered;

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

    public void SetDarkened(bool darkened)
    {
        if (image != null)
        {
            image.color = darkened ? new Color(0.7f, 0.7f, 0.7f) : (isHovered ? new Color(0.95f, 0.95f, 0.95f) : Color.white);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true;
        if (image != null)
        {
            image.color = new Color(0.95f, 0.95f, 0.95f);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
        if (image != null)
        {
            image.color = Color.white;
        }
    }
}
