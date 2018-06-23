using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private EnemyController enemyController;
    /// <summary>
    /// 移動制限の幅
    /// </summary>
    [SerializeField] private float limitWidth;
    
    // TODO UIControllerの追加
    private Vector2 minPos = Vector2.zero, maxPos = Vector2.zero;

    void Awake()
    {
        maxPos = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, transform.position.z - Camera.main.transform.position.z)) - new Vector3(limitWidth, 0, 0);
        minPos = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, transform.position.z - Camera.main.transform.position.z)) + new Vector3(limitWidth, 0, 0);
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
        
        enemyController.enabled = true;
        playerController.enabled = true;

        enemyController.BootUp(OnAddScore, OnDeath, maxPos, minPos);
        playerController.BootUp(maxPos.x - limitWidth);
    }
}
