using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KongController : MonoBehaviour
{
    [SerializeField] Transform _target = null;
    [SerializeField] Transform _player = null;
    [SerializeField] int _speed = 1;
    [SerializeField] private float _attackDistance = 2.25f;
    [SerializeField] private float _actionRecovery = 1.25f;

    private bool _canPerformAction = true;


    private void Update()
    {
        DoIt();
    }

    private void Climb()
    {
        if (transform.position.y >= _target.position.y)
        {
            print("WEEWOO");
            return; 
        }

        transform.position += Vector3.up * _speed * Time.deltaTime;
    }
    private void DoIt()
    {
        if (!_canPerformAction)
            return;

        float distanceBetweenPlayerAndKong = Vector2.Distance(transform.position, _player.position);

        if (distanceBetweenPlayerAndKong <= _attackDistance)
        {
            StartCoroutine(RecoverAction());
            return;
        }

        Climb();
    }
    IEnumerator RecoverAction()
    {
        print("ATTACK!");
        
        _canPerformAction = false;
        yield return new WaitForSeconds(_actionRecovery);

        _canPerformAction = true;
    }
}
