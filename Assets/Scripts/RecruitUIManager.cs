using UnityEngine;
using TMPro;

public class RecruitUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textLabel;
    private RecruitScriptableObject currentRecruit;

    private void OnEnable() {
        RecruitManager.nextRecruitEvent += DisplayNewRecruit;
    }

    private void OnDisable() {
        RecruitManager.nextRecruitEvent -= DisplayNewRecruit;
    }

    private void DisplayNewRecruit(RecruitScriptableObject recruit)
    {
        currentRecruit = recruit;
        textLabel.SetText(recruit.prefabName);
    }
}
