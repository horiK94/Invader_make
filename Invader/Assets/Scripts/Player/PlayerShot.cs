using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot : MonoBehaviour {
	[SerializeField]GameObject bullet;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space)) {
			Instantiate (bullet, transform.position, Quaternion.identity);		// TODO objectpoolを使用する
		}
	}
}
