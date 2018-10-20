using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugEnemy : DebugManager
{
#if UNITY_EDITOR
    [SerializeField]
    private Button enemyAnhilitionButton = null;
    [SerializeField]
    private EnemyCrowdController enemyCrowdController = null;

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
        enemyAnhilitionButton.gameObject.SetActive(false);
        enemyAnhilitionButton.onClick.AddListener(AnnihiliatEnemy);
    }

    /// <summary>
    /// 表示
    /// </summary>
    protected override void Appear()
    {
        enemyAnhilitionButton.gameObject.SetActive(true);
    }

    /// <summary>
    /// 敵を全滅させる
    /// </summary>
    void AnnihiliatEnemy()
    {
        enemyCrowdController.KillAllEnemy();
    }
#endif
}
