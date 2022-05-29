using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class DirectionManager : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private List<Direction> directionList;
    [SerializeField] private GameObject upPrefab;
    [SerializeField] private GameObject downPrefab;
    [SerializeField] private GameObject leftPrefab;
    [SerializeField] private GameObject rightPrefab;
    [SerializeField] private Animator magicAnimator;
    [SerializeField] private string currentDirection;
    [SerializeField] public static event Action InvalidInputEvent;
    [SerializeField] public static event Action<int> AddToScoreEvent;
    [SerializeField] public static event Action<int> UpdateDifficultyCounter;
    private PlayerController player;
    private AudioManager audioManager;
    private GameObject currentDirectionGO;
    private DirectionController currentDirectionController;
    private Vector3 spawnPosition;
    private int playerSuccessTapCount = 0;
    private int currentRecruitTapCount = 0;
    private int currentRecruitScore = 0;
    private bool canControl = false;
    private bool isTimesUp = false;

    private void OnEnable() {
        RecruitManager.nextRecruitEvent += SpawnNewRecruitArrows;
        TimerController.timerTimedOutEvent += StopManager;
        GameManager.startGameEvent += ResetManager;
        LevelPromptUI.restartLevelClickEvent += ResetManager;
        LevelPromptUI.goNextLevelClickEvent += ResetManager;
    }

    private void OnDisable() {
        RecruitManager.nextRecruitEvent -= SpawnNewRecruitArrows;
        TimerController.timerTimedOutEvent -= StopManager;
        GameManager.startGameEvent += ResetManager;
        LevelPromptUI.restartLevelClickEvent -= ResetManager;
        LevelPromptUI.goNextLevelClickEvent -= ResetManager;
    }

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        magicAnimator = GetComponentInChildren<Animator>();
        spawnPosition = transform.position;
    }

    private void Update()
    {
        if (canControl && !isTimesUp && currentDirection != null) {
            PlayerInput();
        }
    }

    private void StopManager()
    {
        isTimesUp = true;
        currentDirection = null;
        currentDirectionController = null;
        currentDirectionGO = null;
        if (currentDirectionGO) {
            Destroy(currentDirectionGO);
        }
    }

    private void ResetManager(float time)
    {
        currentDirection = null;
        currentDirectionController = null;
        currentDirectionGO = null;

        currentRecruitTapCount = 0;
        currentRecruitScore = 0;
        playerSuccessTapCount = 0;
        isTimesUp = false;
    }

    private void ResetManager()
    {
        currentDirection = null;
        currentDirectionController = null;
        currentDirectionGO = null;
        
        isTimesUp = false;
        currentRecruitTapCount = 0;
        currentRecruitScore = 0;
        playerSuccessTapCount = 0;
    }

    private void SpawnNewRecruitArrows(RecruitScriptableObject recruit)
    {
        currentRecruitTapCount = recruit.difficultyCount;
        currentRecruitScore = recruit.score;
        UpdateDifficultyCounter?.Invoke(currentRecruitTapCount);
        SpawnNextDirection();
    }

    private void SpawnNextDirection()
    {
        if (isTimesUp) return;

        Direction currDirection = (Direction) Random.Range(0, 4);

        switch (currDirection) {
            case Direction.Up:
                currentDirectionGO = Instantiate(upPrefab, spawnPosition, Quaternion.identity);
                currentDirectionController = currentDirectionGO.GetComponent<DirectionController>();
                currentDirection = "UP";
                break;
            case Direction.Down:
                currentDirectionGO = Instantiate(downPrefab, spawnPosition, Quaternion.identity);
                currentDirectionController = currentDirectionGO.GetComponent<DirectionController>();
                currentDirection = "DOWN";
                break;
            case Direction.Left:
                currentDirectionGO = Instantiate(leftPrefab, spawnPosition, Quaternion.identity);
                currentDirectionController = currentDirectionGO.GetComponent<DirectionController>();
                currentDirection = "LEFT";
                break;
            case Direction.Right:
                currentDirectionGO = Instantiate(rightPrefab, spawnPosition, Quaternion.identity);
                currentDirectionController = currentDirectionGO.GetComponent<DirectionController>();
                currentDirection = "RIGHT";
                break;
            default:
                break;
        }

        canControl = true;
    }

    private void DecideSpawnDirection()
    {
        if(playerSuccessTapCount < currentRecruitTapCount) {
            SpawnNextDirection();
        } else {
            AddToScoreEvent?.Invoke(currentRecruitScore);
            playerSuccessTapCount = 0;
            player.isHitTarget = true;
            canControl = false;
        }
    }

    private void PlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) 
        {
            if (currentDirection == "UP") {
                HitCorrectDirection();
                magicAnimator.SetTrigger("UpCast");
            } else {
                currentDirectionController.OnFailure();
                InvalidInputEvent?.Invoke();
            }
            DecideSpawnDirection();
        } else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            if (currentDirection == "DOWN") {
                HitCorrectDirection();
                magicAnimator.SetTrigger("DownCast");
            } else {
                currentDirectionController.OnFailure();
                InvalidInputEvent?.Invoke();
            }
            DecideSpawnDirection();
        } else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            if (currentDirection == "LEFT") {
                HitCorrectDirection();
                magicAnimator.SetTrigger("LeftCast");
            } else {
                currentDirectionController.OnFailure();
                InvalidInputEvent?.Invoke();
            }
            DecideSpawnDirection();
        } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            if (currentDirection == "RIGHT") {
                HitCorrectDirection();
                magicAnimator.SetTrigger("RightCast");
            } else {
                currentDirectionController.OnFailure();
                InvalidInputEvent?.Invoke();
            }
            DecideSpawnDirection();
        }
    }

    private void HitCorrectDirection()
    {
        playerSuccessTapCount++;
        UpdateDifficultyCounter?.Invoke(currentRecruitTapCount - playerSuccessTapCount);
        currentDirectionController.OnSuccess();
        audioManager.PlaySFX(currentDirectionController.GetClip());
    }

    public int GetDifficultyCountLeft()
    {
        return currentRecruitTapCount - playerSuccessTapCount;
    }
}
