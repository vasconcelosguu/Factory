using System.Collections;
using TMPro;
using UnityEngine;

public class WarningUI : MonoBehaviour
{
    public static WarningUI Instance;

    public GameObject warningPanel;
    public TMP_Text warningText;

    private Coroutine hideCoroutine;

    private void Awake()
    {
        Instance = this;

        if (warningPanel != null)
            warningPanel.SetActive(false);
    }

    public void ShowWarning(string message, float duration = 4f)
    {
        if (warningText != null)
            warningText.text = message;

        if (warningPanel != null)
            warningPanel.SetActive(true);

        if (hideCoroutine != null)
            StopCoroutine(hideCoroutine);

        hideCoroutine = StartCoroutine(HideAfterDelay(duration));
    }

    public void HideWarning()
    {
        if (hideCoroutine != null)
            StopCoroutine(hideCoroutine);

        if (warningPanel != null)
            warningPanel.SetActive(false);
    }

    private IEnumerator HideAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (warningPanel != null)
            warningPanel.SetActive(false);
    }
}