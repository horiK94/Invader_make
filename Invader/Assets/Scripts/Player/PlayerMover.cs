using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour {
	[SerializeField] float speed = 10;

	public void Move(float amount, float limitWorldPosX)
	{
		float posX = Mathf.Clamp(transform.position.x + amount * speed * Time.deltaTime, -limitWorldPosX, limitWorldPosX);
		transform.position = new Vector3 (posX, transform.position.y, transform.position.z);
	}
}
