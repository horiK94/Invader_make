using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int startHp = 1;
    private int hp = 1;
    protected UnityAction<int> onAddScore;
    private int point;
    public int Point
    {
        get { return point; }
        set { point = value; }
    }

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
        Debug.Log("<EnemyHealth> Death");
        OnAddScore(point);
        OnDeath();
        gameObject.SetActive(false);
    }
    
    protected void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Bullet>() != null)
        {
            DecreaseHp();
            if (IsDead())
            {
                Death();
            }
        }
    }
}
