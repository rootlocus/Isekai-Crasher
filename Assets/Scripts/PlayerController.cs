using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    private Vector3 position;
    private Vector3 initialPosition;
    private GameManager gameManager;
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private AudioClip smashSfx;
    [SerializeField] private AudioManager audioManager;
    public bool isHitTarget {get; set;}
    public bool isReturnPosition {get; set;}
    public static event Action playerReturnEvent;
    public static event Action recruitIsekaiEvent;

    private void OnEnable() {
        TimerController.timerTimedOutEvent += SetPlayerIdle;
    }

    private void OnDisable() {
        TimerController.timerTimedOutEvent -= SetPlayerIdle;
    }

    private void Awake() {
        initialPosition = transform.position;
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void FixedUpdate()
    {
        if (isHitTarget) {
            HitTarget();
        }
        if (isReturnPosition) {
            ReturnPosition();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Target") {
            recruitIsekaiEvent?.Invoke();
            audioManager.PlaySFX(smashSfx);
            isHitTarget = false;
            isReturnPosition = true;
        }
    }
    
    private void HitTarget()
    {
        float step = speed * Time.deltaTime;

        transform.position = new Vector3(transform.position.x + step, transform.position.y, transform.position.z);
    }

    private void ReturnPosition()
    {
        float step = speed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, initialPosition, step);

        if (transform.position == initialPosition) {
            isReturnPosition = false;
            playerReturnEvent?.Invoke();
        }
    }

    private void SetPlayerIdle()
    {
        isHitTarget = false;
        ReturnPosition();
    }

    public void ResetPosition()
    {
        transform.position = initialPosition;
    }
}
