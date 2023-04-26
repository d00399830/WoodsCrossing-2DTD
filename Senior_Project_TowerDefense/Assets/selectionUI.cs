using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectionUI : MonoBehaviour
{
    public GameObject viewUI;

    private TowerSystem tower;

    public void Start()
    {
        hide();
    }

    public void SetTarget(TowerSystem towerVar)
    {
        if (towerVar == tower)
        {
            tower = null;
            hide();
            return;
        }

        this.tower = towerVar;
        viewUI.SetActive(true);
    }

    public void UpgradeTower()
    {
        if (tower != null && MoneySystem.Money >= 100)
        {
            tower.upgradeTower();
            MoneySystem.deductCash(100);
        }
    }

    public void SellTower()
    {
        if (tower != null)
        {
            tower.sellTower();
        }
        hide();
    }

    public void hide()
    {
        viewUI.SetActive(false);
    }
}
