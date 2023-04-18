using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public enum InputType
	{
		Keyboard,
		Gamepad,
		Touchscreen
	}

    private static GameManager _instance;
	public static GameManager Instance { get => _instance; }

    public PlayerMover player1;
    public PlayerMover player2;
    public new CameraMover camera;
	public GameObject onScreenControlsUI;
	public EndGameManager endGameManager;
	public InputType inputType;

	public bool IsPaused;

	void Awake()
	{
		if (_instance == null)
			_instance = this;
		else
		{
			Destroy(gameObject);
			return;
		}

		if (Touchscreen.current != null)
		{
			inputType = InputType.Touchscreen;
			onScreenControlsUI.SetActive(true);
		}
		else if (Keyboard.current != null)
			inputType = InputType.Keyboard;
		else
			inputType = InputType.Gamepad;

		Application.targetFrameRate = 60;
	}

	public void LoadGameScene()
	{
		SceneManager.LoadScene("GameScene");
	}
}
