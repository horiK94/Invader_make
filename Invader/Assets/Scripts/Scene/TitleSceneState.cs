using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSceneState : SceneState {
	[SerializeField]GameObject title;

	void Awake()
	{
		sceneTitle = "Title";
	}

	public override void LoadedScene()
	{
		GameObject gb = Instantiate (title) as GameObject;
		Title t = gb.AddComponent<Title> ();
		t.onEnterKeyDown = this.LoadNextScene;
	}

	public override void RemoveSceneBefore()
	{

	}
}
