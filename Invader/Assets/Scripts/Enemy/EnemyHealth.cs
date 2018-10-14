using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// EnemyのHPや生存に関するクラス
/// </summary>
public class EnemyHealth : MonoBehaviour
{
    /// <summary>
    /// 初期HP
    /// </summary>
    [SerializeField]
    private int startHp = 1;
    /// <summary>
    /// Enemyの当たり判定コンポーネントの参照
    /// </summary>
    [SerializeField]
    private EnemyTrigger enemyTrigger = null;
    /// <summary>
    /// 現在のHP
    /// </summary>
    private int hp = 1;
    /// <summary>
    /// 倒した時にもらえるpoint
    /// </summary>
    private int point = 0;

    /// <summary>
    /// 倒した時にスコア加算をするデリゲートメソッド
    /// </summary>
    protected UnityAction<int> onAddScore = null;
    public UnityAction<int> OnAddScore
    {
        get { return onAddScore; }
        set { onAddScore = value; }
    }

    /// <summary>
    /// 倒した時に呼ぶデリゲートメソッド
    /// </summary>
    protected UnityAction onDeath = null;
    public UnityAction OnDeath
    {
        get { return onDeath; }
        set { onDeath = value; }
    }

    protected void Awake()
    {
        hp = startHp;
    }

    void Start()
    {
        enemyTrigger.SetUp((other) =>
        {
            if (other.GetComponent<Bullet>() != null)
            {
                DecreaseHp();
                if (IsDead())
                {
                    Death();
                }
            }
        });
    }

    /// <summary>
    /// 初期設定
    /// </summary>
    public void SetUp(int point, UnityAction<int> _onAddScore, UnityAction _onDeath)
    {
        this.onAddScore = _onAddScore;
        this.onDeath = _onDeath;
        this.point = point;
    }

    /// <summary>
    /// hpを減らす
    /// </summary>
    void DecreaseHp()
    {
        hp--;
    }

    /// <summary>
    /// 死んだかどうか
    /// </summary>
    bool IsDead()
    {
        return hp <= 0;
    }

    /// <summary>
    /// 死んだ時に呼ばれる関数
    /// </summary>
    protected virtual void Death()
    {
        OnAddScore(point);
        OnDeath();
        gameObject.SetActive(false);
    }

#if UNITY_EDITOR
    public void Kill()
    {
        Death();
    }
#endif
}
