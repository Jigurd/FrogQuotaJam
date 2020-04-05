﻿using System.Collections;
using System;
using UnityEngine;
using System.Linq;

public class MuggerManController : MonoBehaviour
{
    [SerializeField] private float _followDistance = 0;
    // After attacking it take this long before the enemy can do move/attack
    [SerializeField] private float _actionRecovery = 0;
    [SerializeField] private Transform _victimTransform = null;

    private CombatActor _combatActor;
    private Movement _movement;

    private bool _canPerformAction = true;

    private State _state = State.FindVictim;

    private void Start()
    {
        _movement = GetComponent<Movement>();
        _combatActor = GetComponent<CombatActor>();

    }

    private void Update()
    {
        if (GameState.IsPaused)
        {
            return;
        }

        Do();

        //print(Vector2.Distance(transform.position, _playerTransform.position));
    }

    private void Do()
    {

        if (_canPerformAction == false)
            return;

        switch (_state)
        {
            case State.FindVictim: _findVictim(); break;
            case State.ChaseVictim: _chaseVictim(); break;
            case State.Mug: _mug(); break;
        }

    }
    private void MoveTowards(Transform t)
    {
        Vector3 pathToTarget = t.position - transform.position;

        float distToMove;

        //if we are closer to target than our speed
        if (Math.Abs(pathToTarget.x) > _movement.MoveSpeed)
        {
            //we will move our distance
            distToMove = pathToTarget.x;
        }
        else
        {
            distToMove = _movement.MoveSpeed * Math.Sign(pathToTarget.x);
        }

        if (t.position.x - transform.position.x > 0)
        {
            _movement.velocity.x = distToMove * Time.deltaTime;
        }


        //Debug.Log((transform.position - _muggeeTransform.position).magnitude);
    }
    private void DoTask()
    {
        print("MUGGIN");
    }
    private void Attack(Transform target)
    {
        _canPerformAction = false;

        StartCoroutine(AttackPlayer(target));
    }
    IEnumerator AttackPlayer(Transform target)
    {
        print("attack animation??");
        yield return new WaitForSeconds(.25f);
        print("KAPOW! le attacke generation");
        _combatActor.Attack(target.position);

        yield return new WaitForSeconds(_actionRecovery);

        _canPerformAction = true;
    }

    private void _chaseVictim()
    {
        MoveTowards(_victimTransform);

        //if we are within attack range, start mugging
        if ((transform.position - _victimTransform.position).magnitude < _combatActor.AttackRange)
        {
            _state = State.Mug;
        }

    }

    private void _findVictim()
    {
        Debug.Log("wtb victim");
        _victimTransform = Storyteller.Civilians
            .OrderByDescending(civilian => (transform.position - civilian.transform.position).sqrMagnitude)
            .FirstOrDefault().transform;

        _state = State.ChaseVictim;
    }

    private void _mug()
    {
        if (_victimTransform == null)
        {
            _state = State.FindVictim;
        }
        else
        {
            Attack(_victimTransform);
        }
    }

    private enum State
    {
        FindVictim,
        ChaseVictim,
        Mug
    }
}
