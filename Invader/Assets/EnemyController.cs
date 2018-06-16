using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float ufoAppearanceProbability = 0.01f;
    [SerializeField] private int enemyWidth = 11;
    [SerializeField] private float enemyHeight = 5;

    private void Awake()
    {
        if (ufoAppearanceProbability < 0 || ufoAppearanceProbability > 1)
        {
            Debug.LogError("ufoArrpearanceProbabilityは0~1の間でなくてはいけない");
        }
        
    }
}
