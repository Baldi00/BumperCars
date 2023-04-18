using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[DisallowMultipleComponent]
public class TimerUpdater : MonoBehaviour
{
	[SerializeField]
	private int startTime;
    [SerializeField]
    private UnityEvent<int> onTimerChanges;
    [SerializeField]
    private UnityEvent<PlayerNumber> onTimerEnds;

    private float currentTime;
	private int currentTimeInt;

	void Awake()
	{
		currentTime = startTime;
		currentTimeInt = startTime;
	}

	void Update()
	{
		if (!GameManager.Instance.IsPaused && currentTime > 0)
		{
			currentTime -= Time.unscaledDeltaTime;
			if(currentTimeInt != (int)currentTime)
			{
				currentTimeInt = (int)currentTime;
				onTimerChanges?.Invoke(currentTimeInt);
			}

			if (currentTimeInt == 0)
			{
				GameManager.Instance.IsPaused = true;
				onTimerEnds?.Invoke(PlayerNumber.None);
			}
		}
	}

}
