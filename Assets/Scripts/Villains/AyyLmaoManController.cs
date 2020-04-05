using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AyyLmaoManController : MonoBehaviour
{
    [SerializeField] private float _probeTimer = 0;
    [SerializeField] private float _speed = 0;
    [SerializeField] private GameObject[] _victims = null;

    private float _probeTime = 3;
    private bool _lookingForVictim = true;
    private Vector3 _pointA;
    private Vector3 _pointB;
    private Vector3 _targetPoint;
    private GameObject _victim;

    private float _distanceBetweenEnemyAndT(Vector3 v3) 
    {
        return Vector3.Distance(transform.position, v3);
    }

    private void Awake()
    {
        _pointA = new Vector3(transform.position.x + 6, transform.position.y, transform.position.z);
        _pointB = new Vector3(transform.position.x - 6, transform.position.y, transform.position.z);
        _targetPoint = _pointA;
    }
    private void Update()
    {

        if (GameState.IsPaused)
        {
            return;
        }

        Move();

        if (!_lookingForVictim)
            Probe();
    }

    private void Move()
    {
        if (!_lookingForVictim)
            return;

        if (_targetPoint == _pointA)
            transform.position += Vector3.right * _speed * Time.deltaTime;
        else if (_targetPoint == _pointB)
            transform.position += -Vector3.right * _speed * Time.deltaTime;

        if (_distanceBetweenEnemyAndT(_pointA) <= 2)
        {
            _targetPoint = _pointB;
        }
        else if (_distanceBetweenEnemyAndT(_pointB) <= 2)
        {
            _targetPoint = _pointA;
        }
                
        _probeTimer -= Time.deltaTime;
        if (_probeTimer <= 0)
        {
            _victim = _victims[Random.Range(0, _victims.Length - 1)].gameObject;

            _lookingForVictim = false;
            print("Came to earth in a silver chrome U.F.O, to probe your ass and see how deep it can go.");

            _probeTimer = _probeTime;
        }
    }
    private void Probe()
    {


        /*
        When I show up, I'm just a little SECTOID.
        But later on, I become a MECHTOID.
        You may spot me, then I move free.
        Then I land a crit on your rookie.
        Can't see me, can't shoot me,
        but I'm fucked when you try to flank me.
        
        Came to earth in a silver chrome U.F.O,
        to probe your ass and see how deep it can go.
        Gain a boost when I merge my mind with my bro,
        AAAAAAAAAAAAAAAY LMAO. (2x)
        */
    }
}
