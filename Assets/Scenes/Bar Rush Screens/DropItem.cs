using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropItem : MonoBehaviour, IDropHandler
{
    public RecipeManager recipeManager;

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Dropped");
        
        GameObject droppedObject = eventData.pointerDrag;

        if (droppedObject != null)
        {
            DragItem draggedItem = droppedObject.GetComponent<DragItem>();
            Image itemImage = droppedObject.GetComponent<Image>();

            if (draggedItem != null && itemImage != null && itemImage.sprite != null)
            {
                string itemName = itemImage.sprite.name;    // Get ingredient name from the image

                Transform originalParent = draggedItem.originalParent; // Keep the original parent
                
                
                Debug.Log($"ingredient name from image: {itemImage.sprite.name}");
                
                // Animation function goes here (WIP)
                
                // Call AddIngredient
                if (recipeManager != null)
                {
                    recipeManager.AddIngredient(itemName);
                }
                else
                {
                    Debug.LogWarning("Recipe Manager not assigned");
                }
            }
            else
            {
                Debug.LogWarning("Dropped Object missing DragItem or Image/Sprite");
            }
        }
        
        
    }
}
