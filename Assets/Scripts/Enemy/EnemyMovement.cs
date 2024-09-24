using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    //Setting Variables 
    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _rotationSpeed;

    private Rigidbody2D _rigidbody;
    private PlayerAwarenessController _playerAwarenessController;
    private Vector2 _targetDirection;

    //On Awake set rigidbody and playerAwareness 
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerAwarenessController = GetComponent<PlayerAwarenessController>();
    }


    private void FixedUpdate()
    {
        UpdateTargetDirection();
        RotateTowardsTarget();
        SetVelocity();
    }

    //update target location for constant chasing 
    private void UpdateTargetDirection()
    {
        if (_playerAwarenessController.AwareOfPlayer)
        {
            _targetDirection = _playerAwarenessController.DirectionToPlayer;
        }
        else
        {
            _targetDirection = Vector2.zero;
        }
    }

    //rotate zombies to face player 
    private void RotateTowardsTarget()
    {
        if (_targetDirection == Vector2.zero)
        {
            return;
        }
        //rotate zombie
        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, _targetDirection);
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

        _rigidbody.SetRotation(rotation);
    }

    //set velocity of enemy to 0 if not chasing player 
    private void SetVelocity()
    {
        if (_targetDirection == Vector2.zero)
        {
            _rigidbody.velocity = Vector2.zero;
        }
        else
        {
            _rigidbody.velocity = transform.up * _speed;
        }
    }
}


