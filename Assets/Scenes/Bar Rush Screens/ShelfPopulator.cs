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
                // Assign sprite to image component
                shelfSlotImage.sprite = sprite;
            }
            else
            {
                Debug.LogError("No Image component found in the 1st child of the shelf slot prefab");
            }

            /*SpriteRenderer shelfItem = newShelfSlot.transform.GetChild(0).GetComponent<SpriteRenderer>();
            if (shelfItem != null)
            {   
                // Create the sprite from the image
                Sprite sprite = Sprite.Create(alcoholTextures[i], new Rect(0, 0, alcoholTextures[i].width, alcoholTextures[i].height), Vector2.one * 0.5f);
                // Assign sprite to image component
                shelfItem.sprite = sprite;
            }
            else
            {
                Debug.LogError("No Image component found in the 1st child of the shelf slot prefab");
            }*/
            
        }
        
    }
}
