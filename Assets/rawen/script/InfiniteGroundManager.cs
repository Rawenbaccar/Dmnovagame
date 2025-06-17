using UnityEngine;
using System.Collections.Generic;

public class InfiniteGroundManager : MonoBehaviour
{
    public ExperienceLevelController ELC;
    public Sprite[] BGs;
    public GameObject groundPrefab;  // Prefab du sol
    public Transform player;         // Référence au joueur
    public int gridSize = 3;         // Taille de la grille (3x3)

    private float tileWidth;
    private float tileHeight;

    private Vector2Int currentPlayerTile;
    private Dictionary<Vector2Int, GameObject> spawnedTiles = new Dictionary<Vector2Int, GameObject>();

    void Start()
    {
        Renderer renderer = groundPrefab.GetComponent<Renderer>();
        if (renderer != null)
        {
            tileWidth = renderer.bounds.size.x;
            tileHeight = renderer.bounds.size.y;
        }
        else
        {
            Debug.LogError("Ground prefab does not have a Renderer component.");
            return;
        }

        currentPlayerTile = GetTileCoords(player.position);
        SpawnInitialGrid();
    }

    void Update()
    {
        int spriteIndex = ELC.currentLevel / 3;

        if (ELC.currentLevel < 10 && spriteIndex < BGs.Length)
        {
            foreach (var tile in spawnedTiles.Values)
            {
                SpriteRenderer sr = tile.GetComponent<SpriteRenderer>();
                if (sr != null && sr.sprite != BGs[spriteIndex])
                {
                    sr.sprite = BGs[spriteIndex];
                }
            }
        }

        Vector2Int newPlayerTile = GetTileCoords(player.position);
        if (newPlayerTile != currentPlayerTile)
        {
            currentPlayerTile = newPlayerTile;
            UpdateGrid();
        }
    }


    Vector2Int GetTileCoords(Vector3 position)
    {
        int x = Mathf.FloorToInt(position.x / tileWidth);
        int y = Mathf.FloorToInt(position.y / tileHeight);
        return new Vector2Int(x, y);
    }

    void SpawnInitialGrid()
    {
        for (int x = -gridSize / 2; x <= gridSize / 2; x++)
        {
            for (int y = -gridSize / 2; y <= gridSize / 2; y++)
            {
                Vector2Int tileCoord = new Vector2Int(currentPlayerTile.x + x, currentPlayerTile.y + y);
                SpawnTileAt(tileCoord);
            }
        }
    }

    void UpdateGrid()
    {
        // Tiles to keep (3x3 around currentPlayerTile)
        HashSet<Vector2Int> newTiles = new HashSet<Vector2Int>();

        for (int x = -gridSize / 2; x <= gridSize / 2; x++)
        {
            for (int y = -gridSize / 2; y <= gridSize / 2; y++)
            {
                Vector2Int tileCoord = new Vector2Int(currentPlayerTile.x + x, currentPlayerTile.y + y);
                newTiles.Add(tileCoord);
                if (!spawnedTiles.ContainsKey(tileCoord))
                {
                    SpawnTileAt(tileCoord);
                }
            }
        }

        // Remove tiles that are not in the newTiles set
        List<Vector2Int> tilesToRemove = new List<Vector2Int>();
        foreach (var tile in spawnedTiles.Keys)
        {
            if (!newTiles.Contains(tile))
                tilesToRemove.Add(tile);
        }

        foreach (var tileCoord in tilesToRemove)
        {
            Destroy(spawnedTiles[tileCoord]);
            spawnedTiles.Remove(tileCoord);
        }
    }

    void SpawnTileAt(Vector2Int tileCoord)
    {
        Vector3 spawnPos = new Vector3(tileCoord.x * tileWidth, tileCoord.y * tileHeight, 0);
        GameObject tile = Instantiate(groundPrefab, spawnPos, Quaternion.identity);
        spawnedTiles.Add(tileCoord, tile);
    }
}
