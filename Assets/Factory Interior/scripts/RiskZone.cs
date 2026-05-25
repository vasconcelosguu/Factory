using System.Collections.Generic;
using UnityEngine;

public class RiskZone : MonoBehaviour
{
    public string riskName = "Área de risco";
    [TextArea] public string description = "Área que exige EPIs.";

    [Header("EPIs obrigatórios")]
    public List<string> requiredEPIs = new List<string>();

    [Header("Bloqueio")]
    public bool blockEntryWithoutEPI = true;
    public float pushBackDistance = 1.5f;
    public float messageCooldown = 1f;

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

    private void CheckAccess(Transform playerTransform)
    {
        if (TrainingManager.Instance == null)
            return;

        List<string> missingEPIs = GetMissingEPIs();

        if (missingEPIs.Count == 0)
        {
            if (WarningUI.Instance != null)
                WarningUI.Instance.HideWarning();

            return;
        }

        if (Time.time >= nextMessageTime)
        {
            string msg = FormatEPIList(missingEPIs);

            if (WarningUI.Instance != null)
                WarningUI.Instance.ShowWarning("Você precisa de " + msg.ToLower(), 4f);

            nextMessageTime = Time.time + messageCooldown;
        }

        if (blockEntryWithoutEPI)
            PushPlayerOut(playerTransform);
    }

    private List<string> GetMissingEPIs()
    {
        List<string> missing = new List<string>();

        foreach (string epi in requiredEPIs)
        {
            if (!TrainingManager.Instance.HasEPI(epi))
                missing.Add(epi);
        }

        return missing;
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

    private string FormatEPIList(List<string> epis)
    {
        if (epis.Count == 1)
            return epis[0];

        if (epis.Count == 2)
            return epis[0] + " e " + epis[1];

        string result = "";

        for (int i = 0; i < epis.Count; i++)
        {
            if (i == epis.Count - 1)
                result += "e " + epis[i];
            else
                result += epis[i] + ", ";
        }

        return result;
    }
}