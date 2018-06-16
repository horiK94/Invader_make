using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletHeight;
    [SerializeField] private float speed;
    private float maxWorldPosY;
    private Rigidbody rigid;
    
    void Start()
    {
        maxWorldPosY = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, transform.position.z - Camera.main.transform.position.z)).y - bulletHeight;
        rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        rigid.position += new Vector3(0, speed * Time.deltaTime, 0);
        if (transform.position.y > maxWorldPosY)
        {
            // TODO 壊れるアニメーション
            gameObject.SetActive(false);
        }
    }
}
