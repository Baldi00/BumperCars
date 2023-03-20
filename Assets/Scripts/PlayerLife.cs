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
    private GameObject explosionPrefab;

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
            GameManager.Instance.player1.StopMoving();
            GameManager.Instance.player2.StopMoving();
            Destroy(gameObject);
        }
    }
}
