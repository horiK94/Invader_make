using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyLineInfo : IComparer
{
    [SerializeField] private GameObject prefab;
    public GameObject Prefab => prefab;

    [SerializeField] private int highestLine; // prefabのenemyがいる最高段
    public int HighestLine => highestLine;

    public int Compare(object x, object y)
    {
        if (x == null && y == null)
        {
            return 0;
        }

        if (x == null)
        {
            return -1;
        }

        if (y == null)
        {
            return 1;
        }

        if (!(x is EnemyLineInfo))
        {
            throw new ArgumentException("EnemyLineInfo型でなければならない", "x");
        }

        if (!(y is EnemyLineInfo))
        {
            throw new ArgumentException("EnemyLineInfo型でなければならない", "y");
        }

        return ((EnemyLineInfo) x).HighestLine - ((EnemyLineInfo) y).HighestLine;
    }
}
