using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text timeText;
    public int generationTime = 120;
    public UnityEvent OnTimeRunOut;

    [Button]
    private void Start()
    {
        timeText.text = generationTime.ToString();
        StartCoroutine(TimeCorotuine());
    }
    IEnumerator TimeCorotuine()
    {
        for (int i = generationTime; generationTime >= 0; generationTime--)
        {
            yield return new WaitForSeconds(1);
            timeText.text = generationTime.ToString();
        }
        OnTimeRunOut.Invoke();
    }
}
