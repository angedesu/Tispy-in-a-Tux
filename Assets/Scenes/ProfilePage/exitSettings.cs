using UnityEngine;  

public class exitSettings : MonoBehaviour
{
    public GameObject panel;

    public void closeScreen()
    {
        panel.SetActive(false);
    }
}
