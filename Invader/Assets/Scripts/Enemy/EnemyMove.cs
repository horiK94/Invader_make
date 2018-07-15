using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 移動に関するクラス
/// </summary>
public class EnemyMove : MonoBehaviour {
    /// <summary>
    /// Enemyの移動
    /// </summary>
    public void Move(Vector3 moveVec)
    {
        transform.position += moveVec;
    }
}
