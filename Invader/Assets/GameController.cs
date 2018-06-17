using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private EnemyController enemyController;
    // TODO UIControllerの追加
    private UnityAction<int> onAddScore;

    public UnityAction<int> OnAddScore
    {
        get { return onAddScore; }
        set { onAddScore = value; }
    }

    private void Awake()
    {
        OnAddScore = (score) =>
        {
            //TODO UIControlllerに伝達
        };
    }
}
