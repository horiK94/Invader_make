using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyColumnCreateInfo : MonoBehaviour
{
    /// <summary>
    /// 行ID
    /// </summary>
    public int columnId;
    /// <summary>
    /// Enemyの段数
    /// </summary>
    public int enemyHeightNum = 5;
    /// <summary>
    /// スタート時の一番上の段ID
    /// </summary>
    public int startUpStageId;
    /// <summary>
    /// 敵と敵の幅
    /// </summary>
    public float enemyWidthInterval;
    /// <summary>
    /// 段数の数
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

    /// <summary>
    /// １体を移動させるのにかかる時間
    /// </summary>
    public float moveWaitTime;
}
