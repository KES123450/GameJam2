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
    [SerializeField] [Range(0f, 5f)] public float playTimeScale;

    private void Start()
    {
        Time.timeScale = playTimeScale;
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
        Time.timeScale = playTimeScale;
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
                Time.timeScale = playTimeScale;
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
