using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyColumnController : MonoBehaviour
{
    private int columnId;
    private UnityAction<int> onAddScore;
    private GameObject[] enemy;
    private int remainEnemy;

    public void Create(int columnId, int enemyHeight, EnemyLineInfo[] lineInfo, Transform enemyColumnParent, UnityAction<int> _onAddScore, UnityAction _onDeath)
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
            
            EnemyHealth health = enemy[i].GetComponent<EnemyHealth>();
            health.BootUp(line[i].Point, _onAddScore, () =>
            {
                remainEnemy--;
                if (remainEnemy <= 0)
                {
                    // 一列全滅した時の処理
                }
            });
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

    public void Shot()
    {
        Debug.Log("Shot");
    }
}
