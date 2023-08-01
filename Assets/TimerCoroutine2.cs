using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimerCoroutine2 : MonoBehaviour
{
    public class Task
    {
        public float duration = 1f;

        public float timeScale = 1f;

        public float startTime = 0f;

        public float time = 0f;

        public bool isRunning = false;

        public event Action OnComplete;

        public void Complete()
        {
            OnComplete?.Invoke();
            OnComplete = null;
        }

        public float GetTime()
        {
            return time;
        }

        public float GetTimeElapsed()
        {
            return isRunning ? GetTime() - startTime : 0;
        }

        public float GetTimeRemaining()
        {
            return duration - GetTimeElapsed();
        }

        public float GetRatioComplete()
        {
            return GetTimeElapsed() / duration;
        }
    }

    public void StartTimer(float duration, Action onComplete)
    {
        Task task = new Task();
        task.duration = duration;
        task.OnComplete += onComplete;
        if (task.isRunning)
        {
            return;
        }
        task.isRunning = true;
        task.startTime = task.GetTime();
        StartCoroutine(cRun(task));
    }

    IEnumerator cRun(Task task)
    {
        while (task.isRunning)
        {
            task.time += Time.deltaTime * task.timeScale;
            var value = task.GetTimeRemaining();
            if (value <= 0f)
            {
                task.isRunning = false;
                task.Complete();
            }
            yield return null;
        }
    }
}
