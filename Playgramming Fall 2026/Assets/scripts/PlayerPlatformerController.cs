using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPlatformerController : PhysicsObject
{
    public float jumpTakeOffSpeed = 7.0f;
	public float maxSpeed = 7.0f;
	public float fallForce = .5f;
	public float coyoteTime = .2f;

	[Header("Checks")]
	[SerializeField] private Transform _groundCheckPoint;
	//Size of groundCheck depends on the size of your character generally you want them slightly small than width (for ground) and height (for the wall check)
	[SerializeField] private Vector2 _groundCheckSize = new Vector2(0.49f, 0.03f);

	[Header("Layers & Tags")]
	[SerializeField] private LayerMask _groundLayer;


	private SpriteRenderer spriteRenderer;
	private PlayerInput playerInput;
    private Animator animator;

	public bool IsJumping { get; private set; }

	private void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		playerInput = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
	}



	private void Update()
	{
		animator.SetBool("grounded", grounded);
		animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

		lastOnGroundTime -= Time.deltaTime;

		if (velocity.y < 0)
		{
			velocity.y -= fallForce;
		}
		
	}

	public void Jump(InputAction.CallbackContext context)
	{
		if (context.performed && grounded)
		{
			if (Physics2D.OverlapBox(_groundCheckPoint.position, _groundCheckSize, 0, _groundLayer) && !IsJumping) //checks if set box overlaps with ground
			{
				lastOnGroundTime = coyoteTime; //if so sets the lastGrounded to coyoteTime
				velocity.y = jumpTakeOffSpeed;
			}
			//Debug.Log("Jump performed");
			//velocity.y = jumpTakeOffSpeed;
		}
		else if (context.canceled)
		{
			if (velocity.y < 0)
			{
				velocity.y += fallForce;	
			}
		}
	}

	public void Move(InputAction.CallbackContext context)
	{
		Vector2 move = Vector2.zero;

		move.x = context.ReadValue<Vector2>().x;

		bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < -0.01f));
		if (flipSprite)
		{
			spriteRenderer.flipX = !spriteRenderer.flipX;
		}

		targetVelocity = move * maxSpeed;
	}

}
/*
if (Physics2D.OverlapBox(_groundCheckPoint.position, _groundCheckSize, 0, _groundLayer) && !IsJumping) //checks if set box overlaps with ground
{
	lastOnGroundTime = coyoteTime; //if so sets the lastGrounded to coyoteTime
}
*/