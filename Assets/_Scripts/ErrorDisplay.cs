using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ErrorDisplay : MonoBehaviour
{
    public static Action<string> ShowError;

    [SerializeField] private TextMeshProUGUI errorText;

    private Coroutine doAlpha;

    private void OnEnable()
    {
        ShowError += ShowMessage;
    }

    private void OnDisable()
    {
        ShowError -= ShowMessage;
    }

    private void ShowMessage(string message)
    {
        if (doAlpha != null)
        {
            StopCoroutine(doAlpha);
            doAlpha = null;
        }
        
        errorText.text = message;
        doAlpha = StartCoroutine(DoAlpha(0.5f));
    }

    private IEnumerator DoAlpha(float time)
    {
        float startTime = Time.realtimeSinceStartup;
        float fraction = 0f;
        while (fraction < 1f)
        {
            fraction = Mathf.Clamp01((Time.realtimeSinceStartup - startTime) / time);
            errorText.faceColor = new Color32(0,0,0, (byte)(fraction * 255));
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        startTime = Time.realtimeSinceStartup;
        fraction = 0f;

        while (fraction < 1f)
        {
            fraction = Mathf.Clamp01((Time.realtimeSinceStartup - startTime) / time);

            errorText.faceColor = new Color32(0, 0, 0, (byte)(255 - fraction * 255));
            yield return null;
        }

        doAlpha = null;
    }
}
