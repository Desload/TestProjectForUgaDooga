using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LatterInfo : MonoBehaviour
{
    [SerializeField] private RectTransform latterTransform;
    [SerializeField] private TextMeshProUGUI textMP;

    public RectTransform LatterTransform => latterTransform;

    public void SetLetter(string latter)
    {
        textMP.text = latter;
    }
}
