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

    private float currentLife;

    void Awake()
    {
        currentLife = maxLife;
    }

    public void TakeDamage(float damage)
    {
        currentLife = Mathf.Max(minLife, currentLife - damage);
        OnLifeChange.Invoke(Mathf.InverseLerp(minLife, maxLife, currentLife));
    }
}
