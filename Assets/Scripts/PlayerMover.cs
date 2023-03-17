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

    private new Rigidbody2D rigidbody2D;
    private Vector2 input;

    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        input = new Vector2();
    }

    void Update()
    {
        input.x = player == Player.Player1 ? Input.GetAxis("Horizontal") : Input.GetAxis("HorizontalP2");
        input.y = player == Player.Player1 ? Input.GetAxis("Vertical") : Input.GetAxis("VerticalP2");

        rigidbody2D.MovePosition(rigidbody2D.position +
            speed * input.x * Time.deltaTime * Vector2.right +
            speed * input.y * Time.deltaTime * Vector2.up);

        if (input.sqrMagnitude > 0.1f)
        {
            float angle = rotationClamped ?
                AngleClamper(Vector3.SignedAngle(Vector2.right, input, Vector3.forward)) :
                Vector3.SignedAngle(Vector2.right, input, Vector3.forward);

            rigidbody2D.MoveRotation(Quaternion.AngleAxis(angle, Vector3.forward));
        }
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
