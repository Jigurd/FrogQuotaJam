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

    private CombatActor _combatActor;
    private Movement _movement;
    
    private bool _canPerformAction = true;
    private int _movementSpeed;

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

        TaskFinder();

        //print(Vector2.Distance(transform.position, _playerTransform.position));
    }

    private void TaskFinder()
    {

        if (_canPerformAction == false)
            return;

        //LookAtPlayer();

        float distanceBetweenMuggerAndPlayer = Vector2.Distance(transform.position, _playerTransform.position);

        if (distanceBetweenMuggerAndPlayer <= _attackDistance)
        {
            // if the player is within attack-range attack them

            Attack(_playerTransform);
        }
        else if (distanceBetweenMuggerAndPlayer <= _followDistance)
        {
            // if the player is within follow-range chase them
           

            MoveTowards(_playerTransform);
        }
        else if (distanceBetweenMuggerAndPlayer > _followDistance && Vector2.Distance(transform.position, _muggeeTransform.position) > 1.0f)
        {
            Debug.Log("Gon getchu");
            // If the player is too far away -> move towards the meme to fuck with
            MoveTowards(_muggeeTransform);            
        }
        else if (Vector2.Distance(transform.position, _muggeeTransform.position) <= 1.0f)
        {
            // if the whatever you are trying to fuck with is within range -> fuck with it
            Attack(_muggeeTransform);
        }
    }
    private void MoveTowards(Transform t)
    {
        if (t == null)
        {
            print("bad memes :c");
            return;
        }

        if (t.position.x - transform.position.x > 0)
        {
            _movement.velocity.x += _movement.moveSpeed;
        }
        else
        {

            _movement.velocity.x -= _movement.moveSpeed;
        }
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
}
