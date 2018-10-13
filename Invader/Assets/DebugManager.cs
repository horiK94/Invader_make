using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugManager : MonoBehaviour {
    [SerializeField]
    private DebugPlayer debugPlayer = null;

    int enterTapNum = 0;        //エンターキーのタップ回数

    private void Awake()
    {
        enterTapNum = 0;

        debugPlayer.Init();
    }

    private void Update()
    {
        if(enterTapNum >= 5)
        {
            debugPlayer.Appear();
        }
       
        if(Input.GetKey(KeyCode.Return))
        {
            enterTapNum++;
        }
    }
}
