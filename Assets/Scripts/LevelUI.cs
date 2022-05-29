using UnityEngine;
using TMPro;

public class LevelUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelText;

    public void SetLevel(int level)
    {
        levelText.SetText(level.ToString());
        //Set animation
    }
}
