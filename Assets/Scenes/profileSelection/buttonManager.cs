using System;
using UnityEngine;
using UnityEngine.UI;

namespace profilePage
{
    public class ButtonManager : MonoBehaviour
    {
        public Image[] profilePictures;

        public string[] profiles = { "picture01", "picture02", "picture03", "picture04", "picture05", "picture06",
            "picture07", "picture08"};

    private void Start()
        {
            // for testing
            // PlayerPrefs.DeleteAll();
            // get the current profile picture and grey it out before updating the selection
            string currentPic = PlayerPrefs.GetString("profilePic", "picture01");
            int index = Array.FindIndex(profiles, s => s == currentPic );
            SelectedImage(index);
        }

        public void ChangeImage(int profileIndex)
        {
            // updating the selected image and saving the new picture
            SelectedImage(profileIndex);
            SaveNewProfilePicture(profileIndex);
        }

        void SaveNewProfilePicture(int index)
        {
            // save the picture into  Player Prefs
            PlayerPrefs.SetString("profilePic", profiles[index]);
            PlayerPrefs.Save();
        }

        void SelectedImage(int index)
        {
            // go through each image and grey out the one that is selected
            for (int i = 0; i < profilePictures.Length; i++)
            {
                // if the image is the profile slected, grey it out
                if (i == index)
                {
                    profilePictures[i].color = new Color(0.5f, 0.5f, 0.5f, 1f); 
                }
                // otherwise, leave original color intact
                else
                {
                    profilePictures[i].color = Color.white;
                }
            }
        }
    }
}
