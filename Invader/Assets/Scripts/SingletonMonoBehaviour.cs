using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.XR;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T: MonoBehaviour
{
    private static T instance = null;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    Debug.LogError(typeof(T) + "をアタッチしているオブジェクトはありません");
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        SetSingleton();
    }

    void SetSingleton()
    {
        if (instance == null)
        {
            //自分自身をinstanceに登録する
            instance = this as T;
            return;
        }

        if (instance == this)
        {
            //自分自身が登録されている場合はそのまま
            return;
        }
        else
        {
            Destroy(this);
        }
    }
}
