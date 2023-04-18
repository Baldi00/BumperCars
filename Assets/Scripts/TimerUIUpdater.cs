using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[DisallowMultipleComponent]
public class TimerUIUpdater : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI timerUI;

	public void UpdateTimerUI(int currentTimer)
	{
		timerUI.text = currentTimer.ToString("00");
	}
}
