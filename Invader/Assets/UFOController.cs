using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class UFOController : MonoBehaviour
{
    [SerializeField] private GameObject ufoPrefab;
    private GameObject ufo = null;
    private UFOMover ufoMover;
    [SerializeField] private float ufoWidth;
    [SerializeField] private float ufoStartPosY;
    [SerializeField] private float interval = 25;
    private float cornerPosX;

    void Awake()
    {
        if (ufo == null)
        {
            ufo = Instantiate(ufoPrefab);
        }

        ufoMover = ufo.GetComponent<UFOMover>();
        cornerPosX = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, ufo.transform.position.z - Camera.main.transform.position.z)).x;
        Debug.Log(ufoMover);
    }

    private void Start()
    {
        ActiveUfo();
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
