using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    [SerializeField]
    private float maxLife = 100;
    [SerializeField]
    private float minLife = 0;

    private float currentLife;

    void Awake()
    {
        currentLife = maxLife;
    }

    public void TakeDamage(float damage)
    {
        currentLife = Mathf.Max(minLife, currentLife - damage);
    }
}
