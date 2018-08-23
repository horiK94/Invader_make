using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Events;

/// <summary>
/// Enemyの行を管理するクラス
/// </summary>
public class EnemyRowController : MonoBehaviour {
    /// <summary>
    /// 隣の敵の位置に移動するのに、何回移動するか
    /// </summary>
    [SerializeField]
    private int moveNextEnemyActionNum = 8;
    /// <summary>
    /// 行のenemyのゲームオブジェクト
    /// </summary>
    private GameObject[] enemy = null;
    /// <summary>
    /// 行に関する情報
    /// </summary>
    private EnemyRowCreateInfo rowInfo = null;
    
    /// <summary>
    /// 行に対してのEnemy作成
    /// </summary>
    public void Create(EnemyRowCreateInfo rowInfo, Transform enemyCrowdParent, UnityAction<int> _onAddScore, UnityAction<int> _onDeath)
    {
        this.rowInfo = rowInfo;
        int enemyWidthNum = rowInfo.enemyWidthNum;

        //親となる空オブジェクトを生成し、その中にこの後生成するenemyオブジェクトを入れていく
        Transform enemyRowParent = new GameObject("Enemys" + rowInfo.rowId + "Rows").transform;
        enemyRowParent.parent = enemyCrowdParent;

        // 前に移動する際の移動量
        float verticalDiff = (rowInfo.enemyMaxPos.y - rowInfo.enemyMinPos.y) / (rowInfo.stageNum - 1);
        
        enemy = new GameObject[enemyWidthNum];        //メモリの確保

        //enemyの生成
        for (int i = 0; i < enemyWidthNum; i++)
        {
            // enemyの生成・登録
            GameObject obj = Instantiate(rowInfo.rowInfo.Prefab);
            enemy[i] = obj;
            
            //親オブジェクトに登録していく
            obj.transform.parent = enemyRowParent;

            int upperNumber = rowInfo.enemyHeightNum - rowInfo.rowId - 1;        //Enemy全体で上から数えて何番目の行か(一番上の行は0)
            int stageNumber = rowInfo.startUpStageId - 2 * upperNumber;        //前から何行目か
            Vector3 enemyPos = new Vector3(rowInfo.enemyMinPos.x + rowInfo.enemyWidthInterval * i, rowInfo.enemyMinPos.y + stageNumber * verticalDiff, 0);        //位置の確定
            
            obj.transform.position = enemyPos;
            
            EnemyController controller = obj.GetComponent<EnemyController>();
            controller.BootUp(i, rowInfo.rowInfo.Point, _onAddScore, () =>
                {
                    _onDeath(rowInfo.rowId);
                }, rowInfo.enemyWidthInterval / moveNextEnemyActionNum, verticalDiff, rowInfo.enemyMinPos, rowInfo.enemyMaxPos);
        }
    }

    /// <summary>
    /// 前に移動する
    /// </summary>
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

    /// <summary>
    /// 左右に移動する
    /// </summary>
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

    /// <summary>
    /// 次の移動で左右に移動できるか
    /// </summary>
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

    /// <summary>
    /// 引数で指定した列のenemyが生きているか
    /// </summary>
    public bool IsAlive(int id)
    {
        return enemy[id].activeSelf;
    }

    /// <summary>
    /// 引数で指定した列のenemyに弾を撃たせる
    /// </summary>
    public void Shot(GameObject bullet, int columnId)
    {
        // 引数が正しい値かチェック
        if (!IsValidColumnId(columnId))
        {
            return;
        }
        // 該当のEnemyがactiveかどうかチェック
        if (!enemy[columnId].activeSelf)
        {
            return;
        }
        
        enemy[columnId].GetComponent<EnemyController>().Shot(bullet);
    }
    
    /// <summary>
    /// 列idが正常な値か
    /// </summary>
    bool IsValidColumnId(int columnId)
    {
        return columnId >= 0 && columnId < enemy.Length;
    }
}
