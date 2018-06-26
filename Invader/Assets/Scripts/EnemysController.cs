using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;

public class EnemysController : MonoBehaviour
{
    [SerializeField] private EnemyCrowdController enemyCrowdController;
    [SerializeField] private UFOController ufoController;
    
    
    public void BootUp(UnityAction<int> _onAddScore, UnityAction _onDeath, Vector3 _maxPos, Vector3 _minPos)
    {
        ufoController.BootUp(_maxPos, _onAddScore, _onDeath);
        enemyCrowdController.BootUp(_onAddScore, _onDeath, () => { enemyCrowdController.enabled = false; },_maxPos, _minPos);
        ufoController.enabled = true;
        enemyCrowdController.enabled = true;
    }
}
