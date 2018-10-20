using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.Assertions.Must;
using UnityEngine.Events;
using Random = UnityEngine.Random;

/// <summary>
/// Enemy全体のController
/// </summary>
public class EnemyCrowdController : MonoBehaviour
{
    private Transform enemyParent = null;

    /// <summary>
    /// Enemyの列数
    /// </summary>
    [SerializeField]
    private int enemyWidthNum = 11;
    /// <summary>
    /// Enemyの段数
    /// </summary>
    [SerializeField]
    private int enemyHeightNum = 5;
    /// <summary>
    /// Enemyの列ごとのプレファブ情報
    /// </summary>
    [SerializeField]
    private EnemyLineInfo[] enemyLineInfo = null;
    /// <summary>
    /// ufoを出現させるのに必要なEnemyの最低の数
    /// </summary>
    [SerializeField]
    private int ufoPopMinEnemyNum = 8;
    /// <summary>
    /// 移動速度を上げ始めるEnemyの数
    /// </summary>
    [SerializeField]
    private int speedUpStartNum = 10;
    /// <summary>
    /// 敵の最大速度比(初期移動速度は1)
    /// </summary>
    [SerializeField]
    private float enemyMaxSpeed = 8;
    /// <summary>
    /// スタート前の停止時間
    /// </summary>
    [SerializeField]
    private float startWaitTime = 2.5f;
    /// <summary>
    /// Enemyの攻撃間隔
    /// </summary>
    [SerializeField]
    private float shotInterval = 1;
    /// <summary>
    /// Enemyの一番上の段のy座標と画面上のy座標の差
    /// </summary>
    [SerializeField]
    private float enemyTopPosYDiff = 0;
    /// <summary>
    /// Enemyの一番下の段のy座標と画面下のy座標の差
    /// </summary>
    [SerializeField]
    private float enemyBottomPosYDiff = 0;
    /// <summary>
    /// Enemyの横の間隔(基本、8移動でそこまで移動)
    /// </summary>
    [SerializeField]
    private float enemyWidthInterval = 0;
    /// <summary>
    /// 段数の数
    /// </summary>
    [SerializeField]
    int stageNum = 19;
    /// <summary>
    /// １行移動するのに待つ時間
    /// </summary>
    [SerializeField]
    private float moveRowWaitTime = 1.0f;
    /// <summary>
    /// bulletのPrefab
    /// </summary>
    [SerializeField]
    private GameObject bulletPrefab = null;

    /// <summary>
    /// 列ごとのController(EnemyRowController)の参照
    /// </summary>
    private EnemyRowController[] enemyRows = null;
    /// <summary>
    /// Enemyを倒した時のスコア加算デリゲートメソッド
    /// </summary>
    private UnityAction<int> onAddScore = null;
    /// <summary>
    /// Enemy全体が死んだ際に呼ぶデリゲートメソッド
    /// </summary>
    private UnityAction onDeathAll = null;
    /// <summary>
    /// Enemyの可動領域の左下の座標
    /// </summary>
    private Vector3 minPos = Vector3.zero;
    /// <summary>
    /// Enemyの可動領域の右上の座標
    /// </summary>
    private Vector3 maxPos = Vector3.zero;
    /// <summary>
    /// 列ごとの残りEnemy数
    /// </summary>
    private int[] rowAliveEnemyNum = null;
    /// <summary>
    /// 前の移動の際に、方向転換を行ったかどうか
    /// </summary>
    private bool wasTurn = false;
    /// <summary>
    /// Enemyが生きている列idを保存したリスト
    /// </summary>
    private List<int> columnEnemyAliveCash = null;
    /// <summary>
    /// 敵の弾のプレファブ
    /// </summary>
    private GameObject enemyBullet = null;
    /// <summary>
    /// 停止状態か
    /// </summary>
    private bool isStop = false;
    /// <summary>
    /// 全敵が死んだ状態を伝えたか
    /// </summary>
    private bool isDeathCall = false;

    void Awake()
    {
        //メモリの確保
        columnEnemyAliveCash = new List<int>(enemyWidthNum);
        rowAliveEnemyNum = new int[enemyHeightNum];

        // enemyBulletの作成
        enemyBullet = Instantiate(bulletPrefab) as GameObject;
        enemyBullet.SetActive(false);
    }

