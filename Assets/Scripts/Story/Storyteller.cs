using System.Collections.Generic;
using UnityEngine;

public class Storyteller : MonoBehaviour
{
    [SerializeField] private int _minMinutesBetweenTasks = 30;
    [SerializeField] private int _maxMinutesBetweenTasks = 60 * 3;

    private List<Task> _tasks = new List<Task>();
    private float _minutesSinceLastTask = 0;
    private float _nextTaskMinute = 0;

    private void Awake()
    {
        InGameTimeManager.OnMinute += OnMinute;
        SetNextEventMinute();
    }

    private void OnDestroy()
    {
        InGameTimeManager.OnMinute -= OnMinute;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var task in _tasks.ToArray())
        {
            Debug.Log("UPDATING MAH BOY");
            task.OnUpdate();
            Debug.Log("CHECKING IF MAH BOY IS COMPLETE YO");
            if (task.IsComplete())
            {
                task.OnFinished();
                _tasks.Remove(task);
            }
        }
        if (_minutesSinceLastTask == _nextTaskMinute)
        {
            _minutesSinceLastTask = 0;
            SetNextEventMinute();
            StartTask();
        }
    }

    private void StartTask()
    {
        int rand = Random.Range(0, 100);
        if (rand < 100) //lmao
        {
            var task = new StampPapersTask();
            _tasks.Add(task);
            task.OnStart();
        }
    }

    private void SetNextEventMinute()
    {
        _nextTaskMinute = Random.Range(_minMinutesBetweenTasks, _maxMinutesBetweenTasks + 1);
    }

    private void OnMinute(int minute)
    {
        _minutesSinceLastTask++;
        Debug.Log(_nextTaskMinute - _minutesSinceLastTask);
    }
}
