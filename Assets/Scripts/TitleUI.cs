using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleUI : MonoBehaviour
{
    [SerializeField] private GameObject titleUI;

    public void CloseUI()
    {
        titleUI.SetActive(false);
    }
}