    /// <summary>
    /// 初期設定を行う
    /// </summary>
    public void BootUp(UnityAction<int> _onAddScore, UnityAction _onDeath, Vector3 _maxPos, Vector3 _minPos)
    {
        this.onAddScore = _onAddScore;
        this.onDeathAll = _onDeath;
        this.maxPos = _maxPos;
        this.minPos = _minPos;
    }

    /// <summary>
    /// 移動開始
    /// </summary>
    public void MoveStart()
    {
        SortEnemyPrefab();
        Create();
        StartCoroutine(Act());
    }

    /// <summary>
    /// enemyLineInfoを行idの順にソートし直す
    /// </summary>
    void SortEnemyPrefab()
    {
        Array.Sort(enemyLineInfo, (x, y) => { return x.HighestLine - y.HighestLine; });
        if (enemyLineInfo[enemyLineInfo.Length - 1].HighestLine != enemyHeightNum - 1)
        {
            Debug.LogError("enemyLineInfo[enemyLineInfo.Length - 1].HighestLine != enemyHeightNum - 1となっています");
        }
    }

    /// <summary>
    /// Enemyの生成
    /// </summary>
    private void Create()
    {
        //生成したenemyをしまう親オブジェクトを設定
        enemyParent = new GameObject("Enemys").transform;

        rowAliveEnemyNum = rowAliveEnemyNum.Select(i => enemyWidthNum).ToArray();        //全要素にenemyの１行に設置する数を代入する
        //全ての列のidをListに追加
        for (int i = 0; i < enemyWidthNum; i++)
        {
            columnEnemyAliveCash.Add(i);
        }

        //行ごとにContollerを作成
        enemyRows = new EnemyRowController[enemyHeightNum];
        for (int i = 0; i < enemyHeightNum; i++)
        {
            //行に関するControllerの作成
            int lineNumber = i + 1;
            string rowControllerPrefabName = "Enemy" + lineNumber + "RowController";
            GameObject column = new GameObject(rowControllerPrefabName);        //行のControllerとなるオブジェクトを生成
            EnemyRowController controller = column.AddComponent<EnemyRowController>();
            enemyRows[i] = controller;

            column.transform.parent = transform;

            //列に対して与える情報を設定
            EnemyRowCreateInfo rowInfo = new EnemyRowCreateInfo();
            rowInfo.rowId = i;
            rowInfo.rowInfo = FindEnemyInfo(i);
            rowInfo.enemyHeightNum = enemyHeightNum;
            rowInfo.enemyWidthNum = enemyWidthNum;
            rowInfo.startUpStageId = stageNum - 1;        //TODO ステージ数によって変化させる
            rowInfo.enemyWidthInterval = enemyWidthInterval;
            rowInfo.stageNum = stageNum;
            rowInfo.enemyMinPos = new Vector3(minPos.x, minPos.y + enemyBottomPosYDiff, 0);
            rowInfo.enemyMaxPos = new Vector3(maxPos.x, maxPos.y - enemyTopPosYDiff, 0);

            controller.Create(rowInfo, enemyParent, onAddScore, (id) =>
            {
                rowAliveEnemyNum[id]--;
                if (rowAliveEnemyNum.Sum() == 0)        //敵が全員死んだ時
                {
                    onDeathAll();
                }

                if (rowAliveEnemyNum[id] == 0)        //idの行のEnemyが全滅した時
                {
                    // TODO
                }
            });
        }
    }

    /// <summary>
    /// Enemyの移動・攻撃を行う
    /// </summary>
    private IEnumerator Act()
    {
        yield return new WaitForSeconds(startWaitTime);
        StartCoroutine(Move());
        StartCoroutine(Shot());
    }

