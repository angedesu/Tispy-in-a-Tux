using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class ProfilePicture : MonoBehaviour
{
    public GameObject[] profilePictures;
    public string[] profiles = { "picture01", "picture02", "picture03", "picture04", "picture05", "picture06",
        "picture07", "picture08"};
    private void Start()
    {
        // on start, we access the current profile chosen, if not found choose a default (picture01)
        string currentProfile = PlayerPrefs.GetString("profilePic", "picture01");
        // find the image from the array profilePictures
        int index = Array.IndexOf(profiles, currentProfile);
        // We enable the profile image selected
        SelectedProfile(index);
    }
    public void NextScreen()
    {
        SceneManager.LoadScene("profileSelection");
    }

    void SelectedProfile(int index)
    {
        // go through each image and enable the one that is selected, disable the rest
        for (int i = 0; i < profilePictures.Length; i++)
        {
            // if the image is the profile selected, enable
            if (i == index)
            {
                profilePictures[i].SetActive(true);
            }
            // otherwise, disable
            else
            {
                profilePictures[i].SetActive(false);
            }
        }
    }
    
}
