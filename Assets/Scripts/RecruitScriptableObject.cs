using UnityEngine;

[CreateAssetMenu(fileName = "Recruit", menuName = "ScriptableObjects/Recruit", order = 1)]
public class RecruitScriptableObject : ScriptableObject
{
    public string prefabName;
    public int difficultyCount;
    public int score;
    public GameObject prefab;

}
