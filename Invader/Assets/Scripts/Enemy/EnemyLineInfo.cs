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
    /// <summary>
    /// enemyのプレファブ
    /// </summary>
    [SerializeField]
    private GameObject prefab = null;
    public GameObject Prefab => prefab;

    /// <summary>
    /// プレファブのenemyがいる最高段
    /// </summary>
    [SerializeField] 
    private int highestLine = 0;
    public int HighestLine => highestLine;

    /// <summary>
    /// enemyを倒した時の点数
    /// </summary>
    [SerializeField]
    private int point = 0;
    public int Point => point;
}
