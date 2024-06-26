using UnityEngine;

[System.Serializable]
public class ChunkData
{
    public GameObject[] cubePrefabs;
    public int height;
    public bool isRugged;
    public float ruggedness;
}

public class ChunkGenerator : MonoBehaviour
{
    public ChunkData[] chunks;
    public int chunkSize = 20;
    public float borderThickness = 0.1f;
    public GameObject borderPrefab;
    public float borderHeight = 100f;
    public string GameSeed = "Default";
    public int currentSeed = 0;

    private Transform playerTransform;
    private float noiseOffsetX;
    private float noiseOffsetZ;

    void Start()
    {
        playerTransform = transform;
        currentSeed = GameSeed.GetHashCode();
        Random.InitState(currentSeed);
        noiseOffsetX = Random.Range(0f, 1000f);
        noiseOffsetZ = Random.Range(0f, 1000f);
        GenerateChunks();
        CreateBorders();
        SetStaticOccludable();
    }

    void GenerateChunks()
    {
        int totalHeight = 0;

        for (int i = 0; i < chunks.Length; i++)
        {
            Vector3 chunkPosition = Vector3.up * totalHeight;
            GenerateChunk(chunks[i], chunkPosition, i == chunks.Length - 1);
            totalHeight += chunks[i].height;
        }
    }

    void GenerateChunk(ChunkData chunkData, Vector3 position, bool isLastChunk)
    {
        for (int x = 0; x < chunkSize; x++)
        {
            for (int z = 0; z < chunkSize; z++)
            {
                bool isEdge = !isLastChunk && (x == 0 || x == chunkSize - 1 || z == 0 || z == chunkSize - 1);
                int height = chunkData.height;

                if (chunkData.isRugged && !isEdge)
                {
                    height = Mathf.RoundToInt(chunkData.height * Mathf.PerlinNoise((x * chunkData.ruggedness) + noiseOffsetX, (z * chunkData.ruggedness) + noiseOffsetZ));
                }

                for (int y = 0; y < height; y++)
                {
                    GameObject cubePrefab = chunkData.cubePrefabs[Random.Range(0, chunkData.cubePrefabs.Length)];
                    GameObject cube = Instantiate(cubePrefab, new Vector3(x, y, z) + position, Quaternion.identity);
                    cube.isStatic = true;
                }
            }
        }
    }

    void CreateBorders()
    {
        int totalHeight = 0;
        for (int i = 0; i < chunks.Length; i++)
        {
            totalHeight += chunks[i].height;
        }

        float mapWidth = chunkSize;
        float mapHeight = totalHeight;

        CreateBorder(new Vector3((mapWidth / 2), borderHeight / 2, (-borderThickness / 2) - 1f), new Vector3(mapWidth, borderHeight, borderThickness)); // Front border
        CreateBorder(new Vector3((mapWidth / 2), borderHeight / 2, (chunkSize - borderThickness / 2) + 1f), new Vector3(mapWidth, borderHeight, borderThickness)); // Back border
        CreateBorder(new Vector3((-borderThickness / 2) - 1f, borderHeight / 2, mapWidth / 2), new Vector3(borderThickness, borderHeight, mapWidth)); // Left border
        CreateBorder(new Vector3((chunkSize - borderThickness / 2) + 1f, borderHeight / 2, mapWidth / 2), new Vector3(borderThickness, borderHeight, mapWidth)); // Right border
    }

    void CreateBorder(Vector3 position, Vector3 scale)
    {
        GameObject border = Instantiate(borderPrefab, position, Quaternion.identity);
        border.transform.localScale = scale;
        border.transform.SetParent(transform);
        border.isStatic = true;
    }

    void SetStaticOccludable()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.isStatic = true;
        }
    }

    void Update()
    {

    }
}
