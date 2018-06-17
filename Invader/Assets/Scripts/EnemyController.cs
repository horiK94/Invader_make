using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor.iOS.Xcode;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float ufoAppearanceProbability = 0.01f;
    [SerializeField] private int enemyWidth = 11;
    [SerializeField] private int enemyHeight = 5;
    [SerializeField] private EnemyLineInfo[] enemyLineInfo;
    [SerializeField] private UFOController ufo;
    private Transform enemyParent = null;

    private GameObject[,] enemy;
    private int resultEnemy;

    private void Awake()
    {
        if (ufoAppearanceProbability < 0 || ufoAppearanceProbability > 1)
        {
            Debug.LogError("ufoArrpearanceProbabilityは0~1の間でなくてはいけない");
        }

        enemy = new GameObject[enemyWidth, enemyHeight];
        resultEnemy = enemyWidth * enemyHeight;
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
        
        EnemyLineInfo[] line = new EnemyLineInfo[enemyHeight];

        for (int i = 0; i < enemyHeight; i++)
        {
            int lineNumber = i + 1;
            line[i] = FindEnemyInfo(lineNumber);
        }

        for (int i = 0; i < enemyWidth; i++)
        {
            for (int j = 0; j < enemyHeight; j++)
            {
                enemy[i, j] = Instantiate(line[j].Prefab);
                enemy[i, j].transform.parent = enemyParent;
            }
        }
    }

    EnemyLineInfo FindEnemyInfo(int line)
    {
        for (int j = 0; j < enemyLineInfo.Length; j++)
        {
            int lowerLine = j == 0 ? 1 : enemyLineInfo[j - 1].HighestLine + 1;
            if (line >= lowerLine && line <= enemyLineInfo[j].HighestLine)
            {
                return  enemyLineInfo[j];
            }
        }
        return null;
    }
}
