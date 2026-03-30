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
            Debug.Log("Pegou o EPI: " + epiName);

            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            Debug.Log("Pressione E para pegar: " + epiName);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
        }
    }
}