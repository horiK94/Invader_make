using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.XR;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T: MonoBehaviour
{
    private static T instance = null;

    public static T Instance => instance;

    void Awake()
    {
        
    }
}
