using System.Collections.Generic;
using System;
using UnityEngine;

public class TerrainGen : MonoBehaviour
{
    [SerializeField] float scale;
    [SerializeField] [Range(1, 20)] int mapSize;
    [SerializeField] float heightMultiplier;
    [SerializeField] AnimationCurve heightCurve;
    [SerializeField] AnimationCurve falloffcurve;
    [SerializeField] float waterLevel;
    [SerializeField] Material terrainMaterial;
    [SerializeField] Material waterMaterial;
    
    const int chunkSize = 64;

    int totalSize;

    float[,] noiseMap;
    Tile[,] tileGrid;
    Chunk[,] chunkGrid;

    public event Action GenerationFinished;

    void Start() {
        totalSize = chunkSize * mapSize;
        
        GenerateSpawn();
        GenerateMap();
        GenerateChunks();

        Water water = new Water(totalSize, waterLevel * heightMultiplier, waterMaterial);

        GenerationFinished?.Invoke();
    }

    void GenerateSpawn() {
        GameObject obj = new("Spawn");
        obj.transform.position = new Vector3(totalSize / 2, heightMultiplier + 10, totalSize / 2);
    }

    void GenerateChunks() {
        chunkGrid = new Chunk[mapSize, mapSize];

        int index = 0;
        for (int x = 0; x < mapSize; x++) {
            for (int y = 0; y < mapSize; y++) {        
                Chunk chunk = new Chunk(index, GenerateMesh(x, y, tileGrid), terrainMaterial, transform);
                chunkGrid[x, y] = chunk;
                index++;
            }
        }
            
    }

    void GenerateMap() {
        noiseMap = new float[totalSize, totalSize];
        Vector2 seed = new(UnityEngine.Random.Range(-1000f, 1000f), UnityEngine.Random.Range(-1000f, 1000f));
        float maxDistance = totalSize / 2;
        for (int x = 0; x < totalSize; x++) {
            float xDistance = Mathf.Abs(maxDistance - x);
            for (int y = 0; y < totalSize; y++) {
                float yDistance = Mathf.Abs(maxDistance - y);

                float distance = Mathf.Sqrt(xDistance * xDistance + yDistance * yDistance);
                
                float f = 0; 

                if (distance > maxDistance) {
                    f = 0;
                }
                else {
                    f = falloffcurve.Evaluate((maxDistance - distance) / maxDistance);
                }

                float n = Mathf.PerlinNoise(x * scale + seed.x, y * scale + seed.y) +
                          .5f * Mathf.PerlinNoise(x * 2 * scale + seed.x, y * 2 * scale + seed.y) +
                          .25f * Mathf.PerlinNoise(x * 4 * scale + seed.x, y * 4 * scale + seed.y);
                n /= 1.75f;
                n *= f;
                noiseMap[x, y] = n;
            }
        }

        tileGrid = new Tile[totalSize, totalSize];
        for (int x = 0; x < totalSize; x++) {
            for (int y = 0; y < totalSize; y++) {
                float value = noiseMap[x, y];
                Tile tile = new Tile(value, new(x, y), heightMultiplier, heightCurve);
                tileGrid[x, y] = tile;
            }
        }
    }

    Mesh GenerateMesh(int xS, int yS, Tile[,] grid) {
        Mesh mesh = new();
        List<Vector3> vertices = new();
        List<Vector2> uvs = new();
        List<int> triangles = new();

        for (int x = xS * chunkSize; x < xS * chunkSize + chunkSize - (xS >= mapSize - 1? 1 : 0); x++) {
            for (int y = yS * chunkSize; y < yS * chunkSize + chunkSize - (yS >= mapSize - 1? 1 : 0); y++) {
                Vector3 vA = new(x, grid[x, y + 1].Height, y + 1);
                Vector3 vB = new(x + 1, grid[x + 1, y + 1].Height, y + 1);
                Vector3 vC = new(x, grid[x, y].Height, y);
                Vector3 vD = new(x + 1, grid[x + 1, y].Height, y);

                Vector3[] v = new[] { vA, vB, vC, vB, vD, vC };

                for (int i = 0; i < 6; i++) {
                    vertices.Add(v[i]);
                    triangles.Add(triangles.Count);
                }
            }
        }

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.RecalculateNormals();

        return mesh;
    }
}

public struct Tile {
    public float Value;
    public float Height;
    public Vector2Int Position;
    public Tile(float value, Vector2Int position, float heightMultiplier, AnimationCurve heightCurve) {
        Value = value;
        Height = Mathf.Round(value * heightMultiplier * heightCurve.Evaluate(Value));
        Position = position;
    }
}

public class Chunk {
    GameObject mainObj;
    Material mat;
    Texture tex;
    public Chunk(int index, Mesh mesh, Material material, Transform mainObj) {
        mat = material;

        GameObject obj = new("Chunk " + index);
        obj.transform.parent = mainObj;

        MeshRenderer mr = obj.AddComponent<MeshRenderer>();
        mr.material = material;

        MeshFilter mf = obj.AddComponent<MeshFilter>();
        mf.mesh = mesh;

        obj.AddComponent<MeshCollider>();

        obj.layer = 6;
    }
}