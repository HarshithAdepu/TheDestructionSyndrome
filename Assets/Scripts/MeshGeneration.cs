using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGeneration : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;
    [SerializeField] int meshUnit;
    [SerializeField] int xSize;
    [SerializeField] int ySize;
    void Start()
    {
        mesh = new Mesh();
        vertices = new Vector3[((int)xSize + 1) * ((int)ySize + 1)];
        triangles = new int[6 * (int)xSize * (int)ySize];
        GetComponent<MeshFilter>().mesh = mesh;
        GenerateMesh();
        UpdateMesh();
    }
    void GenerateMesh()
    {
        for(int i=0, y = 0; y<=xSize;y++)
        {
            for(int x=0;x<=ySize;x++)
            {
                vertices[i] = new Vector3(x * meshUnit, y * meshUnit, 0);
                i++;
            }
        }
        for (int currentVertex = 0, tris = 0, y = 0; y < xSize; y++)
        {
            for (int x = 0; x < ySize; x++)
            {
                triangles[tris + 0] = currentVertex;
                triangles[tris + 1] = currentVertex + (int)xSize + 1;
                triangles[tris + 2] = currentVertex + 1;
                triangles[tris + 3] = currentVertex + 1;
                triangles[tris + 4] = currentVertex + +(int)xSize + 1;
                triangles[tris + 5] = currentVertex + +(int)xSize + 2;

                currentVertex++;
                tris += 6;
            }
            currentVertex++;
        }
    }
    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }
}
