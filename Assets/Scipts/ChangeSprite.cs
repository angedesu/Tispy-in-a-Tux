using UnityEngine;
using UnityEngine.UI;

public class ChangeMetronome : MonoBehaviour
{

    public Image theImage;
    public Sprite newSprite;
    private Sprite originalSprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalSprite = theImage.sprite;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ImageChange() {
        if (theImage.sprite == newSprite) //image is already changed to new sprite
        {
            theImage.sprite = originalSprite;
        }
        else { //image has not yet changed or is the original sprite
            theImage.sprite = newSprite;
        }
    }

    public void ImageOriginal() {
        theImage.sprite = originalSprite;
    }

}
