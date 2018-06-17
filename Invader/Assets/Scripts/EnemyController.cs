using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor.iOS.Xcode;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyCrowdController enemyCrowdController;
    [SerializeField] private UFOController ufoController;

    void Awake()
    {
        enemyCrowdController.enabled = true;
        ufoController.enabled = true;
    }
}
