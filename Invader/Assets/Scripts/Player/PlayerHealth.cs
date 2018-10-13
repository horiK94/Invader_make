using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
#if UNITY_EDITOR
    private bool isInvalidation = false;

    /// <summary>
    /// 無効化の処理を設定
    /// </summary>
    /// <param name="flag">If set to <c>true</c> flag.</param>
    public void SetInvalidation(bool flag)
    {
        isInvalidation = flag;
    }
#endif

    /// <summary>
    /// Playerが死んだ時の処理
    /// </summary>
    UnityAction onDeath = null;

	/// <summary>
	/// Playerが死んだ時の処理を登録する
	/// </summary>
	public void SetDeathAction(UnityAction _onDeath){
		this.onDeath += _onDeath;
	}

	void OnTriggerEnter(Collider other)
	{
#if UNITY_EDITOR
        if(isInvalidation)
        {
            //無効化中は当たっても処理を行わない
            return;
        }
#endif

        if (other.GetComponent<Bullet>() != null) {
            //弾が当たったら死ぬ
			onDeath();
		}
	}
}
