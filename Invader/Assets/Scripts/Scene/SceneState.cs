using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneState : ISceneState {
	protected string sceneTitle = "";
	public string SceneTitle
	{
		get{
			return sceneTitle;
		}
	}
	[SerializeField]protected SceneState nextScene;
	public SceneState NextScene
	{
		get{
			return nextScene;
		}
	}

	protected void ShowAnimation()
	{

	}

	public void LoadedScene()
	{

	}

	public void RemoveScene()
	{
		
	}

	//アニメーション終了後
	protected virtual void ShowedAnimation()
	{

	}
}
