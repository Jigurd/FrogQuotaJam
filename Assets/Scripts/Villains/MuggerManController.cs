using System.Collections;
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
        GetComponent<Damageable>().OnDie += OnDie;
    }

    private void Update()
    {
        if (GameState.IsPaused)
        {
            return;
        }

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
        if (Math.Abs(pathToTarget.x) > _movement.Speed)
        {
            //we will move our distance
            distToMove = pathToTarget.x;
        }
        else
        {
            distToMove = _movement.Speed * Math.Sign(pathToTarget.x);
        }

        if (Math.Abs(t.position.x - transform.position.x) > 0)
        {
            _movement.velocity.x = distToMove * Time.deltaTime;
        }
    }

    private void Attack(Transform target)
    {
        _canPerformAction = false;

        StartCoroutine(AttackPlayer(target));
    }
    IEnumerator AttackPlayer(Transform target)
    {
        yield return new WaitForSeconds(.25f);

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
            _movement.velocity.x = 0;
            _state = State.Mug;
        }
    }

    private void _findVictim()
    {
        if (Storyteller.Civilians.Count == 0)
        {
            return;
        }

        _victimTransform = Storyteller.Civilians
            .OrderBy(civilian => (transform.position - civilian.transform.position).sqrMagnitude)
            .FirstOrDefault().transform;

        Debug.Log(_victimTransform);
        _state = State.ChaseVictim;
    }

    private void _mug()
    {
        if (_victimTransform == null)
        {
            _state = State.FindVictim;
        }
        else if ((transform.position - _victimTransform.position).magnitude < _combatActor.AttackRange)
        {
            Attack(_victimTransform);
        }
        else
        {
            _state = State.ChaseVictim;
        }
    }

    private void OnDie()
    {
        Highscores.Instance.UpdateCurrentScore(100);
    }

    private enum State
    {
        FindVictim,
        ChaseVictim,
        Mug
    }
}
