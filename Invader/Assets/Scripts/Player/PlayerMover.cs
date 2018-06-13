using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour {
	[SerializeField] float speed = 10;
	[SerializeField] float playerWidth = 4;
	float maxWorldPosX = 0;

	void Awake()
	{
		maxWorldPosX = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, transform.position.z - Camera.main.transform.position.z)).x - playerWidth;
	}

	void Update()
	{
		float h = Input.GetAxis ("Horizontal");
		float posX = Mathf.Clamp (transform.position.x + h * speed * Time.deltaTime, -maxWorldPosX, maxWorldPosX);
		transform.position = new Vector3 (posX, transform.position.y, transform.position.z);
	}
}
