using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// enemy単体の攻撃クラス
/// </summary>
public class EnemyShot : MonoBehaviour
{
    /// <summary>
    /// 攻撃
    /// </summary>
    public void Shot(Vector3 pos, GameObject bullet)
    {
        if (!bullet.activeSelf)
        {
            bullet.SetActive(true);
            bullet.transform.position = pos;
        }
    }
}
