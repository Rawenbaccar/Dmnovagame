using UnityEngine;
using System.Collections.Generic;

public class InfiniteGroundManager : MonoBehaviour
{
    public GameObject groundPrefab;  // Le prefab du sol
    public Transform parentTransform;  // Conteneur des tuiles
    public Transform player;  // Le joueur
    public float tileSize = 30f;  // Taille d'une tuile
    public int poolSize = 20; // Nombre de tuiles dans le pool

    private Vector2 currentTile;  // Tuile actuelle où se trouve le joueur
    private Dictionary<Vector2, GameObject> spawnedTiles = new Dictionary<Vector2, GameObject>();
    private Queue<GameObject> tilePool = new Queue<GameObject>(); // Pool d'objets pour réutiliser les tuiles

    void Start()
    {
        if (parentTransform == null)
        {
            parentTransform = new GameObject("GroundContainer").transform;
        }

        currentTile = GetTilePosition(player.position);
        PreloadTiles();

        // Générer les tuiles autour du joueur (sans la tuile centrale)
        GenerateGround(currentTile);
    }

    void Update()
    {
        Vector2 newTile = GetTilePosition(player.position);

        // Si la position du joueur a changé (il a bougé)
        if (newTile != currentTile)
        {
            currentTile = newTile;
            GenerateGround(newTile);  // Générer les nouvelles tuiles autour du joueur
            CleanUpTiles();
        }
    }

    Vector2 GetTilePosition(Vector3 position)
    {
        return new Vector2(
            Mathf.Floor(position.x / tileSize),
            Mathf.Floor(position.y / tileSize)
        );
    }

    void PreloadTiles()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject newTile = Instantiate(groundPrefab, Vector3.zero, Quaternion.identity, parentTransform);
            newTile.SetActive(false); // Désactiver les tuiles inutilisées
            tilePool.Enqueue(newTile);
        }
    }

    void GenerateGround(Vector2 centerTile)
    {
        // Générer la tuile à gauche
        Vector2 leftTile = centerTile + Vector2.left;  // Déplacement à gauche
        CreateTileAtPosition(leftTile);

        // Générer la tuile à droite
        Vector2 rightTile = centerTile + Vector2.right;  // Déplacement à droite
        CreateTileAtPosition(rightTile);

        // Générer la tuile en haut
        Vector2 upTile = centerTile + Vector2.up;  // Déplacement vers le haut
        CreateTileAtPosition(upTile);

        // Générer la tuile en bas
        Vector2 downTile = centerTile + Vector2.down;  // Déplacement vers le bas
        CreateTileAtPosition(downTile);
    }

    void CreateTileAtPosition(Vector2 tilePos)
    {
        // Vérifier si la tuile existe déjà
        if (!spawnedTiles.ContainsKey(tilePos))
        {
            Vector3 worldPos = new Vector3(tilePos.x * tileSize, tilePos.y * tileSize, 0);
            GameObject newTile = GetTileFromPool();  // Récupérer une tuile du pool
            newTile.transform.position = worldPos;
            newTile.SetActive(true);
            spawnedTiles[tilePos] = newTile;  // Ajouter la tuile à la liste des tuiles générées
        }
    }

    void CleanUpTiles()
    {
        List<Vector2> tilesToRemove = new List<Vector2>();

        foreach (var tile in spawnedTiles)
        {
            float distance = Vector2.Distance(tile.Key, currentTile);
            if (distance > 1)  // Si la tuile est trop loin du joueur (selon la vue)
            {
                tilesToRemove.Add(tile.Key);
            }
        }

        foreach (var tilePos in tilesToRemove)
        {
            GameObject tile = spawnedTiles[tilePos];
            tile.SetActive(false); // Désactiver la tuile au lieu de la détruire
            tilePool.Enqueue(tile); // La remettre dans le pool
            spawnedTiles.Remove(tilePos);
        }
    }

    GameObject GetTileFromPool()
    {
        if (tilePool.Count > 0)
        {
            return tilePool.Dequeue();
        }
        return Instantiate(groundPrefab, Vector3.zero, Quaternion.identity, parentTransform);
    }
}
