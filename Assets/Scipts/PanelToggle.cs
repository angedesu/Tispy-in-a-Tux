using UnityEngine;

public class PanelToggle : MonoBehaviour
{   
    public GameObject panel;

    public void OpenPanel()
    {
        if (panel != null)
        {
            panel.SetActive(true);
        }
    }

    public void ClosePanel()
    {
        if (panel.activeSelf == true)
        {
            panel.SetActive(false);
        }
    }
}
