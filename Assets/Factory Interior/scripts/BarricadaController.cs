using UnityEngine;

public class BarricadeController : MonoBehaviour
{
    public string requiredEPI = "Capacete";

    void Update()
    {
        if (TrainingManager.Instance == null)
            return;

        if (TrainingManager.Instance.HasEPI(requiredEPI))
        {
            gameObject.SetActive(false);
        }
    }
}