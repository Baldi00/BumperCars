using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

[DisallowMultipleComponent]
[RequireComponent(typeof(PixelPerfectCamera))]
public class CameraMover : MonoBehaviour
{
    [SerializeField]
    private float minPixelPerUnit = 50f;
    [SerializeField]
    private float maxPixelPerUnit = 100f;
    [SerializeField]
    private float minPlayersDistanceX = 12f;
    [SerializeField]
    private float maxPlayersDistanceX = 33f;
    [SerializeField]
    private float minPlayersDistanceY = 0f;
    [SerializeField]
    private float maxPlayersDistanceY = 20f;
    [SerializeField]
    private PlayerMover player1;
    [SerializeField]
    private PlayerMover player2;
    [SerializeField]
    private float bounceSpeed;

    private new PixelPerfectCamera camera;

    private Vector3 currentPlayersCenter;
    private Vector2 currentPlayerDistance;

    void Awake()
    {
        camera = GetComponent<PixelPerfectCamera>();
    }

    void Update()
    {
        currentPlayersCenter = Vector3.Lerp(player1.transform.position, player2.transform.position, 0.5f);
        currentPlayersCenter.z = -10;
        transform.position = currentPlayersCenter;

        currentPlayerDistance = player1.transform.position - player2.transform.position;
        int ppuX = (int)Mathf.Lerp(maxPixelPerUnit, minPixelPerUnit,
            Mathf.InverseLerp(minPlayersDistanceX, maxPlayersDistanceX, Mathf.Abs(currentPlayerDistance.x)));
        int ppuY = (int)Mathf.Lerp(maxPixelPerUnit, minPixelPerUnit,
            Mathf.InverseLerp(minPlayersDistanceY, maxPlayersDistanceY, Mathf.Abs(currentPlayerDistance.y)));
        camera.assetsPPU = Mathf.Min(ppuX, ppuY);

        if (Mathf.Abs(currentPlayerDistance.x) > maxPlayersDistanceX)
        {
            player1.StartBounce(player1.transform.position.x > player2.transform.position.x ? Vector2.left : Vector2.right, bounceSpeed);
            player2.StartBounce(player1.transform.position.x > player2.transform.position.x ? Vector2.right : Vector2.left, bounceSpeed);
        }
        else if (Mathf.Abs(currentPlayerDistance.y) > maxPlayersDistanceY)
        {
            player1.StartBounce(player1.transform.position.y > player2.transform.position.y ? Vector2.down : Vector2.up, bounceSpeed);
            player2.StartBounce(player1.transform.position.y > player2.transform.position.y ? Vector2.up : Vector2.down, bounceSpeed);
        }
    }

    [ContextMenu("Distance")]
    void CalculateDistanceDebug()
    {
        Debug.Log(Vector3.Distance(player1.transform.position, player2.transform.position));
    }
}
