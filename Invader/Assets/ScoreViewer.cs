using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreViewer : MonoBehaviour {
    /// <summary>
    /// スコアの数字のテキストの参照
    /// </summary>
    [SerializeField] private Text scoreText;
    
    /// <summary>
    /// scoreの表示フォーマット
    /// </summary>
    private readonly string scoreFormat = "D5";

    public void SetScore(int score)
    {
        scoreText.text = score.ToString(scoreFormat);
    }
}
