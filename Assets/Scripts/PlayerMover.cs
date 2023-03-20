using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    private enum Player
    {
        Player1,
        Player2
    }

    [SerializeField]
    private Player player;
    [SerializeField]
    private float speed;
    [SerializeField]
    private bool rotationClamped;
    [SerializeField]
    private float collisionBounceDuration;
    [SerializeField]
    private float littleCollisionBounceSpeed;
    [SerializeField]
    private float strongCollisionBounceSpeed;
    [SerializeField]
    private AnimationCurve bounceAnimationCurve;

    private new Rigidbody2D rigidbody2D;
    private Vector2 input;
    private bool isBouncing;

    private float collisionBounceTimer;
    private Vector2 collisionBounceDirection;
    private float currentBounceSpeed;

    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        input = new Vector2();
    }

    void Update()
    {
        if (!isBouncing)
        {
            input.x = player == Player.Player1 ? Input.GetAxis("Horizontal") : Input.GetAxis("HorizontalP2");
            input.y = player == Player.Player1 ? Input.GetAxis("Vertical") : Input.GetAxis("VerticalP2");

            rigidbody2D.MovePosition(rigidbody2D.position +
                speed * input.x * Time.deltaTime * Vector2.right +
                speed * input.y * Time.deltaTime * Vector2.up);
        }
        else
        {
            rigidbody2D.MovePosition(rigidbody2D.position +
                currentBounceSpeed *
                bounceAnimationCurve.Evaluate(collisionBounceTimer / collisionBounceDuration) *
                Time.deltaTime * collisionBounceDirection);

            collisionBounceTimer += Time.deltaTime;
            if (collisionBounceTimer >= collisionBounceDuration)
                isBouncing = false;
        }

        if (input.sqrMagnitude > 0.1f)
        {
            float angle = rotationClamped ?
                AngleClamper(Vector3.SignedAngle(Vector2.right, input, Vector3.forward)) :
                Vector3.SignedAngle(Vector2.right, input, Vector3.forward);

            rigidbody2D.MoveRotation(Quaternion.AngleAxis(angle, Vector3.forward));
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isBouncing)
            return;

        float currentBounceSpeed = littleCollisionBounceSpeed;
        if (collision.collider.CompareTag("CarFrontCollider"))
        {
            if (collision.otherCollider.CompareTag("CarFrontCollider"))
            {
                GetComponent<PlayerLife>().TakeDamage(5);
                currentBounceSpeed = littleCollisionBounceSpeed;
            }
            else if (collision.otherCollider.CompareTag("CarBackCollider"))
            {
                GetComponent<PlayerLife>().TakeDamage(collision.relativeVelocity.magnitude * 2);
                currentBounceSpeed = strongCollisionBounceSpeed;
            }
        }

        StartBounce(collision.relativeVelocity.normalized, currentBounceSpeed);
    }

    public void StartBounce(Vector2 bounceDirection, float bounceSpeed)
    {
        isBouncing = true;
        collisionBounceTimer = 0;
        collisionBounceDirection = bounceDirection;
        currentBounceSpeed = bounceSpeed;
        input.x = input.y = 0;
    }

    private float AngleClamper(float rawAngle)
    {
        if (rawAngle < 0)
            rawAngle += 360;

        if (rawAngle >= 0f && rawAngle < 22.5f)
            return 0;
        if (rawAngle >= 22.5f && rawAngle < 67.5f)
            return 45;
        if (rawAngle >= 67.5f && rawAngle < 112.5f)
            return 90;
        if (rawAngle >= 112.5f && rawAngle < 157.5f)
            return 135;
        if (rawAngle >= 157.5f && rawAngle < 202.5f)
            return 180;
        if (rawAngle >= 202.5f && rawAngle < 247.5f)
            return 225;
        if (rawAngle >= 247.5f && rawAngle < 292.5f)
            return 270;
        if (rawAngle >= 292.5f && rawAngle < 337.5f)
            return 315;
        return 0;
    }
}
