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
    [SerializeField] 
    private PlayerController playerController = null;
    /// <summary>
    /// EnemyControllerの参照
    /// </summary>
    [SerializeField] 
    private EnemysController enemysController = null;
    /// <summary>
    /// 移動制限の幅
    /// </summary>
    [SerializeField] 
    private float limitWidth = 0;
    /// <summary>
    /// UIControllerの参照
    /// </summary>
    [SerializeField] 
    private UIController uiController = null;
    
    /// <summary>
    /// 死んだあとに復活するのにかかる時間
    /// </summary>
    [SerializeField]
    private float waitTimeForRevival = 3.0f;

    /// <summary>
    /// ステージ番号
    /// </summary>
    private int stage = 1;

    /// <summary>
    /// 移動可能範囲の左下の座標
    /// </summary>
    private Vector3 minPos = Vector3.zero;

    /// <summary>
    /// 移動可能範囲の右上の座標
    /// </summary>
    private Vector3 maxPos = Vector3.zero;

    void Awake()
    {
        Vector3 leftDownPos = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, transform.position.z - Camera.main.transform.position.z));        //左下の座標
        Vector3 rightUpPos = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, transform.position.z - Camera.main.transform.position.z));        //右上の座標
        
        //Player, Enemyの移動可能範囲を設定
        maxPos = rightUpPos - new Vector3(limitWidth, 0, 0);
        minPos = leftDownPos + new Vector3(limitWidth, 0, 0);

        ScoreManager scoreManager = ScoreManager.Instance;
        uiController.SetScore(scoreManager.GetScore);
        uiController.SetHighScore(scoreManager.GetHighScore);
    }

    private void Start()
    {
        Initialize();
        AppearAtStart();
    }

    //初期化設定
    void Initialize()
    {
        //Enemyが全滅した時の処理
        UnityAction OnDeathAll = () =>
        {
            stage++;
            SceneProcessManager.Instance.LoadNextScene();
        };

        //PlayerとEnemyを初期化
        enemysController.BootUp((addScore) =>
        {
            ScoreManager scoreManager = ScoreManager.Instance;
            scoreManager.AddScore(addScore);
            uiController.SetScore(scoreManager.GetScore);
        }, OnDeathAll, maxPos, minPos);
        playerController.BootUp(maxPos, minPos, waitTimeForRevival, (hp) =>
        {
            //hpを設定する
            uiController.SetHeart(hp);

            if(playerController.RemainHp > 0)
            {
                //enemyの動きを止める
                enemysController.Stop();
                //一定時間たったら、動きを再開する
                StartCoroutine(WaitTime(waitTimeForRevival, () =>
                {
                    enemysController.Restart();
                }));
            }
        }, () =>
        {
            ScoreManager.Instance.SetHighScore();
            enemysController.Stop();
            uiController.AppearGameOver();
        });
    }

    /// <summary>
    /// スタート後に表示すべきオブジェクトをactiveにする
    /// </summary>
    void AppearAtStart()
    {
        enemysController.MoveStart();
        playerController.MoveStart();
    }

    /// <summary>
    /// waitTimeForRevivalの時間だけ待つ
    /// </summary>
    /// <returns>The time.</returns>
    /// <param name="callback">Callback.</param>
    IEnumerator WaitTime(float waitTime, UnityAction callback = null)
    {
        yield return new WaitForSeconds(waitTime);
        callback();
    }
}
