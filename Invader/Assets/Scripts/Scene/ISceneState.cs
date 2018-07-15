using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 各シーンがロード・破棄された際に実行するメソッド群
/// </summary>
public interface ISceneState {
	/// <summary>
	/// シーンロード後に実行されるメソッド
	/// </summary>
	void LoadedScene ();
	/// <summary>
	/// シーン破棄後に実行されるメソッド
	/// </summary>
	void RemoveSceneBefore ();
}
