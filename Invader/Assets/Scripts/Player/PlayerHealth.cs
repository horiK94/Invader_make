using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
	/// <summary>
	/// 初期残機
	/// </summary>
	[SerializeField] private int startHp;
	/// <summary>
	/// Playerが死んだ時の処理
	/// </summary>
	UnityAction onDeath = null;
	/// <summary>
	/// 残り残機
	/// </summary>
	private int resultHp = 0;
	public int ResultHp => resultHp;

	void Awake()
	{
		resultHp = startHp;
	}

	/// <summary>
	/// Playerが死んだ時の処理を登録する
	/// </summary>
	public void SetDeathAction(UnityAction _onDeath){
		this.onDeath += _onDeath;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<Bullet>() != null) {		//TODO 弾なら~といった実装をする
			DecreaseHp();
			//TODO 発散するアニメーション
			gameObject.SetActive(false);
		}
	}

	/// <summary>
	/// HPを減らす
	/// </summary>
	public void DecreaseHp()
	{
		resultHp--;
		if (resultHp <= 0)
		{
			this.onDeath();
			return;
		}
	}
}
