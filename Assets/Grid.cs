using UnityEngine;

public class Grid : MonoBehaviour
{
    private const int WIDTH = 6;
    private const int HEIGHT = 5;
    private const int BLOCK_TYPES = 5;

    [SerializeField] private Block blockPrefab;
    [SerializeField] private Sprite[] blockSprites;
    [SerializeField] private Transform gridParent;

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
        block.transform.position = new Vector3(x, y, 0);
        block.Initialize(colorType, x, y, sprite);

        blocks[x, y] = block;
        return block;
    }
}
