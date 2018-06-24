using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour {
	[SerializeField] float speed = 10;

	private float limitWorldPosX = 0;

	public void Set(float _limitWorldPosX)
	{
		this.limitWorldPosX = _limitWorldPosX;
	}

	public void Move(float _amount)
	{
		float posX = Mathf.Clamp(transform.position.x + _amount * speed * Time.deltaTime, -limitWorldPosX, limitWorldPosX);
		transform.position = new Vector3 (posX, transform.position.y, transform.position.z);
	}
}
