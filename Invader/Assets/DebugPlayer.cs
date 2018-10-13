using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class DebugPlayer : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField]
    PlayerController playerController = null;

    [SerializeField]
    Button playerInvincibleButton = null;

    /// <summary>
    /// 初期化
    /// </summary>
    public void Init()
    {
        playerInvincibleButton.gameObject.SetActive(false);

        playerInvincibleButton.onClick.AddListener(InvalidatePlayerDamage);
    }

    /// <summary>
    /// ボタンの表示
    /// </summary>
    public void Appear()
    {
        playerInvincibleButton.gameObject.SetActive(true);
    }

    /// <summary>
    /// プレイヤーのダメージの無効化
    /// </summary>
    void InvalidatePlayerDamage()
    {
        Assert.IsNotNull(playerController, "playerControllerがアタッチされていません");

        playerController.InvalidatePlayerDamage();
    }
#endif
}
