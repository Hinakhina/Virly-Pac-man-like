using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Pickable : MonoBehaviour
{
    [SerializeField] public PickableType PickableType;
    public Action<Pickable> OnPicked;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player")){
            // Debug.Log("PickUp: " + PickableType);
            OnPicked(this);
            Destroy(gameObject);
        }
    }
}
