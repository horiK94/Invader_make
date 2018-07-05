using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour
{
	private EnemyHealth enemyHealth;
	private EnemyMesh enemyMesh;
	private EnemyMove enemyMove;
	private int id;
	public int Id => id;
	//横移動量
	private float moveHorizontalAmount;
	private float moveVerticalAmount;
	private bool isFacingRight;

	private Vector3 minPos;
	private Vector3 maxPos;

	private bool isDead = false;
	public bool IsDead => isDead;

	void Awake()
	{
		enemyHealth = GetComponentInChildren<EnemyHealth>();
		enemyMesh = GetComponentInChildren<EnemyMesh>();
		enemyMove = GetComponentInChildren<EnemyMove>();
		isFacingRight = true;
	}
	//欲しいデータ
	//横にどれくらい移動するか
	//縦にどのくらい移動するか
	//端っこの情報

	public void BootUp(int id, int point, UnityAction<int> onAddScore, UnityAction onDeath, float moveHorizontalAmount, float moveVerticalAmount, Vector3 minPos, Vector3 maxPos)
	{
		this.id = id;
		this.moveHorizontalAmount = moveHorizontalAmount;
		this.moveVerticalAmount = moveVerticalAmount;
		this.minPos = minPos;
		this.maxPos = maxPos;
		
		enemyHealth.SetUp(point, onAddScore, () =>
		{
			isDead = true;
			onDeath();
		});
	}

	public bool CanMoveSide()
	{
		float moveSign = isFacingRight ? 1 : -1;
		float willMovePosX = transform.position.x + moveSign * moveHorizontalAmount;
		bool isInside = willMovePosX >= minPos.x && willMovePosX <= maxPos.x;
		return isInside;
	}

	public void MoveSide()
	{
		float moveSign = isFacingRight ? 1 : -1;
		enemyMove.Move(moveSign * new Vector3(moveHorizontalAmount, 0, 0));
		enemyMesh.ChangeMesh();
	}

	public void MoveBefore()
	{
		isFacingRight = !isFacingRight;
		enemyMove.Move(new Vector3(0, -moveVerticalAmount, 0));
		enemyMesh.ChangeMesh();
	}
}
