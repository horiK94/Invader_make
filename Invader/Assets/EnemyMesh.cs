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
    private int meshLength = 0;
    public int MeshLength => meshLength;

    void Awake()
    {
        meshFilter = GetComponentInChildren<MeshFilter>();
        meshLength = mesh.Length;
    }

    public void ChangeMesh(int meshId)
    {
        if (meshId < 0 || meshId >= MeshLength)
        {
            Debug.LogError("meshIdが無効な値です");
        }
        meshFilter.mesh = mesh[meshId];
    }
}
