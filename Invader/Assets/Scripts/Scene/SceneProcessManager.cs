using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneProcessManager : MonoBehaviour {
	[SerializeField]float loadTime = 3;
	SceneState nowScene;
	SceneState startScene;

	void Awake()
	{
		nowScene = startScene;
	}

	void LoadNextScene()
	{
		StartCoroutine (LoadScene());
	}

	IEnumerator LoadScene()
	{
		AsyncOperation ope = SceneManager.LoadSceneAsync (nowScene.NextScene.SceneTitle);
		ope.allowSceneActivation = false;

		float startTime = Time.realtimeSinceStartup;
		while (Time.realtimeSinceStartup - startTime < loadTime) {
			yield return null;
		}

		ope.allowSceneActivation = true;
	}
}
