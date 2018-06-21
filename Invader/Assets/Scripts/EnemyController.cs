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
    
    public void BootUp(UnityAction<int> _onAddScore, UnityAction _onDeath)
    {
        ufoController.BootUp(_onAddScore, _onDeath);
        enemyCrowdController.BootUp(_onAddScore, _onDeath);
        Debug.Log(_onDeath != null);
        ufoController.enabled = true;
        enemyCrowdController.enabled = true;
    }
}
