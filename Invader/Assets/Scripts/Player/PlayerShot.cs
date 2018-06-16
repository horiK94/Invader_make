using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot : MonoBehaviour {
	[SerializeField]GameObject bulletPrefab;
	[SerializeField] private Transform muzzleTransform;
	private GameObject bullet;

	void Awake()
	{
		if (bullet == null)
		{
			bullet = Instantiate(bulletPrefab);
		}
	}

	public void Shot()
	{
		if (!bullet.activeSelf)
		{
			bullet.SetActive(true);
			bullet.transform.position = muzzleTransform.position;
		}
	}
}
