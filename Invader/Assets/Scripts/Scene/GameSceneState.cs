using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneState : SceneState {
	void Awake()
	{
		sceneTitle = "Game";
		Debug.Log("Game");
	}

	public override void LoadedScene()
	{
		
	}

	public override void RemoveSceneBefore()
	{

	}

}
