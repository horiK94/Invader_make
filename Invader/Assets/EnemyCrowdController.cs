using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.Assertions.Must;
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
    /// １行移動するのに待つ時間
    /// </summary>
    [SerializeField] private float moveLineWaitTime = 3.0f;

    private EnemyColumnController[] enemyColumns;
    private int remainColumn = 0;
    private UnityAction<int> onAddScore = null;
    private UnityAction onDeath = null;
    private UnityAction onBelowUfoPopMinEnemyNum = null;
    private Vector3 maxPos = Vector3.zero, minPos = Vector3.zero;
    private float enemyHeightInterval;
    private int minStage, maxStage;
    private int[] rowAliveEnemuNum;

    void Awake()
    {
        minStage = 0;
        maxStage = enemyHeightNum - 1;
    }
    
    public void BootUp(UnityAction<int> _onAddScore, UnityAction _onDeath, UnityAction _onBelowUfoPopMinEnemyNum, Vector3 _maxPos, Vector3 _minPos)
    {   
        this.onAddScore = _onAddScore;
        this.onDeath = _onDeath;
        this.onBelowUfoPopMinEnemyNum = _onBelowUfoPopMinEnemyNum;
        this.maxPos = _maxPos;
        this.minPos = _minPos;
        
        remainColumn = enemyWidthNum;
        
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

        rowAliveEnemuNum = new int[enemyHeightNum];
        for (int i = 0; i < enemyHeightNum; i++)
        {
            rowAliveEnemuNum[i] = enemyWidthNum;
        }

        enemyColumns = new EnemyColumnController[enemyWidthNum];
        for (int i = 0; i < enemyWidthNum; i++)
        {
            //列の生成
            int lineNumber = i + 1;
            GameObject column = new GameObject("Enemy" + lineNumber + "ColumnController");
            EnemyColumnController controller = column.AddComponent<EnemyColumnController>();
            enemyColumns[i] = controller;
            
            column.transform.parent = transform;
            
            EnemyColumnCreateInfo columnInfo = new EnemyColumnCreateInfo();
            columnInfo.columnId = lineNumber;
            columnInfo.enemyHeightNum = enemyHeightNum;
            columnInfo.startUpStageId = stageNum;        //TODO ステージ数によって変化させる
            columnInfo.enemyWidthInterval = enemyWidthInterval;
            columnInfo.stageNum = stageNum;
            columnInfo.enemyMinPos = new Vector3(minPos.x, minPos.y + enemyBottomPosYDiff, 0);
            columnInfo.enemyMaxPos = new Vector3(maxPos.x, maxPos.y - enemyTopPosYDiff, 0);
            
            controller.Create(columnInfo, enemyLineInfo, enemyParent, onAddScore, (id) =>
            {
                rowAliveEnemuNum[id]--;
                if (rowAliveEnemuNum.Sum() == 0)
                {
                    onDeath();
                }

                if (rowAliveEnemuNum[id] == 0)
                {
                    onRowDeath();
                }
            });
        }

        StartCoroutine(Move());
        StartCoroutine(Shot());
    }

    void onRowDeath()
    {
        for (int i = 0; i < enemyWidthNum; i++)
        {
            
        }
    }

    IEnumerator Move()
    {
        yield return new WaitForSeconds(startWaitTime);
        while (enemyColumns.Where(e => e != null).ToArray().Length != 0)
        {
            for (int i = 0; i < enemyWidthNum; i++)
            {
                StartCoroutine(enemyColumns[i].Move(minStage, maxStage, moveLineWaitTime));
            }
            yield return new WaitForSeconds((maxStage - minStage + 1) * moveLineWaitTime);
        }
    }

    IEnumerator Shot()
    {
        yield return new WaitForSeconds(shotInterval);
    }
}
