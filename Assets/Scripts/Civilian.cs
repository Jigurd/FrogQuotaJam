using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Civilian : MonoBehaviour
{
    private State _state;
    private Vector3 _targetPos;
    private Movement _movement;
    private float _startedWaitingTime;
    private float _doneWaitingTime;

    public Camera CityCamera;

    [SerializeField]
    private static int _minWaitTime = 4;

    [SerializeField]
    private static int _maxWaitTime = 10;


    // Start is called before the first frame update
    void Start()
    {
        _movement = GetComponent<Movement>();
        Storyteller.Civilians.Add(gameObject);
        _state = State.FindLocation;
    }

    // Update is called once per frame
    void Update()
    {
        switch (_state)
        {
            case State.FindLocation: _findLocation(); break;
            case State.Move: _move(); break;
            case State.Waiting: _waiting(); break;
            default: Debug.Log("Ka i nasen"); break;
        }
    }

    private void OnDestroy()
    {
        Storyteller.Civilians.Remove(gameObject);
    }



    //find a new target location, start moving there.
    private void _findLocation()
    {
        var dist = (transform.position - CityCamera.transform.position).z;

        float randomX = UnityEngine.Random.Range(CityCamera.ViewportToWorldPoint(new Vector3(0, 0, dist)).x, CityCamera.ViewportToWorldPoint(new Vector3(1, 0, dist)).x);

        //Debug.Log(randomX);

        _targetPos = new Vector3(randomX, transform.position.y);

        _state = State.Move;
        //Debug.Log(_targetPos + " New location found!");
    }

    private void _move()
    {
        Vector3 pathToTarget = _targetPos - transform.position;



        //if we are not closer to target than our speed
        if ((Math.Abs(pathToTarget.x) > (_movement.Speed * Time.deltaTime)))
        {
            //we will move our speed
            _movement.velocity.x = _movement.Speed * Time.deltaTime * Math.Sign(pathToTarget.x);
        }
        else
        {
            //good enough. Zero speed, set the time we're done waiting, start waiting.
            _movement.velocity.x = 0;
            _startedWaitingTime = Time.time;
            _doneWaitingTime = _startedWaitingTime + UnityEngine.Random.Range(_minWaitTime, _maxWaitTime);
            _state = State.Waiting;
        }

    }

    //check if we're done waiting
    private void _waiting()
    {
        if(_doneWaitingTime >= Time.time)
        {
            _state = State.FindLocation;
        }
    }

    private enum State
    {
        FindLocation,
        Move,
        Waiting
    }
}
