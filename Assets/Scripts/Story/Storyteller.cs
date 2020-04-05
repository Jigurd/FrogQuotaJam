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
        GameState.IsPaused = false;
        GameState.GameMode = GameMode.None;
        GameState.GameMode = GameMode.Office;
        InGameTimeManager.OnMinute += OnMinute;
        SetNextEventMinute();

        if (InGameTimeManager.Day == 1)
        {
            StartTask();
            var paperPrefab = Resources.Load<GameObject>("Prefabs/SuperHeroBossInstructions");
            var paperGO = Instantiate(paperPrefab, GameObject.Find("StuffOnDesk").transform);
            paperGO.transform.localPosition = new Vector3(-2.34f, -2.47f, 0.0f);
            paperGO.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    private void OnDestroy()
    {
        InGameTimeManager.OnMinute -= OnMinute;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameState.IsPaused) return;

        foreach (var task in _tasks.ToArray())
        {
            task.OnUpdate();
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
    }
}
