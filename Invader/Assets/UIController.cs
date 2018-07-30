using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    /// <summary>
    /// スコア表示のクラスへの参照
    /// </summary>
    [SerializeField]
    private ScoreViewer scoreViewer = null;

    public void SetScore(int score)
    {
        scoreViewer.SetScore(score);
    }
}
