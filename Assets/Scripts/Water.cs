using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water {
    public Water(int size, int additionalSize, float level, Material material) {
        GenerateMesh(size, additionalSize, level, material);
    }

    void GenerateMesh(float size, int additionalSize, float level, Material material) {
        Mesh mesh = new();
        mesh.vertices = new Vector3[] {
            new(-additionalSize, level, additionalSize + size),
            new(additionalSize + size, level, additionalSize + size),
            new(-additionalSize, level, -additionalSize),
            new(additionalSize + size, level, -additionalSize),
            new(-additionalSize, level, -additionalSize),
            new(additionalSize + size, level, additionalSize + size)
        };
        mesh.triangles = new int[] {0, 1, 2, 3, 2, 1};

        mesh.RecalculateNormals();

        GameObject obj = new("Water");

        MeshRenderer mr = obj.AddComponent<MeshRenderer>();
        mr.material = material;

        MeshFilter mf = obj.AddComponent<MeshFilter>();
        mf.mesh = mesh;
    }
}
