using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class DirectionManagerOld : MonoBehaviour
{
    [SerializeField] public List<Direction> currentDirectionList;
    public static event Action directionGeneratedEvent;

    // private void OnEnable() {
    //     RecruitManager.nextRecruitEvent += GenerateList;
    // }

    // private void OnDisable() {
    //     RecruitManager.nextRecruitEvent -= GenerateList;
    // }

    // spawn arrow once finish generate. maybe add co routine ?
    // public void GenerateList(RecruitScriptableObject recruit)
    // {
    //     currentDirectionList.Clear();
    //     int size = recruit.difficultyCount;
    //     for (int i = 0; i < size; i++) {
    //         Direction randomDir = (Direction) Random.Range(0, 4);
    //         currentDirectionList.Add(randomDir);
    //     }

    //     directionGeneratedEvent?.Invoke();
    // }

    // public List<Direction> GetDirectionList()
    // {
    //     return currentDirectionList;
    // }
}
