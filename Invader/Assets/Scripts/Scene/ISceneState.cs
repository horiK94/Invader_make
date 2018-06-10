using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISceneState {
	void LoadedScene ();		//シーンロード時
	void RemoveScene ();		//シーン破棄時
}
