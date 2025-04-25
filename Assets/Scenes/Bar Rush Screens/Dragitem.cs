using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform originalParent;
    private RectTransform rectTransform;
    private Canvas canvas;
    private Vector2 dragOffset;
    private Image itemImage;
    private Color originalColor;
    private float dragAlpha = 0.7f;
    


    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        
        itemImage = GetComponent<Image>();
        originalColor = itemImage.color;
        
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        originalParent = transform.parent;
        
        transform.SetParent(canvas.transform); // Set parent to Canvas
        transform.SetAsLastSibling(); // Set on top
        
        // Calculate drag offset
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), eventData.position, canvas.worldCamera, out Vector2 mouseLocalPosition);
        dragOffset = rectTransform.anchoredPosition - mouseLocalPosition;
        
        // Turn item semi transparent
        Color tempColor = originalColor;
        tempColor.a = dragAlpha;
        itemImage.color = tempColor;
        
        // 
        itemImage.raycastTarget = false;

    }

    public void OnDrag(PointerEventData eventData)    
    {
        Debug.Log("OnDrag");
        Vector2 localPoint;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), eventData.position, canvas.worldCamera, out localPoint))
        {
            rectTransform.anchoredPosition = localPoint + dragOffset;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        transform.SetParent(originalParent);
        
        // Restore item original alpha
        itemImage.color = originalColor;
        
        //
        itemImage.raycastTarget = true;
    }
}
