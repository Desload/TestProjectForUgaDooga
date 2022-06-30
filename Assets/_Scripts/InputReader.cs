using System.Collections;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;
using System.Text.RegularExpressions;

public class InputReader : MonoBehaviour
{
    [SerializeField] private TMP_InputField widthText;
    [SerializeField] private TMP_InputField heightText;

    public bool GetValueFromFields(out int width, out int height)
    {
        string tempWidthText = widthText.text;
        string tempHeightText = heightText.text;

        width = 0;
        height = 0;

        if (tempWidthText == "" || tempHeightText == "")
        {
            return false;
        }

        if (Regex.IsMatch(tempWidthText, "^[^0-9]") || Regex.IsMatch(tempHeightText, "^[^0-9]"))
        {
            return false;
        }

        int.TryParse(string.Join("", tempWidthText.Where(c => char.IsDigit(c))), out width);
        int.TryParse(string.Join("", tempHeightText.Where(c => char.IsDigit(c))), out height);

        return true;
    }
}
