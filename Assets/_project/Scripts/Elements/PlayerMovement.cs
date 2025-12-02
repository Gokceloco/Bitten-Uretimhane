using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameDirector gameDirector;
    public float walkSpeed;
    public float runSpeed;

    public float jumpForce;
    public float fallSpeedBonus;

    public LayerMask jumpLayerMask;
    public LayerMask lookAtLayerMask;

    public Camera mainCamera;
    
    private Rigidbody _rb;

    private Vector3 _initialMousePos;

    private PlayerAnimator _playerAnimator;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _playerAnimator = GetComponent<PlayerAnimator>();
    }
    private void Update()
    {
        if (gameDirector.gameState != GameState.GamePlay)
        {
            if (gameDirector.gameState != GameState.Win)
            {
                _playerAnimator.ChangeAnimationState("Idle");
            }
            _rb.linearVelocity = Vector3.zero;
            return;
        }

        Vector3 direction = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector3.back;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector3.right;
        }

        var isTouchingGround = Physics.Raycast(transform.position + .1f * Vector3.up, Vector3.down, .2f, jumpLayerMask);

        if (Input.GetKeyDown(KeyCode.Space) && isTouchingGround)
        {
            Jump();
        }

        LookAtMouse();
        MovePlayer(direction);

        var angle = Vector3.SignedAngle(transform.forward, direction, Vector3.up);
        _playerAnimator.SetRunDirection(angle);
    }

    private void LookAtMouse()
    {
        if (Physics.Raycast(mainCamera.transform.position,
        mainCamera.ScreenPointToRay(Input.mousePosition).direction,
        out RaycastHit hit, 40, lookAtLayerMask))
        {
            var lookPos = hit.point;
            lookPos.y = transform.position.y;
            transform.LookAt(lookPos);
        }
    }

    private void Jump()
    {
        _rb.AddForce(Vector3.up * jumpForce);
    }

    private void MovePlayer(Vector3 direction)
    {
        var yVelocity = _rb.linearVelocity;
        yVelocity.x = 0;
        yVelocity.z = 0;

        if (yVelocity.y < 0)
        {
            yVelocity.y -= fallSpeedBonus * Time.deltaTime;
        }

        var speed = walkSpeed;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = runSpeed;
        }

        if (gameDirector.gameState == GameState.GamePlay)
        {
            if (direction.magnitude > 0)
            {
                _playerAnimator.ChangeAnimationState("Run");
            }
            else
            {
                _playerAnimator.ChangeAnimationState("Idle");
            }
        }        

        _rb.linearVelocity = direction.normalized * speed + yVelocity;
    }
}
