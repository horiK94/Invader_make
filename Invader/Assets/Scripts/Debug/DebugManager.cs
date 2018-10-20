using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugManager : MonoBehaviour {

    int enterTapNum = 0;        //エンターキーのタップ回数

    private void Awake()
    {
        enterTapNum = 0;
    }

    protected void Start()
    {
        Init();
    }

    protected void Update()
    {
        if(enterTapNum >= 5)
        {
            Appear();
        }
       
        if(Input.GetKeyDown(KeyCode.Return))
        {
            enterTapNum++;
        }
    }

    protected virtual void Init()
    {

    }

    protected virtual void Appear()
    {

    }
}
