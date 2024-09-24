using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    //Setting Variables

    //player speed
    [SerializeField]
    private float _speed;
    //rotation speed of player
    [SerializeField]
    private float _rotationSpeed;
    //player variables
    private Rigidbody2D _rigidbody;
    private Vector2 _movementInput;
    private Vector2 _smoothedMovementInput;
    private Vector2 _movementInputSmoothVelocity;
    //setting rigidbody on start
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        
        SetPlayerVelocity();
        RotateInDirectionOfInput();
    }

    //Setting the players speed/velocity
    private void SetPlayerVelocity()
    {
        _smoothedMovementInput = Vector2.SmoothDamp(
                    _smoothedMovementInput,
                    _movementInput,
                    ref _movementInputSmoothVelocity,
                    0.1f);

        _rigidbody.velocity = _smoothedMovementInput * _speed;
    }

    //Player rotation based on where you move
    private void RotateInDirectionOfInput()
    {
        // if player input is not equal to zero, rotate player in direction 
        if (_movementInput != Vector2.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(transform.forward, _smoothedMovementInput);
            Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

            _rigidbody.MoveRotation(rotation);
        }
    }
    //player movement 
    private void OnMove(InputValue inputValue)
    {
        _movementInput = inputValue.Get<Vector2>();
    }
}

