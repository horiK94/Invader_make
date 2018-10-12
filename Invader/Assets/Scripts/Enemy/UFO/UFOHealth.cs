using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// UFOのHPに関するクラス
/// </summary>
public class UFOHealth : EnemyHealth
{
    /// <summary>
    /// 得点表
    /// </summary>
    private int[] points = {300, 150, 100, 50};
    
    /// <summary>
    /// 死んだ時に呼ぶメソッド
    /// </summary>
    protected override void Death()
    {
        int point = points[Random.Range(0, 4)];
        onAddScore(point);
        gameObject.SetActive(false);
    }
}
