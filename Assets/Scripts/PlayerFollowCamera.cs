using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollowCamera : MonoBehaviour
{
    [SerializeField] private Movement _playerMovement = null;
    [SerializeField] private PlayerController _playerController = null;
    [SerializeField] private OutsideWindow _outsideWindow = null;
    [SerializeField] private Vector3 _groundedOffset = Vector2.zero;
    [SerializeField] private Vector3 _flyingOffset = Vector2.zero;
    [SerializeField] private float _lookAheadVelocityThreshold = 0.15f;
    [SerializeField] private float _groundedLookAheadAmount = 25.0f;
    [SerializeField] private float _flyingLookAheadAmount = 25.0f;
    [SerializeField] private float _groundedHorizontalLerpValue = 18.4f;
    [SerializeField] private float _flyingHorizontalLerpValue = 18.4f;
    [SerializeField] private float _verticalLerpValue = 4.0f;

    private Vector3 _targetPosition = Vector3.zero;

    private void Start()
    {
        transform.position = new Vector3(
            _outsideWindow.transform.position.x,
            _outsideWindow.transform.position.y,
            transform.position.z
        );
    }

    private void Update()
    {
        switch (GameState.GameMode)
        {
            case GameMode.City:
                CityModeUpdate();
                break;
            case GameMode.Office:
                OfficeModeUpdate();
                break;
        }

        transform.position = new Vector3(
            Mathf.Lerp(
                transform.position.x,
                _targetPosition.x,
                //Mathf.Max(1.0f, Mathf.Abs(distance.x)) * _lerpValue * Time.deltaTime),
                (_playerController.IsFlying
                    ? _flyingHorizontalLerpValue
                    : _groundedHorizontalLerpValue) * Time.deltaTime),
            Mathf.Lerp(
                transform.position.y,
                _targetPosition.y,
                //Mathf.Max(1.0f, Mathf.Abs(distance.y)) * _lerpValue * Time.deltaTime),
                _verticalLerpValue * Time.deltaTime),
            transform.position.z
        );
    }

    private void CityModeUpdate()
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

        _targetPosition = _playerMovement.transform.position + lookAhead;
        if (_playerController.IsFlying)
        {
            _targetPosition += _flyingOffset;
        }
        else
        {
            _targetPosition += _groundedOffset;
        }

    }

    private void OfficeModeUpdate()
    {
        if (_outsideWindow == null) return;
        _targetPosition = _outsideWindow.transform.position;
    }
}
