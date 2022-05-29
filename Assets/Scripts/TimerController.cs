using UnityEngine;
using System;

public class TimerController : MonoBehaviour
{
    [SerializeField] private TimerUIManager timerUI;
    [SerializeField] float penalizeTimeAmount = 2f;
    public float timeRemaining = 0f;
    public static event Action timerTimedOutEvent;

    private void OnEnable() {
        DirectionManager.InvalidInputEvent += PenalizeTime;
        GameManager.startGameEvent += SetTime;
    }

    private void OnDisable() {
        DirectionManager.InvalidInputEvent += PenalizeTime;
        GameManager.startGameEvent -= SetTime;
    }

    private void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 0) {
                timerTimedOutEvent?.Invoke();
                timeRemaining = 0f;
            }

            string displayTime = FormatTime(timeRemaining);
            timerUI.UpdateTime(displayTime);
        }
    }

    private void SetTime(float time)
    {
        string displayTime = FormatTime(time);
        timeRemaining = time;
        timerUI.UpdateTime(displayTime);
    }

    private void PenalizeTime()
    {
        if (timeRemaining > 0) {
            timeRemaining = timeRemaining - penalizeTimeAmount;
            if (timeRemaining <= 0) {
                timerTimedOutEvent?.Invoke();
                timeRemaining = 0;
            }
            string displayTime = FormatTime(timeRemaining);
            timerUI.UpdateTime(displayTime);
        }
    }

    private string FormatTime(float timeRemaining)
    {
        return String.Format("{0:0.00}", timeRemaining);
    }
}
