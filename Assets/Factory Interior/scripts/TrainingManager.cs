using System.Collections.Generic;
using UnityEngine;

public class TrainingManager : MonoBehaviour
{
    public static TrainingManager Instance;

    private HashSet<string> equippedEPIs = new HashSet<string>();

    private void Awake()
    {
        Instance = this;
    }

    public void EquipEPI(string epiName)
    {
        equippedEPIs.Add(epiName);
        Debug.Log("Equipado: " + epiName);
    }

    public bool HasEPI(string epiName)
    {
        return equippedEPIs.Contains(epiName);
    }
}