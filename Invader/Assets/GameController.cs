using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private EnemyController enemyController;
    // TODO UIControllerの追加
    private Vector2 minPos, maxPos;

    void Awake()
    {
        minPos = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, transform.position.z - Camera.main.transform.position.z));
        maxPos = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, transform.position.z - Camera.main.transform.position.z));
    }

    private void Start()
    {
        UnityAction<int> OnAddScore = (score) =>
        {
            Debug.Log("Score is " + score);
        };
        UnityAction OnDeath = () =>
        {
            //Sceneの切り替え
            //UIの表示
        };
        
        enemyController.BootUp(OnAddScore, OnDeath);
        enemyController.enabled = true;
        
        playerController.MaxScreenPosX = maxPos.x;
        playerController.enabled = true;
    }
}
