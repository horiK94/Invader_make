using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

/// <summary>
/// UFOのController
/// </summary>
public class UFOController : MonoBehaviour
{
    /// <summary>
    /// UFOのプレファブ
    /// </summary>
    [SerializeField]
    private GameObject ufoPrefab = null;
    /// <summary>
    /// UFOのインスタンス
    /// </summary>
    private GameObject ufo = null;
    /// <summary>
    /// UFOのインスタンスにアタッチされたUFOMoverの参照
    /// </summary>
    private UFOMover ufoMover = null;
    /// <summary>
    /// UFOのインスタンスにアタッチされたUFOHealthの参照
    /// </summary>
    private UFOHealth ufoHelath = null;
    /// <summary>
    /// UFOの幅
    /// </summary>
    [SerializeField]
    private float ufoWidth = 0;
    /// <summary>
    /// UFOが生成される位置yは画面上端からどのくらい離れているか
    /// </summary>
    [SerializeField] 
    private float ufoPosYDiff = 0;
    /// <summary>
    /// UFOが出現する時間感覚
    /// </summary>
    [SerializeField]
    private float interval = 25;
    /// <summary>
    /// 画面端の位置x
    /// </summary>
    private float cornerPosX = 0;
    /// <summary>
    /// 倒した時にポイント加算をするメソッド
    /// </summary>
    private UnityAction<int> onAddScore = null;
    /// <summary>
    /// 画面右上端の位置
    /// </summary>
    private Vector3 maxPos = Vector3.zero;

    public UnityAction<int> OnAddScore
    {
        get { return onAddScore; }
        set { onAddScore = value; }
    }

    void Awake()
    {
        if (ufo == null)
        {
            ufo = Instantiate(ufoPrefab);
        }

        ufoMover = ufo.GetComponent<UFOMover>();
        ufoHelath = ufo.GetComponent<UFOHealth>();
        cornerPosX = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, ufo.transform.position.z - Camera.main.transform.position.z)).x;
    }

    /// <summary>
    /// 初期設定
    /// </summary>
    public void BootUp(UnityAction<int> _onAddScore, Vector3 _maxPos)
    {
        this.maxPos = _maxPos;
        ufoHelath.OnAddScore = _onAddScore;
        StartCoroutine(Move());
    }

    /// <summary>
    /// 移動
    /// </summary>
    /// <returns></returns>
    IEnumerator Move()
    {
        while (true)        //TODO Enemyが一定数数以下になった場合に、bool変数でwhileを抜けるようにしても良いかも
        {
            if (ufo.activeSelf)
            {
                yield return null;
                continue;
            }
            yield return new WaitForSeconds(interval);
            ActiveUfo();
        }
    }

    /// <summary>
    /// UFOを稼働する
    /// </summary>
    public void ActiveUfo()
    {
        bool isRight = Random.Range(0, 2) == 0 ? true : false;
        float sign = isRight ? 1 : -1;
        
        ufo.transform.position = new Vector3(sign * (cornerPosX + ufoWidth), this.maxPos.y - ufoPosYDiff, 0);
        float otherCornerPosX = -sign * (cornerPosX + ufoWidth);

        ufoMover.PrepareToMove(-sign, otherCornerPosX, () =>
        {
            ufo.SetActive(false);
        });
        ufo.SetActive(true);
    }
}
