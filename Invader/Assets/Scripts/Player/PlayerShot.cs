using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot : MonoBehaviour {
	[SerializeField]GameObject bullet;
	[SerializeField] private Transform muzzleTransform;

	public void Shot()
	{
		Instantiate (bullet, muzzleTransform.position, Quaternion.identity);		// TODO objectpoolを使用する
	}
}
