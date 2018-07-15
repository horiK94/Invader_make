using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲームシーンのロード・破棄された際に実行するメソッド群
/// </summary>
public class TitleSceneState : SceneState {
	/// <summary>
	/// TitleシーンでEnterキーを押した時の処理を行うプレファブ
	/// </summary>
	[SerializeField]GameObject title = null;

	void Awake()
	{
		sceneTitle = "Title";
	}

	public override void LoadedScene()
	{
		GameObject gb = Instantiate (title) as GameObject;
		TitleController t = gb.AddComponent<TitleController> ();
		t.OnEnterKeyDown = this.LoadNextScene;
	}

	public override void RemoveSceneBefore()
	{

	}
}
