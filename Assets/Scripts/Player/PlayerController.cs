using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _movementVelocity;
    [SerializeField] private SpriteRenderer _aimSprite;
    [SerializeField] private PlayerRotator _playerRotator;
    [SerializeField] private UserMoveTimeLimiter _userMoveTimeLimiter;
    
    
    
    private Vector3 _startPosition;
    private bool _isMoving;

    private void Awake()
    {
        _startPosition = transform.position;
        _isMoving = false;
    }

    // вызывается через ивент при возвращении игрока в старт поинт триггер
    [UsedImplicitly]
    public void ResetPosition()
    {
        if (_isMoving)
        {
            _isMoving = !_isMoving;
            _aimSprite.enabled = true;
            _playerRotator.StartRotation();
            _userMoveTimeLimiter.RestartTimeLimiter();
            _rigidbody.velocity = Vector2.zero;
            transform.position = _startPosition;
        }
    }

    [UsedImplicitly]
    public void Move()
    {
        if (!_isMoving)
        {
            _isMoving = !_isMoving;
            _aimSprite.enabled = false;
            _playerRotator.StopRotation();
            _userMoveTimeLimiter.StopTimerLimiter();
            _rigidbody.velocity = transform.up * _movementVelocity;
        }
    }

    // вызывается через ивент, при коллизии игрока с врагом 
    [UsedImplicitly]
    public void ChangeDirection()
    {
        _rigidbody.velocity *= -1;
    }
    
    //вызывается по ивенту смерти игрока
    [UsedImplicitly]
    public void OnPlayerDied()
    {
        Destroy(gameObject);
    }
}