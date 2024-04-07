using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public static Timer Instance;
    public TMP_Text timeText;
    public int generationTime = 10;
    public UnityEvent OnTimeRunOut;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    [Button]
    public void Start()
    { 
        generationTime = 10;
        timeText.text = generationTime.ToString();
        StartCoroutine(TimeCorotuine());
    }
    public IEnumerator TimeCorotuine()
    {
        for (int i = generationTime; generationTime >= 0; generationTime--)
        {
            yield return new WaitForSeconds(1);
        }

        PlayerStats.Instance.NewGen();
    }

    private void Update()
    {
        timeText.text = generationTime.ToString();
        if (generationTime < 0) generationTime = 10;
    }
}
