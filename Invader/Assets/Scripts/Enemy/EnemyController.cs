using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour
{
	/// <summary>
	/// 発射してすぐの弾の位置と敵の位置との差
	/// </summary>
	[SerializeField] private Vector3 shotPos;
	
	/// <summary>
	/// EnemyHelathの参照
	/// </summary>
	[SerializeField]
	private EnemyHealth enemyHealth;
	/// <summary>
	/// EnemyMeshの参照
	/// </summary>
	[SerializeField]
	private EnemyMesh enemyMesh;
	/// <summary>
	/// EnemyMoveの参照
	/// </summary>
	[SerializeField]
	private EnemyMove enemyMove;
	/// <summary>
	/// EnemyShotの参照
	/// </summary>
	[SerializeField]
	private EnemyShot enemyShot;
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
		isFacingRight = true;
	}
	
	public void BootUp(int id, int point, UnityAction<int> onAddScore, UnityAction onDeath, float moveHorizontalAmount, float moveVerticalAmount, Vector3 minPos, Vector3 maxPos)
	{
		this.id = id;
		this.moveHorizontalAmount = moveHorizontalAmount;
		this.moveVerticalAmount = moveVerticalAmount;
		this.minPos = minPos;
		this.maxPos = maxPos;
		
		Debug.Log(enemyHealth);
		enemyHealth.SetUp(point, onAddScore, () =>
		{
			isDead = true;
			onDeath();
		});
	}

	public bool CanMoveSide()
	{
		if (!gameObject.activeSelf)
		{
			return true;
		}
		
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

	public void Shot(GameObject bullet)
	{
		enemyShot.Shot(transform.position + shotPos,　bullet);
	}
}
