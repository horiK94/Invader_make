using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UFOMover : MonoBehaviour
{
    [SerializeField] private float speed;
    private UnityAction onArriveCorner = null;
    private float moveSign = 1;
    private float cornerPosX = 0;
    
    
    public void PrepareToMove(float sign, float cornerPosX, UnityAction onArriveCorner)
    {
        if (!gameObject.activeSelf)
        {
            moveSign = sign;
            this.cornerPosX = cornerPosX;
            this.onArriveCorner = onArriveCorner;
        }
    }

    private void Update()
    {
        transform.position += new Vector3(moveSign * Time.deltaTime * speed, 0, 0);
        if (moveSign > 0 && transform.position.x > cornerPosX)
        {
            onArriveCorner();
        }
        else if (moveSign < 0 && transform.position.x < cornerPosX)
        {
            onArriveCorner();
        }
    }
}
