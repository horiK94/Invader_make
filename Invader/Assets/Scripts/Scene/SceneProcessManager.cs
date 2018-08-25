using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// SceneStateを元に、シーン管理を行うクラス
/// </summary>
public class SceneProcessManager : SingletonMonoBehaviour<SceneProcessManager> {
    /// <summary>
    /// 初期シーン
    /// </summary>
    [SerializeField]
    SceneState startScene = null;
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

        nowScene = startScene;
	}

    private void Start()
    {
        LoadScene(true);
    }

    /// <summary>
    /// 非同期で次のシーンをロードする
    /// </summary>
    IEnumerator Load(bool isStart = false)
    {
        if(!isStart)
        {
            yield return fade.FadeIn(fadeTime);
        }
        AsyncOperation ope = SceneManager.LoadSceneAsync(isStart ? nowScene.SceneName : nowScene.NextScene.SceneName);
        while (true)
        {
            if (ope.isDone)
            {   
                //シーンのロード完了
                nowScene.LoadedScene();
                if(!isStart)
                {
                    yield return fade.FadeOut(fadeTime);
                }
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
        LoadScene();
        RemoveScene();
        nowScene = nowScene.NextScene;
    }

    /// <summary>
    /// シーンを破棄する前に呼ばれる
    /// </summary>
    void RemoveScene()
    {
        StartCoroutine(Remove());
    }

    /// <summary>
    /// 非同期でシーンを破棄する
    /// </summary>
    IEnumerator Remove()
    {
        yield return SceneManager.UnloadSceneAsync(nowScene.SceneName);
    }

    /// <summary>
    /// シーンをロードする
    /// </summary>
    public void LoadScene(bool isStart = false)
    {
        StartCoroutine(Load(isStart));
    }
}
