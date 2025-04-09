using UnityEngine;

public class FirstLauchDetector : MonoBehaviour
{
    private const string FirstLaunchKey = "FirstLauchDetector";
    
    void Start()
    {   
        // If first launch (no key), create and save a key
        if (!PlayerPrefs.HasKey(FirstLaunchKey))
        {
            PlayerPrefs.SetInt(FirstLaunchKey, 1);
            Debug.Log("FirstLaunchKey created");
            PlayerPrefs.Save();
        }
    }
}
