using UnityEngine;
using System.Collections;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int maximumQuota = 3;
    [SerializeField] private int incrementTimer = 5;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private RecruitManager recruitManager;
    [SerializeField] private QuotaUI quotaUI;
    [SerializeField] private LevelUI levelUI;
    [SerializeField] private ScoreUI scoreUI;
    [SerializeField] private TitleUI titleUI;
    [SerializeField] private InstructionUI instructionUI;
    private int currentScore = 0;
    private int currentQuota = 0;
    private int currentLevel = 1;
    private bool hasHitQuota = false;
    public float levelTimer = 0;
    public static event Action<float> startGameEvent;
    public static event Action restartGameEvent;
    private bool isReady = false;
    private bool gameStarted = false;

    private void OnEnable() {
        PlayerController.recruitIsekaiEvent += AddToQuota;
        LevelPromptUI.restartLevelClickEvent += RestartLevel;
        LevelPromptUI.goNextLevelClickEvent += StartNextLevel;
        DirectionManager.AddToScoreEvent += AddToScore;
    }

    private void OnDisable() {
        PlayerController.recruitIsekaiEvent -= AddToQuota;
        LevelPromptUI.restartLevelClickEvent -= RestartLevel;
        LevelPromptUI.goNextLevelClickEvent -= StartNextLevel;
        DirectionManager.AddToScoreEvent -= AddToScore;
    }

    private void Awake()
    {
        quotaUI = GameObject.Find("QuotaUI").GetComponent<QuotaUI>();
        levelUI = GameObject.Find("LevelUI").GetComponent<LevelUI>();
        scoreUI = GameObject.Find("ScoreUI").GetComponent<ScoreUI>();
        titleUI = GameObject.Find("TitleUI").GetComponent<TitleUI>();
        instructionUI = GameObject.Find("InstructionUI").GetComponent<InstructionUI>();
        recruitManager = GameObject.Find("RecruitManager").GetComponent<RecruitManager>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (!gameStarted) {
                titleUI.CloseUI();
                isReady = true;
                gameStarted = true;
            } else if (isReady) {
                currentQuota = 0;
                quotaUI.SetQuota(currentQuota, maximumQuota);
                StartCoroutine(CoStartLevel());
            }
        }
    }


    IEnumerator CoStartLevel()
    {
        isReady = false;

        yield return new WaitForSeconds(0.5f);

        playerController.ResetPosition();
        instructionUI.ShowCastInstruction();
        hasHitQuota = false;
        startGameEvent?.Invoke(levelTimer);
        recruitManager.NextRecruit();
    }
 
    IEnumerator CoRestartLevel()
    {
        playerController.ResetPosition();
        currentLevel = 1;
        maximumQuota = 3;
        levelTimer = 15;
        currentQuota = 0;
        scoreUI.SetScore(0);
        quotaUI.SetQuota(currentQuota, maximumQuota);
        levelUI.SetLevel(currentLevel);
        restartGameEvent?.Invoke();
        instructionUI.ShowHitInstruction();

        yield return new WaitForSeconds(0.2f);
        
        isReady = true;
    }

    IEnumerator CoStartNextLevel() 
    {
        currentLevel += 1;
        currentQuota = 0;
        maximumQuota += currentLevel;
        levelTimer += incrementTimer;
        quotaUI.SetQuota(currentQuota, maximumQuota);
        levelUI.SetLevel(currentLevel);
        instructionUI.ShowHitInstruction();

        yield return new WaitForSeconds(0.2f);
        
        isReady = true;
    }

    public void AddToScore(int score)
    {
        currentScore += score;
        scoreUI.SetScore(currentScore);
    }

    public bool GetHasHitQuota()
    {
        return hasHitQuota;
    }

    private void RestartLevel()
    {
        StartCoroutine(CoRestartLevel());
    }


    private void StartNextLevel()
    {
        StartCoroutine(CoStartNextLevel());
    }

    private void AddToQuota()
    {
        currentQuota += 1;
        quotaUI.SetQuota(currentQuota, maximumQuota);
        if (currentQuota == maximumQuota) {
            hasHitQuota = true;
        }
    }

}
