using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot : MonoBehaviour
{
    [SerializeField] private float speed;

    public void Shot(Transform transform, GameObject bullet)
    {
        bullet.SetActive(true);
        bullet.transform.position = transform.position;
    }
}
