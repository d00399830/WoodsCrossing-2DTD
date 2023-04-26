using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawners : MonoBehaviour
{
    public static TowerSpawners instance;

    void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }

    public List<GameObject> towers;
    public Transform towersObject;
    int towerID = -1;
    public TowerSystem currentTower;
    public selectionUI Selection;

   void Update()
    {
        
    }

    public void ValidSpawn()
    {
        Vector3 mousePos = new Vector2();
        if (towerID != -1 && MoneySystem.Money >= 100)
        {
            GameObject newTower = Instantiate(towers[towerID], towersObject);
            mousePos = Input.mousePosition;
            newTower.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10));
            TowerDeselect();
            MoneySystem.deductCash(100);
        }
    }

    public void TowerSelect(int id)
    {
        towerID = id;
        currentTower = null;
        
    }

    public void SelectBuiltTower(TowerSystem tower)
    {
        currentTower = tower;
        towerID = -1;
        Selection.SetTarget(tower);
    }

    public void TowerDeselect() { towerID = -1; }
}
