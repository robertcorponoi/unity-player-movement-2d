using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEditor;
// using UnityEditor.UIElements;

/// <summary>
/// Defines the directions that can be passed to `faceDirection`.
/// </summary>
enum Direction
{
  Up,
  Down,
  Left,
  Right
}

/// <summary>
/// Controls a sprite in 2D space.
/// </summary>
public class PlayerMovement2D : MonoBehaviour
{
  [Header("Components")]
  /// <summary>
  /// A reference to the SpriteRenderer component of this game object.
  /// </summary>
  public SpriteRenderer spriteRenderer;

  /// <summary>
  /// A reference to the Rigidbody2D component of this game object.
  /// </summary>
  public Rigidbody2D rigidBody;

  /// <summary>
  /// A reference to the Animator component of this game object.
  /// </summary>
  public Animator animator;

  [Header("Walking/Running")]
  /// <summary>
  /// The speed at which the game object should run.
  /// </summary>
  public float runSpeed = 2f;

  [Header("Jumping")]
  /// <summary>
  /// The layer that is defines objects that act as 'ground'.
  /// </summary>
  public LayerMask groundLayer;

  /// <summary>
  /// The base jump height for the game object.
  /// </summary>
  public float jumpHeight = 2f;

  /// <summary>
  /// The animator parameter for horizontal movement.
  /// </summary>
  private string velocityXParam = "velocityX";

  /// <summary>
  /// The animator parameter for vertical movement.
  /// </summary>
  private string velocityYParam = "velocityY";

  /// <summary>
  /// The animator parameter for indicating if the game object is grounded or not.
  /// </summary>
  private string isGroundedParam = "isGrounded";

  /// <summary>
  /// Indicates if the player is touching the ground or not.
  /// </summary>
  private bool isGrounded;

  /// <summary>
  /// Indicates if the player is jumping or not.
  /// </summary>
  private bool isJumping;

  /// <summary>
  /// The height of the raycast used to check for ground collision.
  /// </summary>
  private float groundCheckRaycastLength = 1.0f;

  /// <summary>
  /// The horizontal move gets multiplied by the run speed and is used to determine if the sprite should animate.
  /// </summary>
  private float horizontalMove = 0f;

  /// <summary>
  /// Contains the direction that the sprite is currently facing.
  /// </summary>
  private Direction directionFacing = Direction.Right;

  /// <summary>
  /// Once per frame, we check to see if there is any horizontal movement and apply that movement along with the run speed
  /// to the animator.true
  /// </summary>
  void Update()
  {
    horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

    if (horizontalMove > 0 && directionFacing == Direction.Left)
    {
      faceDirection(Direction.Right);

      directionFacing = Direction.Right;
    }
    else if (horizontalMove < 0 && directionFacing == Direction.Right)
    {
      faceDirection(Direction.Left);

      directionFacing = Direction.Left;
    }

    if (Input.GetKeyDown(KeyCode.Space)) isJumping = true;

    if (!isGrounded)
    {
      animator.SetBool(isGroundedParam, false);

      //Set the animator velocity equal to 1 * the vertical direction in which the player is moving 
      animator.SetFloat(velocityYParam, 1 * Mathf.Sign(rigidBody.velocity.y));
    }

    if (isGrounded)
    {
      animator.SetBool(isGroundedParam, true);
      animator.SetFloat(velocityYParam, 0);
    }
  }

  /// <summary>
  /// Perform physics-based calculations in fixed update.
  /// </summary>
  void FixedUpdate()
  {
    isGrounded = isOnGround();

    if (isJumping) jump();

    rigidBody.velocity = new Vector2(horizontalMove * runSpeed, rigidBody.velocity.y);

    if (isGrounded) animator.SetFloat(velocityXParam, Mathf.Abs(rigidBody.velocity.x));
  }

  /// <summary>
  /// Changes the direction of a sprite.
  ///
  /// If the left or right direction is selected, then the sprite renderer will be flipped on the x axis. If the
  /// up or down direction is selected, then it will use the animator.
  /// </summary>
  /// <param name="direction">The direction, from the Direction enum, to face.</param>
  private void faceDirection(Direction direction)
  {
    if (direction == Direction.Left || direction == Direction.Right)
    {
      Vector2 scale = rigidBody.transform.localScale;
      scale.x *= -1;

      rigidBody.transform.localScale = scale;
    }
  }

  /// <summary>
  /// Makes the character jump if the jump button is pressed and the character is grounded.
  /// </summary>
  private void jump()
  {
    if (!isOnGround()) return;

    rigidBody.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);

    isJumping = false;
  }

  /// <summary>
  /// Checks to see if the game object is in contact with the ground layer by using a raycast.
  /// </summary>
  /// <returns>Returns a boolean indicating if the game object is touching the ground or not</returns>
  private bool isOnGround()
  {
    Vector2 position = transform.position;
    Vector2 direction = Vector2.down;

    RaycastHit2D hit = Physics2D.Raycast(position, direction, groundCheckRaycastLength, groundLayer);

    if (hit.collider != null) return true;

    return false;
  }
}