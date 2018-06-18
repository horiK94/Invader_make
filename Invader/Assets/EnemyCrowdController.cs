using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class EnemyCrowdController : MonoBehaviour {
    [SerializeField] private int enemyWidth = 11;
    [SerializeField] private int enemyHeight = 5;
    [SerializeField] private EnemyLineInfo[] enemyLineInfo;
    private Transform enemyParent = null;
    [SerializeField] private int ufoPopMinEnemyNum = 8;        //ufoを出現させるのに必要なEnemyの最低数 TODO: これ以下になったらUfoの生成を止める
    [SerializeField] private float startWaitTime = 2.5f;
    [SerializeField] private float shotInterval = 1;

    private List<EnemyColumnController> enemyColumn;
    private int resultEnemy;

    private void Awake()
    {
        enemyColumn = new List<EnemyColumnController>(enemyWidth);
        resultEnemy = enemyWidth * enemyHeight;        //各変数で値を確認するかデリゲートで死んだことを受け取る方方法でも良いかもしれない
        SortEnemyPrefab();
        Create();
        
    }

    void SortEnemyPrefab()
    {
        Array.Sort(enemyLineInfo, (x, y) => { return x.HighestLine - y.HighestLine; });
        if (enemyLineInfo[enemyLineInfo.Length - 1].HighestLine != enemyHeight)
        {
            Debug.LogError("enemyLineInfo[enemyLineInfo.Length - 1].HighestLine != enemyHeightとなっています");
        }
    }

    private void Create()
    {
        enemyParent = new GameObject("Enemys").transform; 

        for (int i = 0; i < enemyWidth; i++)
        {
            int lineNumber = i + 1;
            GameObject column = new GameObject("Enemy" + lineNumber + "ColumnController");
            column.transform.parent = transform;
            column.AddComponent<EnemyColumnController>();
            column.GetComponent<EnemyColumnController>().Create(lineNumber, enemyHeight, enemyLineInfo, enemyParent);
        }
    }

    IEnumerator Shot()
    {
        yield return new WaitForSeconds(startWaitTime);
        while (true)
        {
            int r = Random.Range(0, enemyColumn.Count);
            enemyColumn[r].Shot();
            yield return new WaitForSeconds(shotInterval);
        }
    }
}
