using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot : MonoBehaviour
{
    [SerializeField] private float speed;

    public void Shot(Vector3 pos, GameObject bullet)
    {
        if (!bullet.activeSelf)
        {
            bullet.SetActive(true);
            bullet.transform.position = transform.position;
        }
    }
}
