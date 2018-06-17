using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor.iOS.Xcode;
using UnityEngine;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyCrowdController enemyCrowdController;
    [SerializeField] private UFOController ufoController;
    private UnityAction<int> onAddScore;

    public UnityAction<int> OnAddScore
    {
        get { return onAddScore; }
        set { onAddScore = value; }
    }

    void Awake()
    {
        enemyCrowdController.enabled = true;
        ufoController.enabled = true;
    }

    void Start()
    {
        ufoController.OnAddScore = this.OnAddScore;
    }
}
