using UnityEngine;
using System;

public class LevelPromptUI : MonoBehaviour
{
    [SerializeField] private GameObject successWindow;
    [SerializeField] private GameObject restartWindow;
    private GameManager gameManager;
    public static event Action goNextLevelClickEvent;
    public static event Action restartLevelClickEvent;

    private void OnEnable() {
        TimerController.timerTimedOutEvent += DisplayWindow;
    }

    private void OnDisable() {
        TimerController.timerTimedOutEvent -= DisplayWindow;
    }

    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        successWindow.SetActive(false);
        restartWindow.SetActive(false);
    }

    public void DisplayWindow()
    {
        if (gameManager.GetHasHitQuota())
        {
            successWindow.SetActive(true);
        } else {
            restartWindow.SetActive(true);
        }

    }

    public void HideWindow()
    {
        successWindow.SetActive(false);
        restartWindow.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void NextLevel()
    {
        goNextLevelClickEvent?.Invoke();
        HideWindow();
    }
    
    public void RestartLevel()
    {
        restartLevelClickEvent?.Invoke();
        HideWindow();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (successWindow.activeSelf) {
                NextLevel();
            } else if (restartWindow.activeSelf) {
                RestartLevel();
            }
        } else if(Input.GetKeyDown(KeyCode.Escape)) {
            if (successWindow.activeSelf || restartWindow.activeSelf) {
                QuitGame();
            }
        }
    }
}
