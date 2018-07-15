using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour {
	/// <summary>
	/// Playerが死んだ時の処理
	/// </summary>
	UnityAction onDeath = null;

	/// <summary>
	/// Playerが死んだ時の処理を登録する
	/// </summary>
	public void SetDeathAction(UnityAction _onDeath){
		this.onDeath = _onDeath;
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.GetComponent<Bullet>() != null) {		//TODO 弾なら~といった実装をする
			this.onDeath();
			//TODO 発散するアニメーション
			gameObject.SetActive(false);
		}
	}
}
