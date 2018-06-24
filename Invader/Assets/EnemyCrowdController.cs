using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class EnemyCrowdController : MonoBehaviour {
    private Transform enemyParent = null;
    
    /// <summary>
    /// Enemyの列数
    /// </summary>
    [SerializeField] private int enemyWidthNum = 11;
    /// <summary>
    /// Enemyの段数
    /// </summary>
    [SerializeField] private int enemyHeightNum = 5;
    /// <summary>
    /// Enemyの列ごとのプレファブ情報
    /// </summary>
    [SerializeField] private EnemyLineInfo[] enemyLineInfo;
    /// <summary>
    /// ufoを出現させるのに必要なEnemyの最低の数
    /// </summary>
    [SerializeField] private int ufoPopMinEnemyNum = 8;
    /// <summary>
    /// スタート前の停止時間
    /// </summary>
    [SerializeField] private float startWaitTime = 2.5f;
    /// <summary>
    /// Enemyの攻撃間隔
    /// </summary>
    [SerializeField] private float shotInterval = 1;
    /// <summary>
    /// Enemyの一番上の段のy座標と画面上のy座標の差
    /// </summary>
    [SerializeField]private float enemyTopPosYDiff;
    /// <summary>
    /// Enemyの一番下の段のy座標と画面下のy座標の差
    /// </summary>
    [SerializeField] private float enemyBottomPosYDiff;
    /// <summary>
    /// Enemyの横の間隔(基本、8移動でそこまで移動)
    /// </summary>
    [SerializeField] private float enemyWidthInterval;
    /// <summary>
    /// 段数の数
    /// </summary>
    [SerializeField]int stageNum = 19;

    /// <summary>
    /// １列移動するのに待つ時間
    /// </summary>
    [SerializeField] private float moveColumnWaitTime = 3.0f;
    
    private List<EnemyColumnController> enemyColumns;
    private int remainColumn = 0;
    private UnityAction<int> onAddScore = null;
    private UnityAction onDeath = null;
    private UnityAction onBelowUfoPopMinEnemyNum = null;
    private Vector3 maxPos = Vector3.zero, minPos = Vector3.zero;
    private float enemyHeightInterval;

    public void BootUp(UnityAction<int> _onAddScore, UnityAction _onDeath, UnityAction _onBelowUfoPopMinEnemyNum, Vector3 _maxPos, Vector3 _minPos)
    {   
        this.onAddScore = _onAddScore;
        this.onDeath = _onDeath;
        this.onBelowUfoPopMinEnemyNum = _onBelowUfoPopMinEnemyNum;
        this.maxPos = _maxPos;
        this.minPos = _minPos;
        
        remainColumn = enemyWidthNum;
        enemyColumns = new List<EnemyColumnController>(remainColumn);
        
        SortEnemyPrefab();
        Create();
    }

    void SortEnemyPrefab()
    {
        Array.Sort(enemyLineInfo, (x, y) => { return x.HighestLine - y.HighestLine; });
        if (enemyLineInfo[enemyLineInfo.Length - 1].HighestLine != enemyHeightNum)
        {
            Debug.LogError("enemyLineInfo[enemyLineInfo.Length - 1].HighestLine != enemyHeightNumとなっています");
        }
    }

    private void Create()
    {
        enemyParent = new GameObject("Enemys").transform; 

        for (int i = 0; i < enemyWidthNum; i++)
        {
            //列の生成
            int lineNumber = i + 1;
            GameObject column = new GameObject("Enemy" + lineNumber + "ColumnController");
            EnemyColumnController controller = column.AddComponent<EnemyColumnController>();
            
            column.transform.parent = transform;
            
            EnemyColumnCreateInfo columnInfo = new EnemyColumnCreateInfo();
            columnInfo.columnId = lineNumber;
            columnInfo.enemyHeightNum = enemyHeightNum;
            columnInfo.startUpStageId = stageNum;        //TODO ステージ数によって変化させる
            columnInfo.enemyWidthInterval = enemyWidthInterval;
            columnInfo.stageNum = stageNum;
            columnInfo.enemyMinPos = new Vector3(minPos.x, minPos.y + enemyBottomPosYDiff, 0);
            columnInfo.enemyMaxPos = new Vector3(maxPos.x, maxPos.y - enemyTopPosYDiff, 0);

            columnInfo.moveWaitTime = moveColumnWaitTime / enemyHeightNum;
            
            controller.Create(columnInfo, enemyLineInfo, enemyParent, onAddScore, () =>
                {
                    remainColumn--;
                    if (remainColumn < ufoPopMinEnemyNum)
                    {
                        onBelowUfoPopMinEnemyNum();
                    }
                    if (remainColumn <= 0)
                    {
                        onDeath();
                    }
                });
            StartCoroutine(Move());
            StartCoroutine(Shot());
        }
    }
    

    IEnumerator Move()
    {
        // TODO 残り敵数に応じて速さが変わるように修正する
        for (int i = 0; i < enemyColumns.Count; i++)
        {
            enemyColumns[i].Move();
            yield return new WaitForSeconds(moveColumnWaitTime);
        }
    }

    IEnumerator Shot()
    {
        yield return new WaitForSeconds(startWaitTime);
        int r = Random.Range(0, enemyColumns.Count - 1);
        //enemyColumns[r].Shot();
        yield return new WaitForSeconds(shotInterval);
    }
}
