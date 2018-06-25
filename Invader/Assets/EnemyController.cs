using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour
{
	private EnemyHealth enemyHealth;
	private EnemyMesh enemyMesh;

	void Awake()
	{
		enemyHealth = GetComponentInChildren<EnemyHealth>();
		enemyMesh = GetComponentInChildren<EnemyMesh>();
	}

	public void BootUp(int point, UnityAction<int> onAddScore, UnityAction onDeath)
	{
		enemyHealth.SetUp(point, onAddScore, onDeath);
	}

	public void Move()
	{
		//TODO 移動
		enemyMesh.Change();
	}
}
