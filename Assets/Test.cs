using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class Test : MonoBehaviour
{
    public int Duration = 5;
    public enum TimerType
    {
        Normal,
        Coroutine,
        TimerTask
    }
     void CreateNormalTimer(int number)
    {
        for (int i = 0; i < number; i++)
        {
            var go = new GameObject($"Timer{i}");
            var timer = go.AddComponent<Timer>();
            timer.OnComplete = () => {
                Destroy(go);
                };
            timer.Duration = Duration;
            timer.StartTimer();
        }
    }

     void CreateCoroutineTimer(int number)
    {
        for (int i = 0; i < number; i++)
        {
            var go = new GameObject($"Timer{i}");
            var timer = go.AddComponent<TimerCoroutine>();
            timer.OnComplete = () => {
                Destroy(go);
            };
            timer.Duration = Duration;
            timer.StartTimer();
        }
    }

    [SerializeField]
    private int counter = 0;
    GameObject TimerTask;
    public void CreateTimer(int nubmer, TimerType type)
    {
        switch (type)
        {
            case TimerType.Normal:
                CreateNormalTimer(nubmer);
                break;
            case TimerType.Coroutine:
                CreateCoroutineTimer(nubmer);
                break;
            case TimerType.TimerTask:
                {
                    if (TimerTask == null)
                    {
                        TimerTask = new GameObject($"TimerTask");
                    }
                    var timer = TimerTask.AddComponent<TimerWithTask>();
                    for (int i = 0; i < nubmer; i++)
                    {
                        
                        var task = timer.CreateTimer();
                        task.Duration = Duration;
                        task.OnComplete += () =>
                        {
                            counter--;
                            if (counter == 0)
                            {
                                Destroy(TimerTask);
                            }
                        };
                        task.StartTimer();
                        counter++;
                    }
                   
                }
                break;
            default:
                break;
        }
    }

    
}
