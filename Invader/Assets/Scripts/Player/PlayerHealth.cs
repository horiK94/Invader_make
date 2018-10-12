using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private bool isInvalidation = false;

	/// <summary>
	/// Playerが死んだ時の処理
	/// </summary>
	UnityAction onDeath = null;

	/// <summary>
	/// Playerが死んだ時の処理を登録する
	/// </summary>
	public void SetDeathAction(UnityAction _onDeath){
		this.onDeath += _onDeath;
	}

	void OnTriggerEnter(Collider other)
	{
        if(isInvalidation)
        {
            //無効化中は当たっても処理を行わない
            return;
        }

		if (other.GetComponent<Bullet>() != null) {
            //弾が当たったら死ぬ
			onDeath();
		}
	}
}
