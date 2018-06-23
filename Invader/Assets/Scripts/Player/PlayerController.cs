using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	[SerializeField]GameObject playerPrefab;
	[SerializeField]Vector3 startPos;

	private GameObject player;
	private PlayerMover playerMover;
	private PlayerHealth playerHealth;
	private PlayerShot playerShot;

	public void BootUp(float _limitWorldPosX)
	{
		player = Instantiate (playerPrefab, startPos, Quaternion.identity);
		playerMover = player.GetComponent<PlayerMover>();
		playerHealth = player.GetComponent<PlayerHealth>();
		playerShot = player.GetComponent<PlayerShot>();
		
		playerMover.Set(_limitWorldPosX);
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
