using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyTrigger : MonoBehaviour
{
    private UnityAction<Collider> myTriggerEnter;
    
    public void SetUp(UnityAction<Collider> myTriggerEnter)
    {
        this.myTriggerEnter = myTriggerEnter;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        this.myTriggerEnter(other);
    }
}
