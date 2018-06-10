using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Title : MonoBehaviour {
	public Action onEnterKeyDown;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space)) {
			onEnterKeyDown ();
		}
	}
}
