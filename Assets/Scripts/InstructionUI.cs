using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionUI : MonoBehaviour
{
    [SerializeField] private GameObject hitSpace;
    [SerializeField] private GameObject castSpace;

    private void Awake() {
        castSpace.SetActive(false);
        hitSpace.SetActive(true);
    }
    
    public void ShowCastInstruction()
    {
        hitSpace.SetActive(false);
        castSpace.SetActive(true);
    }

    public void ShowHitInstruction()
    {
        hitSpace.SetActive(true);
        castSpace.SetActive(false);
    }
}
