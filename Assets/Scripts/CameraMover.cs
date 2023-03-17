using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Camera))]
public class CameraMover : MonoBehaviour
{
    [SerializeField]
    private float minOrtographicSize = 5.4f;
    [SerializeField]
    private float maxOrtographicSize = 10f;
    [SerializeField]
    private float minPlayersDistance = 12f;
    [SerializeField]
    private float maxPlayersDistance = 37f;
    [SerializeField]
    private Transform player1;
    [SerializeField]
    private Transform player2;

    private new Camera camera;

    private Vector3 currentPlayersCenter;
    private float currentPlayerDistance;

    void Awake()
    {
        camera = GetComponent<Camera>();
    }

    void Update()
    {
        currentPlayersCenter = Vector3.Lerp(player1.position, player2.position, 0.5f);
        currentPlayersCenter.z = -10;
        transform.position = currentPlayersCenter;

        currentPlayerDistance = Vector3.Distance(player1.position, player2.position);
        camera.orthographicSize = Mathf.Lerp(minOrtographicSize, maxOrtographicSize, 
            Mathf.InverseLerp(minPlayersDistance, maxPlayersDistance, currentPlayerDistance));
    }

    [ContextMenu("Distance")]
    void CalculateDistanceDebug()
    {
        Debug.Log(Vector3.Distance(player1.position, player2.position));
    }
}
