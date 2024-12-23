using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public bool FacingLeft {  get { return FacingLeft; } set { FacingLeft = value; } }

    [SerializeField] private float moveSpeed = 1f;

    private PlayerControlls playerControlls;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRenderer;

    private bool facingLeft = false;

    private void Awake()
    {
        playerControlls = new PlayerControlls();
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        playerControlls.Enable();
    }

    private void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        AdjustPlayerFacingDirection();
        Move();
    }

    private void PlayerInput()
    {
        movement = playerControlls.Movement.Move.ReadValue<Vector2>();
        myAnimator.SetFloat("moveX", movement.x);
        myAnimator.SetFloat("moveY", movement.y);
    }

    private void Move()
    {
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.deltaTime));

    }
    private void AdjustPlayerFacingDirection()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        if(mousePos.x < playerScreenPoint.x)
        {
            mySpriteRenderer.flipX = true;
            facingLeft = true;
        } else
        {
            mySpriteRenderer.flipY = false; 
            facingLeft = false;
        }
    }
}
