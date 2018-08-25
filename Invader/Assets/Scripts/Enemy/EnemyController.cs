using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Enemy単体のController
/// </summary>
public class EnemyController : MonoBehaviour
{
	/// <summary>
	/// 発射してすぐの弾の位置と敵の位置との差
	/// </summary>
	[SerializeField] 
    private Vector3 shotPos = Vector3.zero;
	
	/// <summary>
	/// EnemyHelathの参照
	/// </summary>
	[SerializeField]
	private EnemyHealth enemyHealth = null;
	/// <summary>
	/// EnemyMeshの参照
	/// </summary>
	[SerializeField]
	private EnemyMesh enemyMesh = null;
	/// <summary>
	/// EnemyMoveの参照
	/// </summary>
	[SerializeField]
	private EnemyMove enemyMove = null;
	/// <summary>
	/// EnemyShotの参照
	/// </summary>
	[SerializeField]
	private EnemyShot enemyShot = null;

    //列id
	private int id = -1;
	public int Id => id;
	//横移動量
	private float moveHorizontalAmount = 0;
	private float moveVerticalAmount = 0;
	private bool isFacingRight = true;

	private Vector3 minPos = Vector3.zero;
	private Vector3 maxPos = Vector3.zero;

	private bool isDead = false;
	public bool IsDead => isDead;

	void Awake()
	{
		isFacingRight = true;
	}
	
	/// <summary>
	/// Enemyの初期設定を行う
	/// </summary>
	/// <param name="id">行id(左から0)/param>
	/// <param name="point">死んだ際に与えられるポイント</param>
	/// <param name="onAddScore">死んだ際にスコア加算するデリゲートメソッド</param>
	/// <param name="onDeath">死んだ際に呼ぶデリゲートメソッド</param>
	/// <param name="moveHorizontalAmount">一回の移動での左右への移動量</param>
	/// <param name="moveVerticalAmount">一回の移動での前への移動量</param>
	/// <param name="minPos">Enemyの移動可能領域の左下の位置</param>
	/// <param name="maxPos">Enemyの移動可能領域の右上の位置</param>
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

	/// <summary>
	/// 次に移動の際に、横に移動可能か
	/// </summary>
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

	/// <summary>
	/// 横に移動する
	/// </summary>
	public void MoveSide()
	{
		float moveSign = isFacingRight ? 1 : -1;
		enemyMove.Move(moveSign * new Vector3(moveHorizontalAmount, 0, 0));
		enemyMesh.ChangeMesh();
	}

	/// <summary>
	/// 前に移動する
	/// </summary>
	public void MoveBefore()
	{
		isFacingRight = !isFacingRight;
		enemyMove.Move(new Vector3(0, -moveVerticalAmount, 0));
		enemyMesh.ChangeMesh();
	}

	/// <summary>
	/// 弾を発射する
	/// </summary>
	/// <param name="bullet"></param>
	public void Shot(GameObject bullet)
	{
		enemyShot.Shot(transform.position + shotPos, bullet);
	}
}
