// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Chunk
// {
//     TerrainGeneration gen;

//     const int chunkSize = 64;
//     int myIndex;
//     Vector3 myPos;
//     Cell[,] globalGrid;
//     Cell[,] myGrid;

//     public Chunk(Cell[,] grid, Vector2Int start, int index, TerrainGeneration main) {
//         globalGrid = grid;
//         myPos = new(start.x, 0, start.y);
//         myIndex = index;
//         gen = main;

//         myGrid = new Cell[chunkSize, chunkSize];
//         for (int x = 0; x < chunkSize; x+a+) {
//             for (int y = 0; y < chunkSize; y++) {
//                 myGrid[x, y] = grid[start.x + x, start.y + y];
//             }
//         }

//         GenerateMesh(myGrid);
//         GenerateEdges(myGrid);
//         GenerateTexture(myGrid);
//     }

//     const int groundLayer = 3;

//     GameObject obj;
//     GameObject eObj;

//     void GenerateMesh(Cell[,] grid) {
//         obj = new GameObject($"Chunk {myIndex}");
//         obj.transform.position = myPos;

//         Mesh mesh = new();
//         mesh.Clear();
        
//         List<Vector3> vertices = new();
//         List<int> triangles = new();
//         List<Vector2> uvs = new();
//         for (int x = 0; x < chunkSize; x++) {
//             for (int y = 0; y < chunkSize; y++) {
//                 Cell cell = grid[x, y];
//                 float h = cell.Height;
//                 Vector3 vA = new(x - .5f, h, y + .5f);
//                 Vector3 vB = new(x + .5f, h, y + .5f);
//                 Vector3 vC = new(x - .5f, h, y - .5f);
//                 Vector3 vD = new(x + .5f, h, y - .5f);
//                 Vector2 uvA = new(x / (float)chunkSize, y / (float)chunkSize);
//                 Vector2 uvB = new((x + 1) / (float)chunkSize, y / (float)chunkSize);
//                 Vector2 uvC = new(x / (float)chunkSize, (y + 1) / (float)chunkSize);
//                 Vector2 uvD = new((x + 1) / (float)chunkSize, (y + 1) / (float)chunkSize);
//                 Vector3[] v = new[] { vA, vB, vC, vB, vD, vC };
//                 Vector2[] uv = new[] { uvA, uvB, uvC, uvB, uvD, uvC };
//                 for (int i = 0; i < 6; i++) {
//                     vertices.Add(v[i]);
//                     triangles.Add(triangles.Count);
//                     uvs.Add(uv[i]);
//                 }
//             }
//         }
//         mesh.vertices = vertices.ToArray();
//         mesh.triangles = triangles.ToArray();
//         mesh.uv = uvs.ToArray();
//         mesh.RecalculateNormals();

//         MeshFilter meshFilter = obj.AddComponent<MeshFilter>();
//         meshFilter.mesh = mesh;

//         MeshRenderer meshRenderer = obj.AddComponent<MeshRenderer>();
        
//         MeshCollider meshCollider = obj.AddComponent<MeshCollider>(); 
//         meshCollider.gameObject.layer = groundLayer;
//     }

//     void GenerateEdges(Cell[,] grid)
//     {
//         eObj = new GameObject("Edges");
//         eObj.transform.position = myPos;
//         eObj.transform.parent = obj.transform;

//         Mesh mesh = new();
//         mesh.Clear();

