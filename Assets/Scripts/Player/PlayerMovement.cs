using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    //Setting Variables

    //player speed
    [SerializeField]
    private float speed;
    //rotation speed of player
    [SerializeField]
    private float rotationSpeed;
    //player variables
    private Rigidbody2D _rigidbody;
    private Vector2 movementInput;
    private Vector2 smoothedMovementInput;
    private Vector2 movementInputSmoothVelocity;
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
        smoothedMovementInput = Vector2.SmoothDamp(
                    smoothedMovementInput,
                    movementInput,
                    ref movementInputSmoothVelocity,
                    0.1f);

        _rigidbody.velocity = smoothedMovementInput * speed;
    }

    //Player rotation based on where you move
    private void RotateInDirectionOfInput()
    {
        // if player input is not equal to zero, rotate player in direction 
        if (movementInput != Vector2.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(transform.forward, smoothedMovementInput);
            Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            _rigidbody.MoveRotation(rotation);
        }
    }
    //player movement 
    private void OnMove(InputValue inputValue)
    {
        movementInput = inputValue.Get<Vector2>();
    }
}

