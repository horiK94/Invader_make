using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : SingletonMonoBehaviour<ScoreManager> {
    private int score = 0; 

    public int GetScore
    {
        get { return score; }
    }

    public int GetHighScore
    {
        get { return PlayerPrefs.GetInt(Dictionary.PlayerPrefsKeyName.HIGHSCORE); }
    }

    protected void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    public void AddScore(int addScore)
    {
        score += addScore;
    }

    public bool IsHighScore()
    {
        int highScore = PlayerPrefs.GetInt(Dictionary.PlayerPrefsKeyName.HIGHSCORE);
        return score > highScore;
    }

    public void SetHighScore()
    {
        if(IsHighScore())
        {
            PlayerPrefs.SetInt(Dictionary.PlayerPrefsKeyName.HIGHSCORE, score);
        }
        else
        {
            Debug.LogWarning("ハイスコアよりも低い値のスコアが引数として代入された");
        }
    }

    private void ClearHighScore()
    {
        PlayerPrefs.SetInt(Dictionary.PlayerPrefsKeyName.HIGHSCORE, 0);
    }
}
