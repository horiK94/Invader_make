using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot : MonoBehaviour {
	[SerializeField]GameObject bullet;
	[SerializeField] private Transform muzzleTransform;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space)) {
			Instantiate (bullet, muzzleTransform.position, Quaternion.identity);		// TODO objectpoolを使用する
		}
	}
}
