using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// enemyの当たり判定に関するクラス
/// </summary>
public class EnemyTrigger : MonoBehaviour
{
    /// <summary>
    /// 当たった際に呼ばれるデリゲートメソッド
    /// </summary>
    private UnityAction<Collider> myTriggerEnter = null;
    
    /// <summary>
    /// デリゲートメソッドの設定
    /// </summary>
    public void SetUp(UnityAction<Collider> myTriggerEnter)
    {
        this.myTriggerEnter = myTriggerEnter;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        this.myTriggerEnter(other);
    }
}
