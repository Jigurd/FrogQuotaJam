using System.Collections;
using UnityEngine;

public class MuggerManController : MonoBehaviour
{
    [SerializeField] private float _followDistance = 0;
    [SerializeField] private float _attackDistance = 0;
    // After attacking it take this long before the enemy can do move/attack
    [SerializeField] private float _actionRecovery = 0;
    [SerializeField] private Transform _playerTransform = null;
    [SerializeField] private Transform _muggeeTransform = null;
    [SerializeField] private int _speed = 2;
    
    private bool _canPerformAction = true;
    private int _movementSpeed;

    private void Update()
    {
        if (GameState.IsPaused)
        {
            return;
        }

        TaskFinder();

        //print(Vector2.Distance(transform.position, _playerTransform.position));
    }

    private void TaskFinder()
    {
        /*
         */
        if (_canPerformAction == false)
            return;

        //LookAtPlayer();

        float distanceBetweenMuggerAndPlayer = Vector2.Distance(transform.position, _playerTransform.position);

        if (distanceBetweenMuggerAndPlayer <= _attackDistance)
        {
            // if the player is within attack-range attack them

            AttackPlayer();
        }
        else if (distanceBetweenMuggerAndPlayer <= _followDistance)
        {
            // if the player is within follow-range chase them

            FindTarget(_playerTransform);
            transform.position += Vector3.right * _movementSpeed * Time.deltaTime;
        }
        else if (distanceBetweenMuggerAndPlayer > _followDistance && Vector2.Distance(transform.position, _muggeeTransform.position) > 1.0f)
        {
            // If the player is too far away -> move towards the meme to fuck with

            FindTarget(_muggeeTransform);
            transform.position += Vector3.right * _movementSpeed * Time.deltaTime;
            
        }
        else if (Vector2.Distance(transform.position, _muggeeTransform.position) <= 1.0f)
        {
            // if the whatever you are trying to fuck with is within range -> fuck with it

            DoTask();
        }
    }
    private void FindTarget(Transform t)
    {
        if (t == null)
        {
            print("bad memes :c");
            return;
        }

        if (t.position.x - transform.position.x > 0)
        {
            _movementSpeed = _speed;
        }
        else
        {
            _movementSpeed = -_speed;
        }
    }
    private void DoTask()
    {
        print("MUGGIN");
    }
    private void AttackPlayer()
    {
        _canPerformAction = false;

        StartCoroutine(Attack());
    }
    IEnumerator Attack()
    {
        print("attack animation??");
        yield return new WaitForSeconds(.25f);
        print("KAPOW! le attacke generation");

        yield return new WaitForSeconds(_actionRecovery);

        _canPerformAction = true;
    }
}
