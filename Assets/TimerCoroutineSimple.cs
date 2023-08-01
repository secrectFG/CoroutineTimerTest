using System;
using System.Collections;
using UnityEngine;

class TimerCoroutineSimple : MonoBehaviour
{
    

    public void StartTimer(float duration, Action onComplete)
    {
        StartCoroutine(cStartTimer(duration, onComplete));
    }

    IEnumerator cStartTimer(float duration, Action onComplete)
    {
        yield return new WaitForSeconds(duration);
        onComplete?.Invoke();
    }
}