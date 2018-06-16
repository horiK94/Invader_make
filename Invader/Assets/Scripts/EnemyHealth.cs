using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int startHp = 1;
    private int hp = 1;

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
