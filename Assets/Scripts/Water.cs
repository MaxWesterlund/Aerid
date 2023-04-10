using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water {
    public Water(int size, float level, Material material) {
        GenerateMesh(size, level, material);
    }

    void GenerateMesh(float size, float level, Material material) {
        Mesh mesh = new();
        mesh.vertices = new Vector3[] {
            new(0, level, size),
            new(size, level, size),
            new(0, level, 0),
            new(size, level, 0),
            new(0, level, 0),
            new(size, level, size)
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
