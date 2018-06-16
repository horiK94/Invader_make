using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOHealth : EnemyHealth
{
    private int[] points = {300, 150, 100, 50};
    protected override void Death()
    {
        int point = points[Random.Range(0, 5)];
        // Controllerに返す
        Debug.Log("point" + point);
    }

    protected void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}
