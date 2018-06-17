using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UFOHealth : EnemyHealth
{
    private int[] points = {300, 150, 100, 50};
    private UnityAction<int> onAddScore;

    public UnityAction<int> OnAddScore
    {
        get { return onAddScore; }
        set { onAddScore = value; }
    }
    
    protected override void Death()
    {
        int point = points[Random.Range(0, 5)];
        onAddScore(point);
    }

    protected void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}
