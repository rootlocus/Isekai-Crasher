using UnityEngine;
using TMPro;

public class TargetController : MonoBehaviour
{
    private Animator animator;
    private BoxCollider2D targetCollider;
    private TextMeshProUGUI difficultyCounter;
    private DirectionManager directionManager;

    private void OnEnable() {
        TimerController.timerTimedOutEvent += RecruitEscaped;
        DirectionManager.UpdateDifficultyCounter += SetDifficultyCounter;
    }

    private void OnDisable() {
        TimerController.timerTimedOutEvent -= RecruitEscaped;
        DirectionManager.UpdateDifficultyCounter -= SetDifficultyCounter;
    }

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        targetCollider = GetComponent<BoxCollider2D>();
        difficultyCounter = GetComponentInChildren<TextMeshProUGUI>();
        directionManager = GameObject.Find("DirectionManager").GetComponent<DirectionManager>();
        SetDifficultyCounter(directionManager.GetDifficultyCountLeft());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") {
            targetCollider.enabled = false;
            animator.SetTrigger("Smashed");
        }
    }

    private void RecruitEscaped()
    {
        targetCollider.enabled = false;
        animator.SetTrigger("Escaped");
    }

    private void SetDifficultyCounter(int count)
    {
        difficultyCounter.SetText(count.ToString());
    }
}
