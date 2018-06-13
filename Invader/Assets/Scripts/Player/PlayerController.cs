using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	[SerializeField]GameObject player;
	[SerializeField]Vector3 startPos;
	void OnEnable()
	{
		Instantiate (player, startPos, Quaternion.identity);
	}
}
