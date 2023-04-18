using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[DisallowMultipleComponent]
public class UITextColorChanger : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;
    [SerializeField]
    private float fullHueChangeDuration;

    private float timer = 0;

    void OnValidate()
    {
        if (fullHueChangeDuration <= 0)
            fullHueChangeDuration = 0.1f;
    }

    void Update()
    {
        timer += Time.deltaTime;
        text.color = Color.HSVToRGB(timer / fullHueChangeDuration, 1, 1);
        timer %= fullHueChangeDuration;
    }
}
