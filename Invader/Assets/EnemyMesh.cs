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
    private int count = 0;

    void Awake()
    {
        meshFilter = GetComponentInChildren<MeshFilter>();
        meshLength = mesh.Length;
    }

    public void Change()
    {
        count++;
        meshFilter.mesh = mesh[count % meshLength];
    }
}
