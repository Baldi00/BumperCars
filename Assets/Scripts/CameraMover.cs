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
    private Transform player1;
    [SerializeField]
    private Transform player2;

    private new PixelPerfectCamera camera;

    private Vector3 currentPlayersCenter;
    private Vector2 currentPlayerDistance;

    void Awake()
    {
        camera = GetComponent<PixelPerfectCamera>();
    }

    void Update()
    {
        currentPlayersCenter = Vector3.Lerp(player1.position, player2.position, 0.5f);
        currentPlayersCenter.z = -10;
        transform.position = currentPlayersCenter;

        currentPlayerDistance = player1.position - player2.position;
        int ppuX = (int)Mathf.Lerp(maxPixelPerUnit, minPixelPerUnit, 
            Mathf.InverseLerp(minPlayersDistanceX, maxPlayersDistanceX, Mathf.Abs(currentPlayerDistance.x)));
        int ppuY = (int)Mathf.Lerp(maxPixelPerUnit, minPixelPerUnit, 
            Mathf.InverseLerp(minPlayersDistanceY, maxPlayersDistanceY, Mathf.Abs(currentPlayerDistance.y)));
        camera.assetsPPU = Mathf.Min(ppuX, ppuY);
    }

    [ContextMenu("Distance")]
    void CalculateDistanceDebug()
    {
        Debug.Log(Vector3.Distance(player1.position, player2.position));
    }
}
