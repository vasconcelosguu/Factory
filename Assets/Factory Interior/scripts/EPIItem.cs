using UnityEngine;

public class EPIItem : MonoBehaviour
{
    public string epiName = "Capacete";
    public KeyCode interactKey = KeyCode.E;

    private bool playerNearby = false;

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(interactKey))
        {
            TrainingManager.Instance.EquipEPI(epiName);

            if (InteractUI.Instance != null)
                InteractUI.Instance.Hide();

            Debug.Log("Pegou o EPI: " + epiName);

            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;

            if (InteractUI.Instance != null)
            {
                InteractUI.Instance.Show("Pressione [E] para pegar o " + epiName);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;

            if (InteractUI.Instance != null)
                InteractUI.Instance.Hide();
        }
    }
}