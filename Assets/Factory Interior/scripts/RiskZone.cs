using UnityEngine;

public class RiskZone : MonoBehaviour
{
    public string riskName = "Área 1";
    [TextArea] public string description = "Área de risco";
    public string requiredEPI = "Capacete";

    [Header("Bloqueio")]
    public bool blockEntryWithoutEPI = true;
    public float pushBackDistance = 1.5f;
    public float messageCooldown = 1.0f;

    private float nextMessageTime = 0f;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        CheckAccess(other.transform);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        CheckAccess(other.transform);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (WarningUI.Instance != null)
            WarningUI.Instance.HideWarning();
    }

    private void CheckAccess(Transform playerTransform)
    {
        if (TrainingManager.Instance == null)
        {
            Debug.LogWarning("TrainingManager não encontrado.");
            return;
        }

        bool hasRequiredEPI = TrainingManager.Instance.HasEPI(requiredEPI);

        if (hasRequiredEPI)
        {
            if (WarningUI.Instance != null)
                WarningUI.Instance.HideWarning();

            return;
        }

        if (Time.time >= nextMessageTime)
        {
            Debug.LogWarning("Sem EPI obrigatório: " + requiredEPI);

            if (WarningUI.Instance != null)
                WarningUI.Instance.ShowWarning("Você não pode entrar sem capacete", 4f);

            nextMessageTime = Time.time + messageCooldown;
        }

        if (blockEntryWithoutEPI)
            PushPlayerOut(playerTransform);
    }

    private void PushPlayerOut(Transform playerTransform)
    {
        CharacterController cc = playerTransform.GetComponent<CharacterController>();

        Vector3 direction = playerTransform.position - transform.position;
        direction.y = 0f;

        if (direction.sqrMagnitude < 0.001f)
            direction = -transform.forward;

        direction.Normalize();

        Vector3 targetPosition = playerTransform.position + direction * pushBackDistance;

        if (cc != null) cc.enabled = false;
        playerTransform.position = targetPosition;
        if (cc != null) cc.enabled = true;
    }
}