using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ClawCharge_Impossible : MonoBehaviour
{
    private CharacterController _characterController;
    private SalmonControls inputActions;
    private float chargeTime = 0f;
    [SerializeField] float maxChargeTime = 2f;
    [SerializeField] float minDropDistance = 2f;
    [SerializeField] float maxDropDistance = 10f;
    [SerializeField] private AudioClip clawDropSound;
    

    private Vector3 originalPosition;

    private bool isCharging = false;
    private bool isClawActive = false;

    //UI
    [SerializeField] Slider chargeBar;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        inputActions = new SalmonControls();

        inputActions.Player.ClawCharge.started += ctx => StartCharging();
        inputActions.Player.ClawCharge.canceled += ctx => ReleaseClaw();

        originalPosition = transform.position;

        if (chargeBar != null)
        {
            chargeBar.value = 0f;
        }
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void StartCharging()
    {
        if (isClawActive) return;
        isClawActive = true;
        isCharging = true;
        chargeTime = 0f;
    }

    private void ReleaseClaw()
    {
        if (!isCharging || !isClawActive) return;
        if (isCharging)
        {
            PerformClawDrop();

            SoundEffectManager.Instance.PlaySoundFXClip(clawDropSound, transform, 0.2f);

            chargeTime = 0f;
            isCharging = false;
            if (chargeBar != null)
                chargeBar.value = 0f;
            StartCoroutine(ResetClawPositionAfterDelay(0.5f));
        }
    }

    private IEnumerator ResetClawPositionAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        // move back to original position and reset isclawactive bool
        float duration = 0.3f; // Duration of reset animation
        Vector3 startPosition = transform.position;
        float elapsed = 0f;
        isClawActive = false;

        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(startPosition, originalPosition, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPosition;
    }

    private void Update()
    {
        if (isCharging)
        {
            chargeTime += Time.deltaTime;
            if (chargeTime > maxChargeTime)
                chargeTime = maxChargeTime;
            if (chargeBar != null)
                chargeBar.value = chargeTime / maxChargeTime;

        }
    }

    private void PerformClawDrop()
    {
        float chargePercent = chargeTime / maxChargeTime;
        float dropDistance = Mathf.Lerp(minDropDistance, maxDropDistance, chargePercent);

        Vector3 randomDirection = new Vector3(Random.Range(-2f, 2f), -2, 0);
        Vector3 targetPosition = transform.position + randomDirection * dropDistance;
        //transform.position = targetPosition;

        StartCoroutine(MoveClawToPosition(targetPosition));
    }

    private IEnumerator MoveClawToPosition(Vector3 targetPosition)
    {
        float duration = 0.2f; // adjust for speed
        Vector3 startPosition = transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
    }
}