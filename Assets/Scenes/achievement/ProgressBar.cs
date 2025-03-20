using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class ProgressBar : MonoBehaviour
{
    public int maximum;
    public int current;
    public Image mask;
    public GameObject uncheckMark;
    public GameObject checkMark;

    void Start()
    {
        UpdateCheckMark();
    }

    void Update()
    {
        GetCurrentFill();
        UpdateCheckMark();
    }

    void GetCurrentFill()
    {
        float fillAmount = (float)current / (float)maximum;
        mask.fillAmount = fillAmount;
    }

    void UpdateCheckMark()
    {
        if (current >= maximum)
        {
            checkMark.SetActive(true);
            uncheckMark.SetActive(false);
        }
        else
        {
            checkMark.SetActive(false);
            uncheckMark.SetActive(true);
        }
    }
}
