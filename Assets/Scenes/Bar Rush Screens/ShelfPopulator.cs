using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShelfPopulator : MonoBehaviour
{
    public GameObject shelfSlotPrefab;
    // Path to image folder
    public string itemImagePath;
    
    void Start()
    {
        PopulateShelf();
    }

    void PopulateShelf()
    {
        if (shelfSlotPrefab == null)
        {
            Debug.LogError("ShelfSlotPrefab not assigned");
            return;
        }
        
        // Get all images from the folder
        Texture2D[] itemTextures = Resources.LoadAll<Texture2D>(itemImagePath);

        if (itemTextures.Length == 0)
        {
            Debug.LogWarning("No alcohol image found in specified path");
            return;
        }
        
        int numberOfImages = itemTextures.Length;
        
        // Instantate shelf slots prefabs based on the number of images
        for (int i = 0; i < numberOfImages; i++)
        {
            GameObject newShelfSlot = Instantiate(shelfSlotPrefab, transform);
            
            // Get the slot's Image component
            Image shelfSlotImage = newShelfSlot.transform.GetChild(0).GetComponent<Image>();

            if (shelfSlotImage != null)
            {   
                // Create the sprite from the image
                Sprite sprite = Sprite.Create(itemTextures[i], new Rect(0, 0, itemTextures[i].width, itemTextures[i].height), Vector2.one * 0.5f);
                // Assign sprite name from texture name
                sprite.name = itemTextures[i].name;
                
                //Debug.Log($"texture name {itemTextures[i].name}");
                //Debug.Log($"sprite name {sprite.name}");
                
                // Assign sprite to image component
                shelfSlotImage.sprite = sprite;
            }
            else
            {
                Debug.LogError("No Image component found in the 1st child of the shelf slot prefab");
            }
            
            // Get the slot's Text component
            TMP_Text shelfSlotLabel = newShelfSlot.transform.GetChild(1).GetComponent<TMP_Text>();

            if (shelfSlotLabel != null)
            {
                shelfSlotLabel.text = itemTextures[i].name;
                Debug.Log($"Shelf label {shelfSlotLabel.text}");
            }
            else
            {
                Debug.LogError("No Text component found in the 2st child of the shelf slot prefab");
            }

        }
        
    }
}
