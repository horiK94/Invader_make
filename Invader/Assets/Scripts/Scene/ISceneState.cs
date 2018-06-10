using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISceneState {
	void LoadedScene ();		//シーンロード後
	void RemoveSceneBefore ();		//シーン破棄時
}
