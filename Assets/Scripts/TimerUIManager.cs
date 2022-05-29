using UnityEngine;
using TMPro;

public class TimerUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timer;
    [SerializeField] private Animator animator;

    private void OnEnable() {
        DirectionManager.InvalidInputEvent += TriggerPenalty;
    }

    private void OnDisable() {
        DirectionManager.InvalidInputEvent += TriggerPenalty;
    }

    private void Awake()
    {
        timer = GetComponentInChildren<TextMeshProUGUI>();
        animator = GetComponentInChildren<Animator>();
    }

    public void UpdateTime(string time)
    {
        timer.SetText(time);
    }

    public void TriggerPenalty()
    {
        animator.SetTrigger("Penalty");
    }
}
