using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //public Button startButton;
    [SerializeField] public GameObject startUI;
    [SerializeField] public GameObject pauseUI;
    bool isPause = false;

    private void Start()
    {
        GameStart();   
    }
    private void Update()
    {
        GamePause();
    }
    public void GameStart()
    {
        Time.timeScale = 0;
        startUI.SetActive(true);
        pauseUI.SetActive(false);
    }

    public void onClickedStartButton()
    {
        Time.timeScale = 1;
        isPause = false;
        pauseUI.SetActive(false);
        startUI.SetActive(false);
    }

    public void GamePause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPause)
            {
                Time.timeScale = 0;
                isPause = true;
                ActiveSettingUI();
                return;
            }
            else
            {
                Time.timeScale = 1;
                isPause = false;
                pauseUI.SetActive(false);
                return;
            }
        }
    }
    
    public void GameExit()
    {
        Application.Quit();
    }

    public void ActiveSettingUI()
    {
        pauseUI.SetActive(true);
        startUI.SetActive(false);
    }
}
