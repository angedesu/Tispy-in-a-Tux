using UnityEngine;

public class ChangeBackground: MonoBehaviour
{
    public GameObject bgOpen;
    public GameObject bgClosed;

    public void BackgroundChanger()
    {
        bgOpen.SetActive(true);
        bgClosed.SetActive(false);
    }
}
