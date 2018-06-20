using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour {
	UnityAction onDeath = null;

	public void SetDeathAction(UnityAction onDeath){
		this.onDeath = onDeath;
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
