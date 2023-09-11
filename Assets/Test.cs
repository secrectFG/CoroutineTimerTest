
using UnityEngine;

public class Test : MonoBehaviour
{
    public int Duration = 5;

    public enum TimerType
    {
        Normal,
        Coroutine,
        CoroutineSimple,
        TimerTask,
        Coroutine2,
    }

    void CreateNormalTimer(int number)
    {
        for (int i = 0; i < number; i++)
        {
            var go = new GameObject($"Timer{i}");
            var timer = go.AddComponent<Timer>();
            timer.OnComplete = () =>
            {
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
            timer.OnComplete = () =>
            {
                Destroy(go);
            };
            timer.Duration = Duration;
            timer.StartTimer();
        }
    }

    [SerializeField]
    private int counter = 0;
    GameObject temp;

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
            case TimerType.CoroutineSimple:

                {
                    if (temp == null)
                    {
                        temp = new GameObject($"TimerCoroutineSimple");
                    }
                    var timer = temp.AddComponent<TimerCoroutineSimple>();
                    counter = nubmer;
                    for (int i = 0; i < nubmer; i++)
                    {
                        timer.StartTimer(
                            Duration,
                            () =>
                            {
                                counter--;
                                if (counter == 0)
                                {
                                    Destroy(temp);
                                }
                            }
                        );
                    }
                }
                break;
            case TimerType.TimerTask:

                {
                    if (temp == null)
                    {
                        temp = new GameObject($"TimerWithTask");
                    }
                    var timer = temp.AddComponent<TimerWithTask>();
                    counter = nubmer;
                    for (int i = 0; i < nubmer; i++)
                    {
                        var task = timer.CreateTimer();
                        task.Duration = Duration;
                        task.OnComplete += () =>
                        {
                            counter--;
                            if (counter == 0)
                            {
                                Destroy(temp);
                            }
                        };
                        task.StartTimer();
                    }
                }
                break;
            case TimerType.Coroutine2:

                {
                    if (temp == null)
                    {
                        temp = new GameObject($"TimerCoroutine2");
                    }
                    var timer = temp.AddComponent<TimerCoroutine2>();
                    counter = nubmer;
                    for (int i = 0; i < nubmer; i++)
                    {
                        timer.StartTimer(
                            Duration,
                            () =>
                            {
                                counter--;
                                if (counter == 0)
                                {
                                    Destroy(temp);
                                }
                            }
                        );
                    }
                }
                break;
        }
    }
}
