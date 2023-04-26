using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public GameObject viewUI;
    public GameObject moneyUI;
    public GameObject waveUI;
    public Text healthText;
    public int levelHealth = 10;
   public static int currentHealth;
   // Start is called before the first frame update

   int GetHealth()
   {
       return currentHealth;
   }

   void Start()
   {
       currentHealth = levelHealth;
       viewUI.SetActive(false);
   }

        // For right now, I don't plan on enemies having different damage values, so I am not taking parameters for the method
    public void DealDamage()
    {
        currentHealth -= 1;
        Debug.Log("enemy destroyed");
    }

    void Update()
    {
        healthText.text = "HP " + currentHealth.ToString();
        if (currentHealth <= 0)
        {
            viewUI.SetActive(true);
            moneyUI.SetActive(false);
            waveUI.SetActive(false);
        }
    }
}

