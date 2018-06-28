using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour
{
	private EnemyHealth enemyHealth;
	private EnemyMesh enemyMesh;
	private int id;
	public int Id => id;

	void Awake()
	{
		enemyHealth = GetComponentInChildren<EnemyHealth>();
		enemyMesh = GetComponentInChildren<EnemyMesh>();
	}

	public void BootUp(int id, int point, UnityAction<int> onAddScore, UnityAction<int> onDeath)
	{
		id = this.id;
		enemyHealth.SetUp(point, onAddScore, () => { onDeath(id); });
	}

	public void Move()
	{
		//TODO 移動
		enemyMesh.Change();
	}
}
