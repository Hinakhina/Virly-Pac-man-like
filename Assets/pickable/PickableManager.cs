using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableManager : MonoBehaviour
{
    [SerializeField] Player Player;
    private List<Pickable> pickableList = new List<Pickable>();

    private int point;
    void Start()
    {
        InitPickableList();
    }

    private void InitPickableList()
    {
        Pickable[] pickableObjects = GameObject.FindObjectsOfType<Pickable>();
        for (int i = 0; i < pickableObjects.Length; i++)
        {
            pickableList.Add(pickableObjects[i]);
            pickableObjects[i].OnPicked += OnPickablePicked;
            
        }
        Debug.Log("Pickable List: " + pickableList.Count);

    }

    private void OnPickablePicked(Pickable pickable)
    {
        pickableList.Remove(pickable);
        if(pickable.PickableType == PickableType.PowerUp)
        {
            Player?.PickPowerUp();
        }
        // Debug.Log("Pickable List: " + pickableList.Count);
        if(pickableList.Count <= 0)
        {
            Debug.Log("Win");
        }
    }

}
