using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuggerManController : MonoBehaviour
{
    [SerializeField] private bool _canPerformAction = true;
    [SerializeField] private float _followDistance;
    [SerializeField] private float _attackDistance;
    // After attacking it take this long before the enemy can do move/attack
    [SerializeField] private float _actionRecovery;
    [SerializeField] private Vector3 _movementSpeed = new Vector3(2, 0, 0);
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Transform _muggeeTransform;
    // GameObject(s) whomst get mugged
    [SerializeField] private GameObject[] _memes;

    private void Update()
    {
        TaskFinder();

        //print(Vector2.Distance(transform.position, _playerTransform.position));
    }

    private void DoTask()
    {

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
            // if the plaeyr is within attack-range attack them

            AttackPlayer();
            print("1");
        }
        else if (distanceBetweenMuggerAndPlayer <= _followDistance)
        {
            // if the player is within follow-range chase them

            FindTarget(_playerTransform);
            transform.position += _movementSpeed * Time.deltaTime;
            print("2");
        }
        else if (distanceBetweenMuggerAndPlayer > _followDistance && Vector2.Distance(transform.position, _muggeeTransform.position) > 1.0f)
        {
            // If the player is too far away -> move towards the meme to fuck with

            FindTarget(_muggeeTransform);
            transform.position += _movementSpeed * Time.deltaTime;
            
            print("3");
        }
        else if (Vector2.Distance(transform.position, _muggeeTransform.position) <= 1.0f)
        {
            // if the whatever you are trying to fuck with is within range -> fuck with it
            print("4");
        }
    }
    private void FindTarget(Transform t)
    {
        // there is a better way to do this for

        float dotMeme = Vector2.Dot(transform.right, t.position);

        if (dotMeme == 0)
        {
            print("UMMM");

            _movementSpeed = new Vector3(0, 0, 0);
        }
        else if (dotMeme > 0)
        {
            //print("Player is to the right!");

            _movementSpeed = new Vector3(2, 0);
        }
        else
        {
            //print("Player is to the left!");

            _movementSpeed = new Vector3(-2, 0);
        }
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
