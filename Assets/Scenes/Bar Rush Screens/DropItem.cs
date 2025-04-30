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
    private const string ICE_ANIM = "dropIce"; // trigger for ice animation
    private const string GARNISH_ANIM = "garnish"; // trigger for garnish animation
    
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
                
                //droppedObject.transform.SetParent(transform, true); // Set parent to mixer
                
                Debug.Log($"ingredient name from image: {itemImage.sprite.name}");
                
                // Animation function goes here (WIP)
                Animator itemAnimator = droppedObject.GetComponent<Animator>();
                if (itemAnimator != null)
                {
                    if (itemName == "Ice cubes_0")  // Separate animation for ice
                    {
                        itemAnimator.SetTrigger(ICE_ANIM);
                    }
                    else if (IngredientLibrary.Garnishes.Contains(itemName))
                    {
                        itemAnimator.SetTrigger(GARNISH_ANIM);
                    }
                    else
                    {
                        itemAnimator.SetTrigger(POUR_ANIM);
                    }
                    
                    StartCoroutine(WaitForAnimation(droppedObject, originalParent)); // Wait for animation to finish before going back to shelf
                    
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

    IEnumerator WaitForAnimation(GameObject itemToReturn, Transform returnParent)
    {
        yield return new WaitForSeconds(0.6f);
        itemToReturn.transform.SetParent(returnParent);
        itemToReturn.transform.SetAsFirstSibling();
    }
    
}
