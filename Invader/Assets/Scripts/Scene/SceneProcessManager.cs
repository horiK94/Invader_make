using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// シーンの進行に関するクラス
/// </summary>
public class SceneProcessManager : MonoBehaviour {
	/// <summary>
	/// 最初にロードするシーン
	/// </summary>
	[SerializeField]SceneState  startScene = null;
	/// <summary>
	/// 現在のシーン
	/// </summary>
	SceneState nowScene = null;
	/// <summary>
	/// フェードイン, フェードアウトを行うオブジェクトの参照
	/// </summary>
	[SerializeField]Fade fade = null;
	/// <summary>
	/// フェードイン・フェードアウトをする時間
	/// </summary>
	[SerializeField]float fadeTime = 1f;

	void Awake()
	{
		DontDestroyOnLoad (gameObject);
	}

	void Start()
	{
		LoadStart ();
	}

	/// <summary>
	/// 最初のシーンをロード
	/// </summary>
	void LoadStart()
	{
		//最初のシーンをロード
		nowScene = startScene;
		nowScene.LoadScene ();
	}
}
