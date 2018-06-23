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
    
    public void BootUp(UnityAction<int> _onAddScore, UnityAction _onDeath, Vector2 _maxPos, Vector2 _minPos)
    {
        ufoController.BootUp(_onAddScore, _onDeath);
        enemyCrowdController.BootUp(_onAddScore, _onDeath, _maxPos, _minPos);
        ufoController.enabled = true;
        enemyCrowdController.enabled = true;
    }
}
