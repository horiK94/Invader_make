using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TitleController : MonoBehaviour {
	/// <summary>
	/// エンターキーが押された時に行う処理
	/// </summary>
	UnityAction onEnterKeyDown = null;
	/// <summary>
	/// エンターキーが押された時に行う処理
	/// </summary>
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
		if (Input.GetKeyDown(KeyCode.Space)) {
			OnEnterKeyDown ();
		}
	}
}
