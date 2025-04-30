using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class moveImage : MonoBehaviour
{
    public Image theImage;
    public int moveX = 0;
    public int moveY = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void MoveImage() { 
        var pos = theImage.rectTransform.localPosition;

        if (moveX != 0 && moveY != 0)
        {
            theImage.rectTransform.localPosition = new Vector2(moveX, moveY);
            theImage.rectTransform.anchoredPosition = new Vector2(moveX, moveY);
        }
        else if (moveX != 0)
        { //Move Image by the x
            theImage.rectTransform.localPosition = new Vector2(moveX, pos.y);
            theImage.rectTransform.anchoredPosition = new Vector2(moveX, pos.y);
        }
        else if (moveY != 0)
        {
            theImage.rectTransform.localPosition = new Vector2(pos.x, moveY);
            theImage.rectTransform.anchoredPosition = new Vector2(pos.x, moveY);
        }
    }

    //Temp function that does nothing, until I actually need to move the Image back from where it started
    public void MoveImageBack() { 
    }




}
