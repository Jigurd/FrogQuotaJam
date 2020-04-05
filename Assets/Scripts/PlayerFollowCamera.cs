using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollowCamera : MonoBehaviour
{
    [SerializeField] private Movement _playerMovement = null;
    [SerializeField] private float _lookAheadVelocityThreshold = 0.15f;
    [SerializeField] private float _lookAheadAmount = 25.0f;
    [SerializeField] private float _lerpValue = 50.0f;

    private void Update()
    {
        if (_playerMovement == null) return;

        var lookAhead = Vector3.right;
        if (Mathf.Abs(_playerMovement.velocity.x) >= _lookAheadVelocityThreshold)
        {
            if (_playerMovement.velocity.x > 0)
            {
                lookAhead *= _lookAheadAmount;
            }
            else if (_playerMovement.velocity.x < 0)
            {
                lookAhead *= -_lookAheadAmount;
            }
        }
        else
        {
            lookAhead *= 0.0f;
        }

        var targetPosition = _playerMovement.transform.position + lookAhead;
        var distance = targetPosition - transform.position;

        // This is magic.
        transform.position = new Vector3(
            Mathf.Lerp(
                transform.position.x,
                targetPosition.x,
                //Mathf.Max(1.0f, Mathf.Abs(distance.x)) * _lerpValue * Time.deltaTime),
                _lerpValue * Time.deltaTime),
            Mathf.Lerp(
                transform.position.y,
                targetPosition.y,
                //Mathf.Max(1.0f, Mathf.Abs(distance.y)) * _lerpValue * Time.deltaTime),
                _lerpValue * Time.deltaTime),
            transform.position.z
        );
    }
}
