using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲームシーンのロード・破棄された際に実行するメソッド群
/// </summary>
public class GameSceneState : SceneState {
	void Awake()
	{
        sceneName = "Game";
	}

	public override void LoadedScene()
	{
		
	}

	public override void RemoveSceneBefore()
	{

	}
}
