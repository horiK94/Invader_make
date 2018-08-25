using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

/// <summary>
/// シーンのロードやアンロード時の処理に関するクラス
/// </summary>
public abstract class SceneState : MonoBehaviour, ISceneState {
	/// <summary>
	/// シーン名
	/// </summary>
    protected string sceneName = "";
	/// <summary>
	/// シーン名(readonly)
	/// </summary>
	public string SceneName
	{
		get{
			return sceneName;
		}
	}
	/// <summary>
	/// 次のシーンの参照
	/// </summary>
	[SerializeField]
    protected SceneState nextScene = null;
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
    public abstract void LoadedScene();

    /// <summary>
    /// シーンが破棄される前に呼ばれるメソッド
    /// </summary>
    public abstract void RemoveSceneBefore();
}