    /// <summary>
    /// enemyの移動を行う
    /// </summary>
    private IEnumerator Move()
    {
        while (enemyRows.Where(e => e != null).ToArray().Length != 0)        //全敵が死んでいる場合は移動を行わない
        {
            //全行の敵に対してどの方向に移動するかの処理を同じに行うため、for文の外で判定を行う
            /*
             * 各行で前方に進むかの処理をしようとすると、端の敵が倒された時に前進するタイミングが行によってずれる
             * また、for文の外で判定しないと、ある行が移動する前に、前方へ移動した行があった時に前方に移動した行の判定が前方へ移動した後に左右に移動できるに変わるため、
             * 前方に移動すべきかの判定がうまくとれなくなる
             */
            if (isStop)
            {
                //停止状態の時
                yield return null;
                continue;
            }
            bool canMoveSide = CanMoveSide();
            int enemyNum = rowAliveEnemyNum.Sum();

            if (enemyNum <= 0 && !isDeathCall)
            {
                isDeathCall = true;
                onDeathAll();
            }

            if (enemyNum <= speedUpStartNum)
            {
                //敵の速度を早くする
                for (int i = 0; i < enemyHeightNum; i++)
                {
                    if (canMoveSide)
                    {
                        float enemySpeed = enemyMaxSpeed / enemyNum;
                        enemyRows[i].MoveSide(enemySpeed);
                    }
                    else
                    {
                        enemyRows[i].MoveBefore();
                    }
                }
                yield return null;
            }
            else
            {
                for (int i = 0; i < enemyHeightNum; i++)
                {
                    if (canMoveSide)        //左右に移動できるか
                    {
                        enemyRows[i].MoveSide();
                    }
                    else
                    {
                        enemyRows[i].MoveBefore();        //移動できない場合は前に進む
                    }
                    yield return new WaitForSeconds(moveRowWaitTime);        //移動後のインターバル
                }
            }
        }
    }

    /// <summary>
    /// 次の移動で全て行が左右に移動できるか
    /// </summary>
    bool CanMoveSide()
    {
        for (int i = 0; i < enemyHeightNum; i++)
        {
            if (!enemyRows[i].CanMoveSide() && !wasTurn)
            {
                wasTurn = true;
                return false;
            }
        }
        wasTurn = false;
        return true;
    }

    /// <summary>
    /// 攻撃する
    /// </summary>
    IEnumerator Shot()
    {
        while (columnEnemyAliveCash.Count != 0)        //全列で敵が死んでいる時
        {
            if (isStop)
            {
                //停止状態の時
                yield return null;
                continue;
            }

            int r = Random.Range(0, columnEnemyAliveCash.Count);        //発射する列のidが入ったリストの要素番号をランダムで決定
            int randomId = columnEnemyAliveCash[r];        //列のidを取得

            //上で決定した列idに敵が生存しているかの確認
            int aliveColumnid = GetAliveMinRowId(randomId);
            if (aliveColumnid == -1)        //生存していない
            {
                //次選択されないようにリストから削除する
                columnEnemyAliveCash.RemoveAt(r);
            }
            else
            {
                //生存している敵のうち一番Playerに近い敵に発射を依頼
                enemyRows[aliveColumnid].Shot(enemyBullet, randomId);
                yield return new WaitForSeconds(shotInterval);
            }
        }
    }

    /// <summary>
    /// 引数で渡した行idに該当する行を生成するのに必要な情報を返す
    /// </summary>
    EnemyLineInfo FindEnemyInfo(int line)
    {
        for (int j = 0; j < enemyLineInfo.Length; j++)
        {
            int lowerLine = j == 0 ? 0 : enemyLineInfo[j - 1].HighestLine + 1;
            if (line >= lowerLine && line <= enemyLineInfo[j].HighestLine)
            {
                return enemyLineInfo[j];
            }
        }
        return null;
    }

    /// <summary>
    /// 行と列のidのEnemyが生存しているかどうかを返す
    /// </summary>
    /// <param name="rowId"></param>
    bool IsAliveEnemy(int rowId, int columnId)
    {
        return enemyRows[rowId].IsAlive(columnId);
    }

    /// <summary>
    /// 列を示すcolumnIdに対して、生きている最小の行Idを返す
    /// </summary>
    int GetAliveMinRowId(int columnId)
    {
        for (int i = 0; i < enemyHeightNum; i++)
        {
            if (IsAliveEnemy(i, columnId))
            {
                return i;
            }
        }
        return -1;
    }

    /// <summary>
    /// 動きを止める
    /// </summary>
    public void StopAct()
    {
        isStop = true;
    }

    /// <summary>
    /// 動きを再開する
    /// </summary>
    public void RestartAct()
    {
        isStop = false;
    }

#if UNITY_EDITOR
    public void KillAllEnemy()
    {
        for (int i = 0; i < enemyRows.Length; i++)
        {
            enemyRows[i].KillAllEnemy();
        }
    }
#endif
}
