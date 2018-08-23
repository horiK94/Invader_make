using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// UFOの移動に関するクラス
/// </summary>
public class UFOMover : MonoBehaviour
{
    /// <summary>
    /// UFOの移動速度
    /// </summary>
    [SerializeField]
    private float speed = 0;
    /// <summary>
    /// 端まで到着した時に呼ぶデリゲートメソッド
    /// </summary>
    private UnityAction onArriveCorner = null;
    /// <summary>
    /// 移動方向を決める数値
    /// </summary>
    private float moveSign = 1;
    /// <summary>
    /// 画面端の位置
    /// </summary>
    private float cornerPosX = 0;
    
    /// <summary>
    /// 移動のための値設定
    /// </summary>
    public void PrepareToMove(float sign, float cornerPosX, UnityAction onArriveCorner)
    {
        if (!gameObject.activeSelf)
        {
            moveSign = sign;
            this.cornerPosX = cornerPosX;
            this.onArriveCorner = onArriveCorner;
        }
    }

    private void Update()
    {
        transform.position += new Vector3(moveSign * Time.deltaTime * speed, 0, 0);
        if (moveSign > 0 && transform.position.x > cornerPosX)        //右端に移動し終わったか
        {
            onArriveCorner();
        }
        else if (moveSign < 0 && transform.position.x < cornerPosX)        //左端に移動し終わったか
        {
            onArriveCorner();
        }
    }
}
