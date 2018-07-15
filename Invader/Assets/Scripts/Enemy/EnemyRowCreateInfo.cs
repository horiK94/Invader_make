using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 行ごとのEnemyに関する情報
/// </summary>
public class EnemyRowCreateInfo : MonoBehaviour {
    /// <summary>
    /// 行ID
    /// </summary>
    public int rowId = 0;
    /// <summary>
    /// 行に相当するprefab
    /// </summary>
    public EnemyLineInfo rowInfo = null;
    /// <summary>
    /// 一行のEnemyの数
    /// </summary>
    public int enemyWidthNum = 0;
    /// <summary>
    /// Enemyの軍団の初期高さ
    /// </summary>
    public int enemyHeightNum = 0;
    /// <summary>
    /// スタート時の一番上の段ID
    /// </summary>
    public int startUpStageId = 0;
    /// <summary>
    /// 敵と敵の幅
    /// </summary>
    public float enemyWidthInterval = 0;
    /// <summary>
    /// 全段数の数
    /// </summary>
    public int stageNum = 0;
    /// <summary>
    /// 敵の可動域の左下の座標
    /// </summary>
    public Vector3 enemyMinPos = Vector3.zero;
    /// <summary>
    /// 敵の可動域の右上の座標
    /// </summary>
    public Vector3 enemyMaxPos = Vector3.zero;
}
