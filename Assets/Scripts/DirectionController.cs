using UnityEngine;

public class DirectionController : MonoBehaviour
{
    private Animator myAnimator;
    [SerializeField] private AudioClip clip;

    private void OnEnable() {
        EscapeEventOnExit.escapedEvent += OnFailure;
    }

    private void OnDisable() {
        EscapeEventOnExit.escapedEvent -= OnFailure;
    }

    public void Awake() {
        myAnimator = GetComponentInChildren<Animator>();
    }

    public void OnSuccess()
    {
        myAnimator.SetTrigger("OnSuccess");
    }

    public void OnFailure()
    {
        myAnimator.SetTrigger("OnFailed");
    }

    public AudioClip GetClip()
    {
        return clip;
    }
}
