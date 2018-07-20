using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Gameの全体的な処理や連携を行う
/// </summary>
public class GameController : MonoBehaviour
{
    /// <summary>
    /// PlayerControllerの参照
    /// </summary>
    [SerializeField] private PlayerController playerController = null;
    /// <summary>
    /// EnemyControllerの参照
    /// </summary>
    [SerializeField] private EnemysController enemysController = null;
    /// <summary>
    /// 移動制限の幅
    /// </summary>
    [SerializeField] private float limitWidth = 0;
    /// <summary>
    /// UIControllerの参照
    /// </summary>
    [SerializeField] private UIController uiController = null;

    /// <summary>
    /// 現在のスコア
    /// </summary>
    private int score = 0;

    /// <summary>
    /// ステージ番号
    /// </summary>
    private int stage = 1;
    
    // TODO UIControllerの追加
    private Vector3 minPos = Vector3.zero, maxPos = Vector3.zero;

    void Awake()
    {
        Vector3 leftDownPos = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, transform.position.z - Camera.main.transform.position.z));        //左下の座標
        Vector3 rightUpPos = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, transform.position.z - Camera.main.transform.position.z));        //右上の座標
        
        //Player, Enemyの移動可能範囲を設定
        maxPos = rightUpPos - new Vector3(limitWidth, 0, 0);        //移動可能範囲の右上の座標
        minPos = leftDownPos + new Vector3(limitWidth, 0, 0);        //移動可能範囲の左下の座標
        
        //scoreの初期化
        score = 0;
        uiController.SetScore(score);
    }

    private void Start()
    {
        //Enemyを倒した時の処理
        UnityAction<int> OnAddScore = (score) =>
        {
            this.score += score;
            uiController.SetScore(this.score);
        };
        //Enemyが全滅した時の処理
        UnityAction OnDeathAll = () =>
        {
            stage++;
            //TODO Sceneの切り替え
            //TODO UIの表示
        };
        
        //PlayerとEnemyを初期化
        enemysController.BootUp(OnAddScore, OnDeathAll, maxPos, minPos);
        playerController.BootUp(maxPos, minPos, () =>
        {
            //TODO GAMEOVERと表示
            
        });
        
        //Playerを移動できるようにする
        playerController.enabled = true;
    }
}
