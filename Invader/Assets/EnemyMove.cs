using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour {
    public void Move(Vector3 moveVec)
    {
        transform.position += moveVec;
    }
}
