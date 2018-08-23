using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour {
	/// <summary>
	/// 移動スピード
	/// </summary>
	[SerializeField] 
    float speed = 10;
	/// <summary>
	/// 移動可能なx座標の最小値
	/// </summary>
	private float limitMinPosX = 0;
	/// <summary>
	/// 移動可能なx座標の最大値
	/// </summary>
	private float limitMaxPosX = 0;
	
	/// <summary>
	/// playerのx座標方向の移動可能範囲を設定
	/// </summary>
	public void SetLimitPos(float _limitMaxPosX, float _limitMinPosX)
	{
		this.limitMinPosX = _limitMinPosX;
		this.limitMaxPosX = _limitMaxPosX;
	}

	/// <summary>
	/// playerの移動を行う
	/// </summary>
	public void Move(float _amount)
	{
		float posX = Mathf.Clamp(transform.position.x + _amount * speed * Time.deltaTime, limitMinPosX, limitMaxPosX);
		transform.position = new Vector3 (posX, transform.position.y, transform.position.z);
	}
}
