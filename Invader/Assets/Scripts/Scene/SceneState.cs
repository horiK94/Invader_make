using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneState : MonoBehaviour, ISceneState {
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

	/// <summary>
	/// Call when completed to load Scene.
	/// </summary>
	public virtual void LoadedScene()
	{

	}

	/// <summary>
	/// Call when remove scene before.
	/// </summary>
	public virtual void RemoveSceneBefore()
	{

	}
		
	/// <summary>
	/// Loads the scene.
	/// </summary>
	public void LoadScene()
	{
		StartCoroutine (Load());
	}

	IEnumerator Load()
	{
		AsyncOperation ope = SceneManager.LoadSceneAsync (sceneTitle);
		while (true) {
			if (ope.isDone) {
				this.LoadedScene ();
				yield break;
			}
			yield return null;
		}
	}

	/// <summary>
	/// Loads the next scene.
	/// </summary>
	public void LoadNextScene()
	{
		NextScene.LoadScene ();
		RemoveScene ();
	}

	/// <summary>
	/// Call when remove scene before.
	/// </summary>
	void RemoveScene()
	{
		StartCoroutine (Remove ());
	}

	IEnumerator Remove()
	{
		yield return SceneManager.UnloadSceneAsync (sceneTitle);
	}
}
