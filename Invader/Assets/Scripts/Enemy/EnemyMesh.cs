using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMesh : MonoBehaviour
{
    /// <summary>
    /// Enemyが動いているように見せるMesh
    /// </summary>
    [SerializeField] private Mesh[] mesh;
    private MeshFilter meshFilter;
    public int MeshLength => mesh.Length;
    private int id = 0;

    void Awake()
    {
        meshFilter = GetComponentInChildren<MeshFilter>();
        if (MeshLength != 0)
        {
            meshFilter.mesh = mesh[0];
        }
    }

    public void ChangeMesh()
    {
        if (MeshLength == 0)
        {
            return;
        }
        meshFilter.mesh = mesh[++id % MeshLength];
    }
}
