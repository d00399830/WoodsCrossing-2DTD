using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameProgress : MonoBehaviour
{
    public GameObject viewUI;
    // Start is called before the first frame update
    public static bool gameState;
    void Start()
    {
        viewUI.SetActive(false);
        gameState = true;
    }

    void Update()
    {
        if (gameState == false)
        {
            viewUI.SetActive(true);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame()
    {
        Debug.Log("Huzzah");
        SceneManager.LoadScene("MainScene");
    }
}
