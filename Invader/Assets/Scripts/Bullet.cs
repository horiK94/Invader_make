using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 弾の挙動に関するクラス
/// </summary>
public class Bullet : MonoBehaviour
{
    /// <summary>
    /// 弾の高さ
    /// </summary>
    [SerializeField] private float bulletHeight;
    /// <summary>
    /// 移動速度
    /// </summary>
    [SerializeField] private float speed;
    /// <summary>
    /// 弾の移動する座標yの最大値
    /// </summary>
    private float maxWorldPosY = 0;
    /// <summary>
    /// 弾の移動する座標yの最小値
    /// </summary>
    private float minWorldPosY = 0;
    /// <summary>
    /// 弾のRigidbody
    /// </summary>
    private Rigidbody rigid = null;
    
    void Start()
    {
        //弾の移動可能距離を設定 
        maxWorldPosY = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, transform.position.z - Camera.main.transform.position.z)).y - bulletHeight;
        minWorldPosY = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, transform.position.z - Camera.main.transform.position.z)).y + bulletHeight;
        
        rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        rigid.position += new Vector3(0, speed * Time.deltaTime, 0);

        //移動可能座標外かどうか
        bool isOutside = transform.position.y > maxWorldPosY || transform.position.y < minWorldPosY;
        if (isOutside)
        {
            // TODO 壊れるアニメーション
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
    }
}
