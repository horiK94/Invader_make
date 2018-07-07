using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Events;

public class EnemyRowController : MonoBehaviour {
    /// <summary>
    /// 隣の敵のいちに移動するのに、何回移動するか
    /// </summary>
    [SerializeField] private int moveNextEnemyActionNum = 8;
    private UnityAction<int> onAddScore;
    private GameObject[] enemy;
    private EnemyRowCreateInfo rowInfo;
    
    public void Create(EnemyRowCreateInfo rowInfo, Transform enemyColumnParent, UnityAction<int> _onAddScore, UnityAction<int> _onDeath)
    {
        this.rowInfo = rowInfo;
        int enemyWidthNum = rowInfo.enemyWidthNum;
        float enemyHeightInterval = (rowInfo.enemyMaxPos.y - rowInfo.enemyMinPos.y) / (rowInfo.stageNum - 1);

        //親となる空オブジェクトを生成し、その中にこの後生成するenemyオブジェクトを入れていく
        Transform enemyColumnObj = new GameObject("Enemys" + rowInfo.rowId + "Column").transform;
        enemyColumnObj.parent = enemyColumnParent;

        // 垂直方向の段差を求める
        float verticalDiff = (rowInfo.enemyMaxPos.y - rowInfo.enemyMinPos.y) / (rowInfo.stageNum - 1);
        
        enemy = new GameObject[enemyWidthNum];

        //enemyの生成
        for (int i = 0; i < enemyWidthNum; i++)
        {
            GameObject obj = Instantiate(rowInfo.rowInfo.Prefab);
            enemy[i] = obj;
            
            obj.transform.parent = enemyColumnObj;

            int upperNumber = rowInfo.enemyHeightNum - rowInfo.rowId - 1;
            int stageNumber = rowInfo.startUpStageId - 2 * upperNumber;
            Vector3 enemyPos = new Vector3(rowInfo.enemyMinPos.x + rowInfo.enemyWidthInterval * i, rowInfo.enemyMinPos.y + stageNumber * verticalDiff, 0);
            obj.transform.position = enemyPos;
            EnemyController controller = obj.GetComponent<EnemyController>();
            controller.BootUp(i, rowInfo.rowInfo.Point, _onAddScore, () =>
                {
                    _onDeath(rowInfo.rowId);
                }, rowInfo.enemyWidthInterval / moveNextEnemyActionNum, verticalDiff, rowInfo.enemyMinPos, rowInfo.enemyMaxPos);
        }
    }

    public void MoveBefore()
    {
        for (int i = 0; i < enemy.Length; i++)
        {
            if (enemy[i].activeSelf)
            {
                enemy[i].GetComponent<EnemyController>().MoveBefore();
            }
        }
    }

    public void MoveSide()
    {
        for (int i = 0; i < enemy.Length; i++)
        {
            if (enemy[i].activeSelf)
            {
                enemy[i].GetComponent<EnemyController>().MoveSide();
            }
        }
    }

    public bool CanMoveSide()
    {
        for (int i = 0; i < enemy.Length; i++)
        {
            if (enemy[i].activeSelf && !enemy[i].GetComponent<EnemyController>().CanMoveSide())
            {
                return false;
            }
        }
        return true;
    }

    public bool IsAlive(int id)
    {
        return enemy[id].activeSelf;
    }

    public void Shot(int columnId)
    {
        if (!IsValidColumnId(columnId) || !enemy[columnId].activeSelf)
        {
            return;
        }
    }
    
    bool IsValidColumnId(int columnId)
    {
        return columnId >= 0 && columnId < enemy.Length;
    }
}
