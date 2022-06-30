using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LetterController : MonoBehaviour
{
    [SerializeField] private RectTransform letterTransform;
    [SerializeField] private TextMeshProUGUI textMP;

    private Coroutine moveRoutin;

    public RectTransform LetterTransform => letterTransform;

    public void SetLetter(string latter)
    {
        textMP.text = latter;
    }

    public void MoveToNewPosiotion(Vector2 newPosition)
    {
        StartCoroutine(DoMove(2f, newPosition));
    }

    private IEnumerator DoMove(float time, Vector2 targetPosition)
    {
        Vector2 startPosition = letterTransform.anchoredPosition;
        float startTime = Time.realtimeSinceStartup;
        float fraction = 0f;
        while (fraction < 1f)
        {
            fraction = Mathf.Clamp01((Time.realtimeSinceStartup - startTime) / time);
            letterTransform.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, fraction);
            yield return null;
        }
    }
}
