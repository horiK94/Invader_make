using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverViewer : MonoBehaviour
{
    [SerializeField]
    private Text text = null;

    private string gameOverWord = "Game Over";

    private void Awake()
    {
        text.enabled = false;
    }

    public void ShowGameOver()
    {
        text.text = gameOverWord;
        text.enabled = true;
    }
}