using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class UFOController : MonoBehaviour
{
    [SerializeField] private GameObject ufoPrefab;
    private GameObject ufo = null;
    private UFOMover ufoMover;
    private UFOHealth ufoHelath;
    [SerializeField] private float ufoWidth;
    [SerializeField] private float ufoStartPosY;
    [SerializeField] private float interval = 25;
    private float cornerPosX;
    private UnityAction<int> onAddScore;

    public UnityAction<int> OnAddScore
    {
        get { return onAddScore; }
        set { onAddScore = value; }
    }

    private UnityAction onDeath;

    public UnityAction OnDeath
    {
        get { return onDeath; }
        set { onDeath = value; }
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

    private void Start()
    {
        ufoHelath.OnAddScore = this.OnAddScore;
        ufoHelath.OnDeath += () => { this.OnDeath(); };
        StartCoroutine(Move());
    }

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

    public void ActiveUfo()
    {
        bool isRight = Random.Range(0, 2) == 0 ? true : false;
        float sign = isRight ? 1 : -1;
        
        ufo.transform.position = new Vector3(sign * (cornerPosX + ufoWidth), ufoStartPosY, 0);
        float otherCornerPosX = -sign * (cornerPosX + ufoWidth);

        ufoMover.PrepareToMove(-sign, otherCornerPosX, () =>
        {
            ufo.SetActive(false);
        });
        ufo.SetActive(true);
    }
}
