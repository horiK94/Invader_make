using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	[SerializeField]GameObject playerPrefab;
	[SerializeField]Vector3 startPos;
	[SerializeField] private float playerWidth = 4;

	private float maxLimitPosX = 0;

	private GameObject player;
	private PlayerMover playerMover;
	private PlayerHealth playerHealth;
	private PlayerShot playerShot;

	void Awake()
	{
		player = Instantiate (playerPrefab, startPos, Quaternion.identity);
		playerMover = player.GetComponent<PlayerMover>();
		playerHealth = player.GetComponent<PlayerHealth>();
		playerShot = player.GetComponent<PlayerShot>();
		
		maxLimitPosX = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, player.transform.position.z - Camera.main.transform.position.z)).x - playerWidth;
	}

	void Update()
	{
		playerMover.Move(Input.GetAxis("Horizontal"), maxLimitPosX);
		if (Input.GetKeyDown(KeyCode.Space))
		{
			playerShot.Shot();
		}
	}	
}
