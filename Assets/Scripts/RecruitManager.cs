using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class RecruitManager : MonoBehaviour
{
    private int size;
    private Vector3 spawnPoint;
    private bool stopRecruit = false;
    [SerializeField] private RecruitScriptableObject currentRecruit;
    [SerializeField] private List<RecruitScriptableObject> recruitCatalogue;
    [SerializeField] private GameObject spawnPointGO;
    public static event Action<RecruitScriptableObject> nextRecruitEvent;

    private void OnEnable() {
        PlayerController.playerReturnEvent += NextRecruit;
        TimerController.timerTimedOutEvent += ToggleRecruiter;
        LevelPromptUI.restartLevelClickEvent += ToggleRecruiter;
        LevelPromptUI.goNextLevelClickEvent += ToggleRecruiter;
    }

    private void OnDisable() {
        PlayerController.playerReturnEvent -= NextRecruit;
        TimerController.timerTimedOutEvent -= ToggleRecruiter;
        LevelPromptUI.restartLevelClickEvent -= ToggleRecruiter;
        LevelPromptUI.goNextLevelClickEvent -= ToggleRecruiter;
    }

    private void Awake()
    {
        size = recruitCatalogue.Count;
        spawnPoint = spawnPointGO.transform.position;
    }

    private void ToggleRecruiter()
    {
        stopRecruit = !stopRecruit;
    }

    public void NextRecruit()
    {
        if (stopRecruit) {
            return;
        }

        int random = Random.Range(1, 100);
        if (random >= 90) {
            random = 2;
        } else if (random < 90 && random >= 50) {
            random = 1;
        } else {
            random = 0;
        }

        currentRecruit = recruitCatalogue[random];
        nextRecruitEvent?.Invoke(currentRecruit);
        SpawnTarget(currentRecruit);
    }

    private void SpawnTarget(RecruitScriptableObject recruit) {
        Instantiate(recruit.prefab, spawnPoint, Quaternion.identity);
    }

}
