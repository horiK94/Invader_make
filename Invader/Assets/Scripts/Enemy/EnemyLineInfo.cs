using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.AccessControl;
using UnityEngine;
using Object = System.Object;

[Serializable]
public class EnemyLineInfo
{
    [SerializeField] private GameObject prefab;
    public GameObject Prefab => prefab;

    [SerializeField] private int highestLine; // prefabのenemyがいる最高段
    public int HighestLine => highestLine;

    [SerializeField] private int point;
    public int Point => point;
}
