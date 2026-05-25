using UnityEngine;

public class VREPIItem : MonoBehaviour
{
    public string epiName = "Capacete";
    public KeyCode interactKey = KeyCode.E;

    private bool playerNearby = false;

    private void Update()
    {
        if (playerNearby && Input.GetKeyDown(interactKey))
        {
            if (TrainingManager.Instance != null)
            {
                TrainingManager.Instance.EquipEPI(epiName);
                Debug.Log("EPI coletado: " + epiName);
            }

            playerNearby = false;

            if (InteractUI.Instance != null)
                InteractUI.Instance.Hide();

            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.CompareTag("Player"))
        {
            playerNearby = true;

            if (InteractUI.Instance != null)
                InteractUI.Instance.Show("Pressione [E] para pegar: " + epiName);

            Debug.Log("Pressione E para pegar: " + epiName);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.root.CompareTag("Player"))
        {
            playerNearby = false;

            if (InteractUI.Instance != null)
                InteractUI.Instance.Hide();
        }
    }

    private void OnDisable()
    {
        if (InteractUI.Instance != null)
            InteractUI.Instance.Hide();
    }
}