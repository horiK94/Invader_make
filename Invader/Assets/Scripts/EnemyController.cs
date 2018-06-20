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

    private UnityAction onDeath;

    public UnityAction OnDeath
    {
        get { return onDeath; }
        set { onDeath = value; }
    }

    void Start()
    {
        ufoController.OnAddScore = this.OnAddScore;
        enemyCrowdController.OnAddScore = this.OnAddScore;
        enemyCrowdController.enabled = true;
        ufoController.enabled = true;
        enemyCrowdController.OnDeath += () => { OnDeath(); };
    }
}
