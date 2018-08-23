using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot : MonoBehaviour {
	/// <summary>
	/// 弾のプレファブ
	/// </summary>
	[SerializeField]
    GameObject bulletPrefab = null;
	/// <summary>
	/// 発射口の位置
	/// </summary>
	[SerializeField]
    private Transform muzzleTransform = null;
	/// <summary>
	/// 生成した弾の参照
	/// </summary>
	private GameObject bullet = null;

	void Awake()
	{
		if (bullet == null)
		{
			bullet = Instantiate(bulletPrefab);
		}
	}

	/// <summary>
	/// 弾を撃つ
	/// </summary>
	public void Shot()
	{
		//　弾がステージ上に残っていない
		if (!bullet.activeSelf)
		{
			bullet.SetActive(true);
			bullet.transform.position = muzzleTransform.position;
		}
	}
}
