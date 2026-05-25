using System.Collections.Generic;
using UnityEngine;

public class BarricadeController : MonoBehaviour
{
    [Header("EPIs necessários")]
    public List<string> requiredEPIs = new List<string>();

    private bool unlocked = false;

    private void Update()
    {
        if (unlocked)
            return;

        CheckRequirements();
    }

    private void CheckRequirements()
    {
        if (TrainingManager.Instance == null)
            return;

        foreach (string epi in requiredEPIs)
        {
            if (!TrainingManager.Instance.HasEPI(epi))
            {
                return;
            }
        }

        UnlockBarricade();
    }

    private void UnlockBarricade()
    {
        unlocked = true;

        Debug.Log("Barricada removida.");

        gameObject.SetActive(false);
    }
}