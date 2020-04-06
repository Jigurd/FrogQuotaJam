using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AyyLmaoManController : MonoBehaviour
{
    [SerializeField] private float _probeTimer = 0;
    [SerializeField] private float _speed = 0;
    [SerializeField] private float _probeRange = 1.25f;
    [SerializeField] private GameObject[] _victims = null;

    // This GameObject needs a collider.
    private GameObject _victim;
    private Vector3 _pointA;
    private Vector3 _pointB;
    private Vector3 _targetPoint;
    private bool _lookingForVictim = true;
    private bool _lookingAtVictim = false;
    private float _probeTime = 3;
    private float _lineUpStartTime = 0;
    private float _distanceBetweenEnemyAndT(Vector3 v3)
    {
        return Vector3.Distance(transform.position, v3);
    }

    private void Awake()
    {
        _pointA = new Vector3(transform.position.x + 4, transform.position.y, transform.position.z);
        _pointB = new Vector3(transform.position.x - 4, transform.position.y, transform.position.z);
        _targetPoint = _pointA;
    }
    private void Update()
    {
<<<<<<< HEAD
        //print(Random.Range(0, _victims.Length));
=======

        if (GameState.IsPaused)
        {
            return;
        }

>>>>>>> 18bff30d8a0527bb5ea8c3c06b976b5ed17b4b0e
        Move();
        if (_lookingAtVictim)
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

        if (_distanceBetweenEnemyAndT(_pointA) <= .1f)
        {
            _targetPoint = _pointB;
        }
        else if (_distanceBetweenEnemyAndT(_pointB) <= .1f)
        {
            _targetPoint = _pointA;
        }

        _probeTimer -= Time.deltaTime;
        if (_probeTimer <= 0)
        {
            _victim = _victims[Random.Range(0, _victims.Length)];
            _lineUpStartTime = Time.time;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity);
            if (hit.collider == _victim.GetComponent<BoxCollider2D>())
            {
                _lookingForVictim = false;
                _probeTimer = _probeTime;
                StartCoroutine(WaitBeforeProbe());
            }
        }
    }
    private void Probe()
    {
        if (_distanceBetweenEnemyAndT(_victim.transform.position) <= _probeRange)
        {
            print("PROBE MEMES HAPPEN HERE");
            return;
        }
        Vector3 newDir = _victim.transform.position - transform.position;
        transform.position += newDir.normalized * _speed * Time.deltaTime;
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
    private IEnumerator WaitBeforeProbe()
    {
        yield return new WaitForSeconds(2);
        _lookingAtVictim = true;
    }
}
