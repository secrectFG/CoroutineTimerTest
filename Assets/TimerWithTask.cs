using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TimerTask
{
    private float _duration = 1f;

    private float _timeScale = 1f;

    private float _startTime = 0f;

    private float _time = 0f;

    private bool _isRunning = false;

    public float TimeScale { get => _timeScale; set => _timeScale = value; }
    public float Duration { get => _duration; set => _duration = value; }
    public bool IsRunning { get => _isRunning; set => _isRunning = value; }

    public event Action OnComplete;

    // Update is called once per frame
    public void Update()
    {
        if (_isRunning)
        {
            _time += Time.deltaTime * _timeScale;
            var value = GetTimeRemaining();
            if (value <= 0f)
            {
                _isRunning = false;
                OnComplete?.Invoke();
                OnComplete = null;
            }
        }
    }


    public float GetTime()
    {
        return _time;
    }

    public float GetTimeElapsed()
    {
        return _isRunning ? GetTime() - _startTime : 0;
    }

    public float GetTimeRemaining()
    {
        return _duration - GetTimeElapsed();
    }

    public float GetRatioComplete()
    {
        return GetTimeElapsed() / _duration;
    }

    public void StartTimer()
    {
        if (_isRunning)
        {
            return;
        }
        _isRunning = true;
        _startTime = GetTime();
    }
}
public class TimerWithTask : MonoBehaviour
{

    private List<TimerTask> _timers = new List<TimerTask>();


    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < _timers.Count; i++)
        {
            var timer = _timers[i];
            timer.Update();
        }
    }

    public TimerTask CreateTimer()
    {
        var timer = new TimerTask();
        _timers.Add(timer);
        timer.OnComplete += ()=>
        {
            _timers.Remove(timer);
        };
        return timer;
    }
}