using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class DebugPlayer : DebugManager
{
#if UNITY_EDITOR
    [SerializeField]
    PlayerController playerController = null;

    [SerializeField]
    Button playerInvincibleButton = null;

    protected void Start()
    {
        base.Start();
    }

    protected void Update()
    {
        base.Update();
    }

    /// <summary>
    /// 初期化
    /// </summary>
    protected override void Init()
    {
        playerInvincibleButton.gameObject.SetActive(false);

        playerInvincibleButton.onClick.AddListener(InvalidatePlayerDamage);
    }

    /// <summary>
    /// ボタンの表示
    /// </summary>
    protected override void Appear()
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
