using UnityEngine;
using System.Collections;

public class FlashCardFlip : MonoBehaviour
{
    public GameObject frontText;
    public GameObject backText;

    private bool isFront = true;
    private bool isFlipping = false;
    private float flipDuration = 0.5f;

    public void Flip()
    {
        if (!isFlipping)
            StartCoroutine(FlipAnimation());
    }

    private IEnumerator FlipAnimation()
    {
        isFlipping = true;

        float time = 0f;
        float halfDuration = flipDuration / 2f;
        Quaternion startRot = transform.rotation;
        Quaternion midRot = Quaternion.Euler(90, 0, 0);
        Quaternion endRot = isFront ? Quaternion.Euler(180, 0, 0) : Quaternion.Euler(0, 0, 0);

        while (time < flipDuration)
        {
            transform.rotation = Quaternion.Slerp(startRot, endRot, time / flipDuration);
            time += Time.deltaTime;
            yield return null;
        }

        transform.rotation = endRot;
        isFront = !isFront;
        isFlipping = false;
    }


}
