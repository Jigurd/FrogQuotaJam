using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollowCamera : MonoBehaviour
{
    [SerializeField] private Movement _playerMovement = null;
    [SerializeField] private PlayerController _playerController = null;
    [SerializeField] private Vector3 _groundedOffset = Vector2.zero;
    [SerializeField] private Vector3 _flyingOffset = Vector2.zero;
    [SerializeField] private float _lookAheadVelocityThreshold = 0.15f;
    [SerializeField] private float _groundedLookAheadAmount = 25.0f;
    [SerializeField] private float _flyingLookAheadAmount = 25.0f;
    [SerializeField] private float _groundedHorizontalLerpValue = 18.4f;
    [SerializeField] private float _flyingHorizontalLerpValue = 18.4f;
    [SerializeField] private float _verticalLerpValue = 4.0f;

    private void Update()
    {
        if (_playerMovement == null) return;

        var lookAhead = Vector3.right;
        if (Mathf.Abs(_playerMovement.velocity.x) >= _lookAheadVelocityThreshold)
        {
            if (_playerMovement.velocity.x > 0)
            {
                lookAhead *=
                    _playerController.IsFlying
                        ? _flyingLookAheadAmount
                        : _groundedLookAheadAmount;
            }
            else if (_playerMovement.velocity.x < 0)
            {
                lookAhead *=
                    _playerController.IsFlying
                        ? -_flyingLookAheadAmount
                        : -_groundedLookAheadAmount;
            }
        }
        else
        {
            lookAhead *= 0.0f;
        }

        var targetPosition = _playerMovement.transform.position + lookAhead;
        if (_playerController.IsFlying)
        {
            targetPosition += _flyingOffset;
        }
        else
        {
            targetPosition += _groundedOffset;
        }

        transform.position = new Vector3(
            Mathf.Lerp(
                transform.position.x,
                targetPosition.x,
                //Mathf.Max(1.0f, Mathf.Abs(distance.x)) * _lerpValue * Time.deltaTime),
                (_playerController.IsFlying
                    ? _flyingHorizontalLerpValue
                    : _groundedHorizontalLerpValue) * Time.deltaTime),
            Mathf.Lerp(
                transform.position.y,
                targetPosition.y,
                //Mathf.Max(1.0f, Mathf.Abs(distance.y)) * _lerpValue * Time.deltaTime),
                _verticalLerpValue * Time.deltaTime),
            transform.position.z
        );
    }
}
