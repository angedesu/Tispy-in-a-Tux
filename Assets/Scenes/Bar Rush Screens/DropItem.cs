using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class DropItem : MonoBehaviour, IDropHandler
{
    public RecipeManager recipeManager;

    private const string POUR_ANIM = "pour"; // trigger for pour animation
    public float pouringDuration = 1.5f;   // Duration of pour animation
    public float returnDelay = 0.5f;    // Delay before return to shelf

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
                
                droppedObject.transform.SetParent(transform, true); // Set parent to mixer
                
                Debug.Log($"ingredient name from image: {itemImage.sprite.name}");
                
                // Animation function goes here (WIP)
                Animator itemAnimator = droppedObject.GetComponent<Animator>();
                if (itemAnimator != null)
                {
                    itemAnimator.SetTrigger(POUR_ANIM);
                }
                else
                {
                    Debug.LogWarning($"Can't find animator in dropped object");
                }
                
                
                
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

    /*public void ReturnToShelf(GameObject itemToReturn, Transform returnParent)
    {
        
    }
    
    IEnumerator ReturnToShelfDelayed(GameObject itemToReturn, Transform returnParent, float delay)
    {
        yield return new WaitForSeconds(delay + returnDelay);
        ReturnToShelf(itemToReturn, returnParent);
    }

    IEnumerator ReturnToShelf(GameObject itemToReturn, Transform returnParent)
    {
        itemToReturn.transform.SetParent(returnParent);
    }*/
}
