using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMesh : MonoBehaviour
{
    /// <summary>
    /// Enemyが動いているように見せるMesh
    /// </summary>
    [SerializeField]
    private Mesh[] mesh = null;
    /// <summary>
    /// enemyのMeshFilter
    /// </summary>
    private MeshFilter meshFilter = null;
    /// <summary>
    /// 設定したMeshの数
    /// </summary>
    public int MeshLength => mesh.Length;
    /// <summary>
    /// Meshを変更した回数
    /// </summary>
    private int count = 0;

    void Awake()
    {
        meshFilter = GetComponentInChildren<MeshFilter>();
        if (MeshLength != 0)
        {
            meshFilter.mesh = mesh[0];
        }
    }

    /// <summary>
    /// meshの切り替え
    /// </summary>
    public void ChangeMesh()
    {
        if (MeshLength == 0)
        {
            return;
        }
        meshFilter.mesh = mesh[++count % MeshLength];
    }
}
