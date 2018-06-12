using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TitleView : MonoBehaviour {
	UnityAction onEnterKeyDown = null;
	public UnityAction OnEnterKeyDown
	{
		get{
			return onEnterKeyDown;
		}
		set{
			onEnterKeyDown = value;
		}
	}
	
	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Return)) {
			OnEnterKeyDown ();
		}
	}
}
