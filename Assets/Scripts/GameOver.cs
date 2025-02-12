using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject AP;
    private int turnChoice;

    void OnEnable()
    {
        turnChoice = 1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            turnChoice++;
            if (turnChoice > 2)
            {
                turnChoice = 1;
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            turnChoice--;
            if (turnChoice < 1)
            {
                turnChoice = 2;
            }
        }
        switch (turnChoice)
        {
            case 1:
                AP.transform.position = new Vector3(-4.49f, -1.32f, 0);
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
                }
                break;
            case 2:
                AP.transform.position = new Vector3(-1.77f, -3.07f, 0);
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    SceneManager.LoadScene("Credits", LoadSceneMode.Single);
                }
                break;
        }
    }
}
