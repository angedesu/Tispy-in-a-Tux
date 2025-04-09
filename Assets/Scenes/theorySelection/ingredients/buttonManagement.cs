using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class buttonManagement : MonoBehaviour
{
    public ScrollRect scrollRect;
    public RectTransform content;
    
    // Call this function and pass in the RectTransform of the chapter to scroll to
    public void ScrollTo(RectTransform target)
    {
        Canvas.ForceUpdateCanvases(); // ensure layout is up-to-date

        float contentHeight = content.rect.height;
        float targetY = Mathf.Abs(target.anchoredPosition.y);

        float normalizedPosition = 1 - (targetY / (contentHeight - scrollRect.viewport.rect.height));
        normalizedPosition = Mathf.Clamp01(normalizedPosition);

        scrollRect.verticalNormalizedPosition = normalizedPosition;
    }

    public void PreviousScreen()
    {
        SceneManager.LoadScene("Scenes/theorySelection/theorySelect");
    }
}
