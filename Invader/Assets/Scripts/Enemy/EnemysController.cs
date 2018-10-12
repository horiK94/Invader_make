using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Enemy全体のController
/// </summary>
public class EnemysController : MonoBehaviour
{
    /// <summary>
    /// EnemyCrowdControllerへの参照
    /// </summary>
    [SerializeField] 
    private EnemyCrowdController enemyCrowdController = null;

    /// <summary>
    /// UFOControllerへの参照
    /// </summary>
    [SerializeField]
    private UFOController ufoController = null;

    /// <summary>
    /// 初期設定
    /// </summary>
    public void BootUp(UnityAction<int> _onAddScore, UnityAction _onDeath, Vector3 _maxPos, Vector3 _minPos)
    {
        ufoController.BootUp(_onAddScore, _maxPos);
        enemyCrowdController.BootUp(_onAddScore, _onDeath, _maxPos, _minPos);
        ufoController.enabled = true;
        enemyCrowdController.enabled = true;
    }

    /// <summary>
    /// 敵の動きを開始させる
    /// </summary>
    public void MoveStart()
    {
        enemyCrowdController.MoveStart();
    }

    /// <summary>
    /// 敵の動きを止める
    /// </summary>
    public void Stop()
    {
        enemyCrowdController.StopAct();
    }

    /// <summary>
    /// 敵の動きを再開する
    /// </summary>
    public void Restart()
    {
        enemyCrowdController.RestartAct();
    }
}
