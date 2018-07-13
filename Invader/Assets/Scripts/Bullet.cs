using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletHeight;
    [SerializeField] private float speed;
    private float maxWorldPosY = 0;
    private float minWorldPosY = 0;
    private Rigidbody rigid = null;
    
    void Start()
    {
        maxWorldPosY = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, transform.position.z - Camera.main.transform.position.z)).y - bulletHeight;
        minWorldPosY = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, transform.position.z - Camera.main.transform.position.z)).y + bulletHeight;
        rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        rigid.position += new Vector3(0, speed * Time.deltaTime, 0);
        if (transform.position.y > maxWorldPosY || transform.position.y < minWorldPosY)
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
