using TMPro;
using UnityEngine;

public class InteractUI : MonoBehaviour
{
    public static InteractUI Instance;

    public GameObject panel;
    public TMP_Text text;

    private void Awake()
    {
        Instance = this;

        if (panel != null)
            panel.SetActive(false);
    }

    public void Show(string message)
    {
        if (text != null)
            text.text = message;

        if (panel != null)
            panel.SetActive(true);
    }

    public void Hide()
    {
        if (panel != null)
            panel.SetActive(false);
    }
}