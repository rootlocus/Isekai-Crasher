using UnityEngine;
using TMPro;

public class QuotaUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI quoteText;

    public void SetQuota(int quota, int maximumQuota)
    {
        quoteText.SetText(quota.ToString() + "/" + maximumQuota.ToString());
    }
}
