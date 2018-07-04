using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRowCreateInfo : MonoBehaviour {
    /// <summary>
    /// 行ID
    /// </summary>
    public int rowId;
    /// <summary>
    /// 行に相当するprefab
    /// </summary>
    public EnemyLineInfo rowInfo;
    /// <summary>
    /// 一行のEnemyの数
    /// </summary>
    public int enemyWidthNum;
    /// <summary>
    /// Enemyの軍団の初期高さ
    /// </summary>
    public int enemyHeightNum;
    /// <summary>
    /// スタート時の一番上の段ID
    /// </summary>
    public int startUpStageId;
    /// <summary>
    /// 敵と敵の幅
    /// </summary>
    public float enemyWidthInterval;
    /// <summary>
    /// 全段数の数
    /// </summary>
    public int stageNum;
    /// <summary>
    /// 敵の可動域の左下の座標
    /// </summary>
    public Vector3 enemyMinPos;
    /// <summary>
    /// 敵の可動域の右上の座標
    /// </summary>
    public Vector3 enemyMaxPos;
}
