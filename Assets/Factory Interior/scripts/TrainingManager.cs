using System.Collections.Generic;
using UnityEngine;

public class TrainingManager : MonoBehaviour
{
    public static TrainingManager Instance;

    private HashSet<string> equippedEPIs = new HashSet<string>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void EquipEPI(string epiName)
    {
        if (string.IsNullOrEmpty(epiName))
            return;

        equippedEPIs.Add(epiName);
        Debug.Log("EPI equipado: " + epiName);
    }

    public bool HasEPI(string epiName)
    {
        return equippedEPIs.Contains(epiName);
    }
}