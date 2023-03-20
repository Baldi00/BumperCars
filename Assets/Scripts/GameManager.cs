using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
	public static GameManager Instance { get => _instance; }

    public PlayerMover player1;
    public PlayerMover player2;
    public new CameraMover camera;

	void Awake()
	{
		if (_instance == null)
			_instance = this;
		else
		{
			Destroy(gameObject);
			return;
		}
	}
}
