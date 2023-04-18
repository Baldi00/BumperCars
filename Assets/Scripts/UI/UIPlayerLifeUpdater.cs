using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
[RequireComponent(typeof(Image))]
public class UIPlayerLifeUpdater : MonoBehaviour
{
    [SerializeField]
    [Range(0, 1)]
    private float midThreshold;
    [SerializeField]
    [Range(0, 1)]
    private float lowThreshold;
    [SerializeField]
    private Color maxHealthColor;
    [SerializeField]
    private Color midHealthColor;
    [SerializeField]
    private Color minHealthColor;

    private RectTransform rectTransform;
    private Image healthBar;
    private float maxHealthbarSize;


    void Awake()
    {
        rectTransform = ((RectTransform)transform);
        healthBar = GetComponent<Image>();
        maxHealthbarSize = rectTransform.rect.width;
    }

    public void UpdatePlayerHealthBar(float currentLifePercentage)
    {
        rectTransform.sizeDelta = new Vector2(Mathf.Lerp(0, maxHealthbarSize, currentLifePercentage), rectTransform.sizeDelta.y);

        if (currentLifePercentage <= lowThreshold)
            healthBar.color = minHealthColor;
        else if (currentLifePercentage <= midThreshold)
            healthBar.color = midHealthColor;
        else
            healthBar.color = maxHealthColor;
    }
}
