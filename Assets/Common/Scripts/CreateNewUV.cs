using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateNewUV : MonoBehaviour {
    public float offSetX;
    void Awake()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        Vector2[] uvs = new Vector2[vertices.Length];
        uvs = mesh.uv;

        for (int i = 0; i < uvs.Length; i++)
        {
            uvs[i] = new Vector2(uvs[i].x + offSetX, uvs[i].y);
        }
        mesh.uv = uvs;
    }
}
