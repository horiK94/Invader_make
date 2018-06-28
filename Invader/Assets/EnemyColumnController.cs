using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class EnemyColumnController : MonoBehaviour
{
    /// <summary>
    /// 隣の敵のいちに移動するのに、何回移動するか
    /// </summary>
    [SerializeField] private int moveNextEnemyActionNum = 8;
    private UnityAction<int> onAddScore;
    private List<GameObject> enemy = new List<GameObject>();
    private EnemyColumnCreateInfo columnInfo;

    public void Create(EnemyColumnCreateInfo columnInfo, EnemyLineInfo[] lineInfo, Transform enemyColumnParent, UnityAction<int> _onAddScore, UnityAction _onDeath)
    {
        this.columnInfo = columnInfo;
        int enemyHeightNum = columnInfo.enemyHeightNum;
        float enemyHeightInterval = (columnInfo.enemyMaxPos.y - columnInfo.enemyMinPos.y) / (columnInfo.stageNum - 1);
        
        //親となる空オブジェクトを生成し、その中にこの後生成するenemyオブジェクトを入れていく
        Transform enemyColumnObj = new GameObject("Enemys" + columnInfo.columnId + "Column").transform;
        enemyColumnObj.parent = enemyColumnParent;
        
        //enemyの情報を参照
        EnemyLineInfo[] line = new EnemyLineInfo[enemyHeightNum];
        for (int i = 0; i < enemyHeightNum; i++)
        {
            int lineNumber = i + 1;
            line[i] = FindEnemyInfo(lineNumber, lineInfo);
        }

        //enemyの生成
        for (int i = 0; i < enemyHeightNum; i++)
        {
            GameObject obj = Instantiate(line[i].Prefab);
            enemy.Add(obj);
            
            obj.transform.parent = enemyColumnObj;
            
            obj.transform.position = new Vector3(columnInfo.enemyMinPos.x + columnInfo.enemyWidthInterval * (columnInfo.columnId - 1),
                columnInfo.enemyMaxPos.y - (columnInfo.stageNum - columnInfo.startUpStageId + 2 * (enemyHeightNum - i - 1)) * enemyHeightInterval, 0);

            EnemyController controller = obj.GetComponent<EnemyController>();
            controller.BootUp(i, line[i].Point, _onAddScore, (id) =>
            {
                enemy.RemoveAll(ob => ob.GetComponent<EnemyController>().Id == id);
                if (enemy.Count <= 0)
                {
                    _onDeath();
                }
            });
        }
    }
    
    /// <summary>
    /// 列番号から該当するenemyのデータを受け取る
    /// </summary>
    /// <param name="line">ライン番号</param>
    /// <param name="lineInfo">ライン情報</param>
    /// <returns></returns>
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
    }

    public void Move()
    {
    }
}
