using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyColumnController : MonoBehaviour
{
    private int columnId;
    private UnityAction<int> onAddScore;
    private GameObject[] enemy;

    public UnityAction<int> OnAddScore
    {
        get { return onAddScore; }
        set { onAddScore = value; }
    }

    public void Create(int columnId, int enemyHeight, EnemyLineInfo[] lineInfo, Transform enemyColumnParent)
    {
        this.columnId = columnId;
        
        enemy = new GameObject[enemyHeight];
        Transform enemyColumnObj = new GameObject("Enemys" + columnId + "Column").transform;
        enemyColumnObj.parent = enemyColumnParent;
        
        EnemyLineInfo[] line = new EnemyLineInfo[enemyHeight];

        for (int i = 0; i < enemyHeight; i++)
        {
            int lineNumber = i + 1;
            line[i] = FindEnemyInfo(lineNumber, lineInfo);
        }

        for (int i = 0; i < enemyHeight; i++)
        {
            enemy[i] = Instantiate(line[i].Prefab);
            enemy[i].transform.parent = enemyColumnObj;
        }
    }
    
    EnemyLineInfo FindEnemyInfo(int line, EnemyLineInfo[] lineInfo)
    {
        for (int j = 0; j < lineInfo.Length; j++)
        {
            int lowerLine = j == 0 ? 1 : lineInfo[j - 1].HighestLine + 1;
            if (line >= lowerLine && line <= lineInfo[j].HighestLine)
            {
                return  lineInfo[j];
            }
        }
        return null;
    }
}
