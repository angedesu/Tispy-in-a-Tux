using UnityEngine;

public class ShelfAnim : MonoBehaviour
{
    public GameObject shelf;

    public void ShowHideShelf()
    {
        if (shelf != null)
        {
            Animator animator = shelf.GetComponent<Animator>();
            if (animator != null)
            {
                bool isOpen = animator.GetBool("show");
                animator.SetBool("show", !isOpen);
            }
            
        }
    }
}
