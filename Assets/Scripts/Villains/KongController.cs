using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KongController : MonoBehaviour
{
    [SerializeField] Transform _target = null;
    [SerializeField] int _speed = 1;
    [SerializeField] private float _actionRecovery = 1.25f;

    private bool _canPerformAction = true;
    private GameObject _building;

    private void Start()
    {
        _building = transform.parent.gameObject;
        transform.parent = null;
    }
    private void Update()
    {
        if (!_canPerformAction)
            return;
        Climb();
    }

    private void Climb()
    {
        if (transform.position.y >= _target.position.y)
        {
            if (_building)
            {
                StartCoroutine(KongSmash());
            }
            else
            {
                GetComponentInChildren<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                Destroy(gameObject, 3.5f);
                _canPerformAction = false;
            }
            return; 
        }

        transform.position += Vector3.up * _speed * Time.deltaTime;
    }
    private IEnumerator KongSmash()
    {
        _canPerformAction = false;
        print("YEET! ME KONG ME SMASH");
        _building.GetComponent<Damageable>().Damage(5);
        yield return new WaitForSeconds(_actionRecovery);
        _canPerformAction = true;
    }
}
