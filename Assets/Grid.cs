using UnityEngine;

public class Grid : MonoBehaviour
{
    private const int WIDTH = 5;
    private const int HEIGHT = 6;
    private const int BLOCK_TYPES = 5;

    [SerializeField] private Block blockPrefab;
    [SerializeField] private Sprite[] blockSprites;
    [SerializeField] private Transform gridParent;
    [SerializeField] private Vector2 gridOrigin = Vector2.zero;
    [SerializeField] private float horizontalSpacing = 1f;
    [SerializeField] private float verticalSpacing = 1f;

    private Block[,] blocks;

    void Start()
    {
        blocks = new Block[WIDTH, HEIGHT];
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        for (int x = 0; x < WIDTH; x++)
        {
            for (int y = 0; y < HEIGHT; y++)
            {
                CreateBlock(x, y);
            }
        }
    }

    private Block CreateBlock(int x, int y)
    {
        int colorType = Random.Range(0, BLOCK_TYPES);
        Sprite sprite = blockSprites[colorType];

        Block block = Instantiate(blockPrefab, gridParent);
        block.gameObject.SetActive(true);

        RectTransform rectTransform = block.GetComponent<RectTransform>();
        float posX = gridOrigin.x + (x * horizontalSpacing);
        float posY = gridOrigin.y + (y * verticalSpacing);
        rectTransform.anchoredPosition = new Vector2(posX, posY);

        block.Initialize(colorType, x, y, sprite);
        block.SetGrid(this);

        blocks[x, y] = block;
        return block;
    }

    public void OnBlockClicked(Block block)
    {
        Vector2Int pos = block.GetPosition();

        Destroy(block.gameObject);
        blocks[pos.x, pos.y] = null;
    }
}