//         List<Vector3> vertices = new();
//         List<int> triangles = new();
//         List<Vector2> uvs = new();
//         for (int x = 0; x < chunkSize; x++) {
//             for (int y = 0; y < chunkSize; y++) {
//                 if (x + 1 < chunkSize) {
//                     Cell cell = grid[x, y];
//                     Cell right = grid[x + 1, y];
//                     float h = cell.Height;
//                     float rh = right.Height;
//                     Vector3 vA = new(x + .5f, h, y - .5f);
//                     Vector3 vB = new(x + .5f, h, y + .5f);
//                     Vector3 vC = new(x + .5f, rh, y - .5f);
//                     Vector3 vD = new(x + .5f, rh, y + .5f);
//                     Vector2 uvA = new();
//                     Vector2 uvB = new();
//                     Vector2 uvC = new();
//                     Vector2 uvD = new();
//                     if (cell.Value * h < right.Value * rh)
//                     {
//                         uvA = new((x + 1.5f) / (float)chunkSize, (y + .5f) / (float)chunkSize);
//                         uvB = new((x + 1.5f) / (float)chunkSize, (y + .5f) / (float)chunkSize);
//                         uvC = new((x + 1.5f) / (float)chunkSize, (y + .5f) / (float)chunkSize);
//                         uvD = new((x + 1.5f) / (float)chunkSize, (y + .5f) / (float)chunkSize);
//                     }
//                     else
//                     {
//                         uvA = new((x + .5f) / (float)chunkSize, (y + .5f) / (float)chunkSize);
//                         uvB = new((x + .5f) / (float)chunkSize, (y + .5f) / (float)chunkSize);
//                         uvC = new((x + .5f) / (float)chunkSize, (y + .5f) / (float)chunkSize);
//                         uvD = new((x + .5f) / (float)chunkSize, (y + .5f) / (float)chunkSize);
//                     }
//                     Vector3[] v = new[] { vA, vB, vC, vB, vD, vC };
//                     Vector2[] uv = new[] { uvA, uvB, uvC, uvB, uvD, uvC };
//                     for (int i = 0; i < 6; i++) {
//                         vertices.Add(v[i]);
//                         triangles.Add(triangles.Count);
//                         uvs.Add(uv[i]);
//                     }
//                 }
//                 if (y > 0) {
//                     Cell cell = grid[x, y];
//                     Cell down = grid[x, y - 1];
//                     float h = cell.Height;
//                     float dh = down.Height;
//                     Vector3 vA = new(x - .5f, h, y - .5f);
//                     Vector3 vB = new(x + .5f, h, y - .5f);
//                     Vector3 vC = new(x - .5f, dh, y - .5f);
//                     Vector3 vD = new(x + .5f, dh, y - .5f);
//                     Vector2 uvA = new();
//                     Vector2 uvB = new();
//                     Vector2 uvC = new();
//                     Vector2 uvD = new();
//                     if (cell.Value * h < down.Value * dh)
//                     {
//                         uvA = new((x + .5f) / (float)chunkSize, (y - .5f) / (float)chunkSize);
//                         uvB = new((x + .5f) / (float)chunkSize, (y - .5f) / (float)chunkSize);
//                         uvC = new((x + .5f) / (float)chunkSize, (y - .5f) / (float)chunkSize);
//                         uvD = new((x + .5f) / (float)chunkSize, (y - .5f) / (float)chunkSize);
//                     }
//                     else
//                     {
//                         uvA = new((x + .5f) / (float)chunkSize, (y + .5f) / (float)chunkSize);
//                         uvB = new((x + .5f) / (float)chunkSize, (y + .5f) / (float)chunkSize);
//                         uvC = new((x + .5f) / (float)chunkSize, (y + .5f) / (float)chunkSize);
//                         uvD = new((x + .5f) / (float)chunkSize, (y + .5f) / (float)chunkSize);
//                     }
//                     Vector3[] v = new[] { vA, vB, vC, vB, vD, vC };
//                     Vector2[] uv = new[] { uvA, uvB, uvC, uvB, uvD, uvC };
//                     for (int i = 0; i < 6; i++) {
//                         vertices.Add(v[i]);
//                         triangles.Add(triangles.Count);
//                         uvs.Add(uv[i]);
//                     }
//                 }
//             }
//         }
//         mesh.vertices = vertices.ToArray();
//         mesh.triangles = triangles.ToArray();
//         mesh.uv = uvs.ToArray();
//         mesh.RecalculateNormals();

//         MeshFilter meshFilter = eObj.AddComponent<MeshFilter>();
//         meshFilter.mesh = mesh;

//         MeshRenderer meshRenderer = eObj.AddComponent<MeshRenderer>();
        
//         MeshCollider meshCollider = eObj.AddComponent<MeshCollider>(); 
//         meshCollider.gameObject.layer = groundLayer;
//     }

//     void GenerateTexture(Cell[,] grid)
//     {
//         Texture2D texture = new(chunkSize, chunkSize);
//         Color[] colorMap = new Color[chunkSize * chunkSize];
//         for (int x = 0; x < chunkSize; x++) {
//             for (int y = 0; y < chunkSize; y++) {
//                 Cell cell = grid[x, y];
//                 if (cell.Value <= gen.WaterLevel)
//                     colorMap[y * chunkSize + x] = gen.WaterColor;
//                 else if (cell.Value <= gen.SandLevel)
//                     colorMap[y * chunkSize + x] = gen.SandColor;
//                 else if (cell.Value <= gen.GrassLevel)
//                     colorMap[y * chunkSize + x] = gen.GrassColor;
//                 else if (cell.Value <= gen.RockLevel)
//                     colorMap[y * chunkSize + x] = gen.RockColor;
//                 else
//                     colorMap[y * chunkSize + x] = gen.SnowColor;
//             }
//         }
//         texture.filterMode = FilterMode.Point;
//         texture.SetPixels(colorMap);
//         texture.Apply();
        
//         MeshRenderer meshRenderer = obj.GetComponent<MeshRenderer>();
//         meshRenderer.material = gen.MeshMaterial;
//         meshRenderer.material.mainTexture = texture;

//         meshRenderer = eObj.GetComponent<MeshRenderer>();
//         meshRenderer.material = gen.MeshMaterial;
//         meshRenderer.material.mainTexture = texture;
//     }
// }