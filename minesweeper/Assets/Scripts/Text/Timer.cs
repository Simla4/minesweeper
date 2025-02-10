using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class Timer : MonoBehaviour
{
    private TextMeshProUGUI timerText;
    private int time = 0;
    private bool canTimeContiune = true;

    private void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        InputManager.StartGame += StartTimer;
        GridManager.OnFail += StopTimer;

    }

    private void OnDisable()
    {
        InputManager.StartGame -= StartTimer;
        GridManager.OnFail -= StopTimer;
    }

    private void StartTimer()
    {
        StartCoroutine(StartTimerCoroutine());
    }

    private IEnumerator StartTimerCoroutine()
    {
        while (canTimeContiune)
        {
            yield return new WaitForSeconds(1);
            time++;
            timerText.text = time.ToString("000");
        }
    }

    private void StopTimer()
    {
        canTimeContiune = false;
    }
}
