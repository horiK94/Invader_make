using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneProcessManager : MonoBehaviour {
	[SerializeField]SceneState  startScene;
	SceneState nowScene;

	[SerializeField]Fade fade;
	[SerializeField]float fadeTime = 1f;

	void Awake()
	{
		DontDestroyOnLoad (gameObject);
	}

	void Start()
	{
		LoadStart ();
	}

	void LoadStart()
	{
		nowScene = startScene;
		nowScene.LoadScene ();
	}

	void LoadNextScene()
	{
		nowScene = nowScene.NextScene;
	}
}
