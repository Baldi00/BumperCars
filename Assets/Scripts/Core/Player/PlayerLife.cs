using UnityEngine;
using UnityEngine.Events;

public class PlayerLife : MonoBehaviour
{
    [SerializeField]
    private float maxLife = 100;
    [SerializeField]
    private float minLife = 0;
    [SerializeField]
    private UnityEvent<float> OnLifeChange;
    [SerializeField]
    private UnityEvent onPlayerDies;
    [SerializeField]
    private GameObject explosionPrefab;
    [SerializeField]
    private PlayerNumber player;

    private float currentLife;

    void Awake()
    {
        currentLife = maxLife;
    }

    public void TakeDamage(float damage)
    {
        currentLife = Mathf.Max(minLife, currentLife - damage);
        OnLifeChange.Invoke(Mathf.InverseLerp(minLife, maxLife, currentLife));
        if (currentLife <= 0)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            GameManager.Instance.camera.StopUpdatePositionAndZoom();
            GameManager.Instance.endGameManager.ShowEndGameUIWithWinner(player == PlayerNumber.Player1 ? PlayerNumber.Player2 : PlayerNumber.Player1);
            GameManager.Instance.IsPaused = true;
            onPlayerDies?.Invoke();
            Destroy(gameObject);
        }
    }
}
