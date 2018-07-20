using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour {
	/// <summary>
	/// Playerのプレファブ
	/// </summary>
	[SerializeField]GameObject playerPrefab = null;
	/// <summary>
	/// 画面左下の座標からどれだけ離れた位置からPlayerの移動を解するか
	/// </summary>
	[SerializeField]Vector3 minPosDiffAtStart = Vector3.zero;

	/// <summary>
	/// プレファブから生成したPlayerの参照
	/// </summary>
	private GameObject player = null;
	/// <summary>
	/// PlayerにアタッチされたPlayerMoverコンポーネントの参照
	/// </summary>
	private PlayerMover playerMover  = null;
	/// <summary>
	/// PlayerにアタッチされたPlayerHealthコンポーネントの参照
	/// </summary>
	private PlayerHealth playerHealth = null;
	/// <summary>
	/// PlayerにアタッチされたPlayerShotコンポーネントの参照
	/// </summary>
	private PlayerShot playerShot = null;

	public int ResultHp
	{
		get { return playerHealth == null ? -1 : playerHealth.ResultHp; }
	}

	/// <summary>
	/// playerの生成
	/// </summary>
	public void BootUp(Vector3 _maxPos, Vector3 _minPos, UnityAction onDeath)
	{
		player = Instantiate (playerPrefab, _minPos + minPosDiffAtStart, Quaternion.identity);
		
		playerMover = player.GetComponent<PlayerMover>();
		playerHealth = player.GetComponent<PlayerHealth>();
		playerShot = player.GetComponent<PlayerShot>();
		
		playerHealth.SetDeathAction(onDeath);
		
		//playerのx座標方向の移動可能範囲を設定
		playerMover.SetLimitPos(_maxPos.x, _minPos.x);
	}

	void Update()
	{
		playerMover.Move(Input.GetAxis("Horizontal"));
		if (Input.GetKeyDown(KeyCode.Space))
		{
			playerShot.Shot();
		}
	}	
}
