using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceTowers : MonoBehaviour
{
    public GameObject towerToPlace;

    void OnMouseDown()
    {
        if (towerToPlace == null) 
            return;
    }


}
