using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UFOHealth : EnemyHealth
{
    private int[] points = {300, 150, 100, 50};
    
    protected override void Death()
    {
        int point = points[Random.Range(0, 4)];
        onAddScore(point);
        OnDeath();
        gameObject.SetActive(false);
    }

    protected void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}
