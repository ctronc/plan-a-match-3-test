using System.Collections.Generic;
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
        List<Block> connectedBlocks = FindConnectedBlocks(pos.x, pos.y);

        CollectBlocks(connectedBlocks);
    }

    private List<Block> FindConnectedBlocks(int startX, int startY)
    {
        List<Block> connected = new List<Block>();
        Block startBlock = blocks[startX, startY];

        if (startBlock == null) return connected;

        int targetColor = startBlock.GetColorType();
        bool[,] visited = new bool[WIDTH, HEIGHT];
        Queue<Vector2Int> toCheck = new Queue<Vector2Int>();

        toCheck.Enqueue(new Vector2Int(startX, startY));
        visited[startX, startY] = true;

        while (toCheck.Count > 0)
        {
            Vector2Int current = toCheck.Dequeue();
            Block currentBlock = blocks[current.x, current.y];

            if (currentBlock != null && currentBlock.GetColorType() == targetColor)
            {
                connected.Add(currentBlock);

                // Check all 4 directions (up, down, left, right)
                CheckNeighbor(current.x + 1, current.y, targetColor, visited, toCheck);
                CheckNeighbor(current.x - 1, current.y, targetColor, visited, toCheck);
                CheckNeighbor(current.x, current.y + 1, targetColor, visited, toCheck);
                CheckNeighbor(current.x, current.y - 1, targetColor, visited, toCheck);
            }
        }

        return connected;
    }

    private void CheckNeighbor(int x, int y, int targetColor, bool[,] visited, Queue<Vector2Int> toCheck)
    {
        if (x >= 0 && x < WIDTH && y >= 0 && y < HEIGHT && !visited[x, y])
        {
            Block neighbor = blocks[x, y];
            if (neighbor != null && neighbor.GetColorType() == targetColor)
            {
                visited[x, y] = true;
                toCheck.Enqueue(new Vector2Int(x, y));
            }
        }
    }

    private void CollectBlocks(List<Block> blocksToCollect)
    {
        foreach (Block block in blocksToCollect)
        {
            Vector2Int pos = block.GetPosition();
            Destroy(block.gameObject);
            blocks[pos.x, pos.y] = null;
        }
    }
}
