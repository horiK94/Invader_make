using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneState : MonoBehaviour, ISceneState {
	/// <summary>
	/// シーン名
	/// </summary>
	protected string sceneTitle = "";
	/// <summary>
	/// シーン名(readonly)
	/// </summary>
	public string SceneTitle
	{
		get{
			return sceneTitle;
		}
	}
	/// <summary>
	/// 次のシーンの参照
	/// </summary>
	[SerializeField]protected SceneState nextScene;
	/// <summary>
	/// 次のシーンの参照(readonly)
	/// </summary>
	public SceneState NextScene
	{
		get{
			return nextScene;
		}
	}
	/// <summary>
	/// シーンがロード後呼ばれるメソッド
	/// </summary>
	public virtual void LoadedScene()
	{

	}

	/// <summary>
	/// シーンが破棄される前に呼ばれるメソッド
	/// </summary>
	public virtual void RemoveSceneBefore()
	{

	}
		
	/// <summary>
	/// シーンをロードする
	/// </summary>
	public void LoadScene()
	{
		StartCoroutine (Load());
	}

	/// <summary>
	/// 非同期でシーンをロードする
	/// </summary>
	IEnumerator Load()
	{
		AsyncOperation ope = SceneManager.LoadSceneAsync (sceneTitle);
		while (true) {
			if (ope.isDone) {		//シーンのロード完了
				this.LoadedScene ();
				yield break;
			}
			yield return null;
		}
	}

	/// <summary>
	/// 現在のシーンを破棄、nextSceneをロードする
	/// </summary>
	public void LoadNextScene()
	{
		NextScene.LoadScene ();
		RemoveScene ();
	}

	/// <summary>
	/// シーンを破棄する前に呼ばれる
	/// </summary>
	void RemoveScene()
	{
		StartCoroutine (Remove ());
	}

	/// <summary>
	/// 非同期でシーンを破棄する
	/// </summary>
	IEnumerator Remove()
	{
		yield return SceneManager.UnloadSceneAsync (sceneTitle);
	}
}
