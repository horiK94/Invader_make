using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int startHp = 1;
    [SerializeField] private EnemyTrigger enemyTrigger = null;
    private int hp = 1;
    private int point;

    protected UnityAction<int> onAddScore;
    public UnityAction<int> OnAddScore
    {
        get { return onAddScore; }
        set { onAddScore = value; }
    }

    protected UnityAction onDeath;
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

    public void SetUp(int point, UnityAction<int> _onAddScore, UnityAction _onDeath)
    {
        this.onAddScore = _onAddScore;
        this.onDeath = _onDeath;
        this.point = point;
    }

    void DecreaseHp()
    {
        hp--;
    }

    bool IsDead()
    {
        return hp <= 0;
    }

    protected virtual void Death()
    {
        // TODO 死んだ処理
        OnAddScore(point);
        OnDeath();
        gameObject.SetActive(false);
    }
}
