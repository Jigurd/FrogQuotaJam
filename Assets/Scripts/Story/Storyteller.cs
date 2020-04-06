using System.Collections.Generic;
using UnityEngine;

public class Storyteller : MonoBehaviour
{
    [SerializeField] private int _minMinutesBetweenTasks = 30;
    [SerializeField] private int _maxMinutesBetweenTasks = 60 * 3;

    [SerializeField] private int _minMinutesBetweenMuggers = 10;
    [SerializeField] private int _maxMinutesBetweenMuggers = 40;

    [SerializeField] private GameObject _muggerPrefab = null;
    [SerializeField] private GameObject _civlianPrefab = null;

    [SerializeField] private Camera _cityCamera = null;

    private List<Task> _tasks = new List<Task>();
    private float _minutesSinceLastTask = 0;
    private float _nextTaskMinute = 0;

    private float _minutesSinceLastMugger = 0;
    private float _nextMuggerMinute = 0;

    public static List<GameObject> Civilians;

    private void Awake()
    {
        GameState.IsPaused = false;
        GameState.GameMode = GameMode.None;
        GameState.GameMode = GameMode.Office;
        InGameTimeManager.OnMinute += OnMinute;
        Civilians = new List<GameObject>();

        SetNextEventMinute();

        if (InGameTimeManager.Day == 1)
        {
            StartTask();
            var paperPrefab = Resources.Load<UnityEngine.GameObject>("Prefabs/SuperHeroBossInstructions");
            var paperGO = Instantiate(paperPrefab, UnityEngine.GameObject.Find("StuffOnDesk").transform);
            paperGO.transform.localPosition = new Vector3(-2.34f, -2.47f, 0.0f);
            paperGO.GetComponent<SpriteRenderer>().color = Color.white;
        }

        //spawn some civvies
        int civsToSpawn = (int)System.Math.Ceiling((double)InGameTimeManager.Day / 2);

        for (int i = 0; i < civsToSpawn; i++)
        {
            SpawnCivilian();
        }
    }

    private void OnDestroy()
    {
        InGameTimeManager.OnMinute -= OnMinute;

        foreach (GameObject civ in Civilians)
        {
            Civilians.Remove(civ);
            Destroy(civ);
        }
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

        if (_minutesSinceLastMugger == _nextMuggerMinute)
        {
            _minutesSinceLastMugger = 0;
            _nextMuggerMinute = Random.Range(_minMinutesBetweenMuggers, _maxMinutesBetweenMuggers);
            SpawnMugger();
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
        _minutesSinceLastMugger++;
    }

    private void SpawnMugger()
    {
        var dist = (transform.position - _cityCamera.transform.position).z;

        //find out what side to spawn them on
        int leftSide = Random.Range(0, 1);

        float xPosToSpawnAt;

        if (leftSide == 1)
        {
            xPosToSpawnAt = _cityCamera.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
        }
        else
        {
            xPosToSpawnAt = _cityCamera.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
        }

        GameObject mugger = Instantiate(_muggerPrefab, new Vector3(xPosToSpawnAt, -4.5f), Quaternion.identity);
    }

    //spawn a civilian off-screen
    private void SpawnCivilian()
    {
        var dist = (transform.position - _cityCamera.transform.position).z;

        //find out what side to spawn them on
        int leftSide = Random.Range(0, 1);

        float xPosToSpawnAt;

        if (leftSide == 1)
        {
            xPosToSpawnAt = _cityCamera.ViewportToWorldPoint(new Vector3(0, 0, dist)).x + 4;
        } else
        {
            xPosToSpawnAt = _cityCamera.ViewportToWorldPoint(new Vector3(1, 0, dist)).x+4;
        }

        GameObject civvie = Instantiate(_civlianPrefab, new Vector3(xPosToSpawnAt, -4.5f), Quaternion.identity);
        civvie.GetComponent<Civilian>().CityCamera = _cityCamera;
    }
}
