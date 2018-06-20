using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private EnemyController enemyController;
    // TODO UIControllerの追加

    private void Awake()
    {
        enemyController.OnAddScore = (score) =>
        {
            Debug.Log("Score is " + score);
        };
        enemyController.OnDeath = () =>
        {
            //Sceneの切り替え
            //UIの表示
        };
        enemyController.enabled = true;
        playerController.enabled = true;
    }
}
