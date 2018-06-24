using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	[SerializeField]GameObject playerPrefab;
	[SerializeField]Vector3 startPosUnderDiff;

	private GameObject player;
	private PlayerMover playerMover;
	private PlayerHealth playerHealth;
	private PlayerShot playerShot;

	public void BootUp(Vector3 minPos)
	{
		player = Instantiate (playerPrefab, new Vector3(0, minPos.y, 0) + startPosUnderDiff, Quaternion.identity);
		playerMover = player.GetComponent<PlayerMover>();
		playerHealth = player.GetComponent<PlayerHealth>();
		playerShot = player.GetComponent<PlayerShot>();
		
		playerMover.Set(Mathf.Abs(minPos.x));
	}

	void Update()
	{
		playerMover.Move(Input.GetAxis("Horizontal"));
		if (Input.GetKeyDown(KeyCode.Space))
		{
			playerShot.Shot();
		}
	}	
}
