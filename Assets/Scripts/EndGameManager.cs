using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class EndGameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject redPlayerWinsUI;
    [SerializeField]
    private GameObject bluePlayerWinsUI;
    [SerializeField]
    private GameObject timeUpUI;
    [SerializeField]
    private GameObject nextBattleUI;

    public void ShowEndGameUIWithWinner(PlayerNumber winner)
	{
        switch(winner)
        {
            case PlayerNumber.Player1: redPlayerWinsUI.SetActive(true); break;
            case PlayerNumber.Player2: bluePlayerWinsUI.SetActive(true); break;
            case PlayerNumber.None: timeUpUI.SetActive(true); break;
        }

        nextBattleUI.SetActive(true);
	}
}
