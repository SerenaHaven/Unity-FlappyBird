using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    private GameObject _start;
    private GameObject _buttons;
    private GameObject _tutorial;
    private GameObject _score;

    private Button _buttonRate;
    private Button _buttonPlay;
    private Button _buttonScore;
    private Button _buttonTutorial;
    private Text _textScore;

    private Game _game;
    private Balance _balance;
    private Flasher _flasher;
    private Switcher _switcher;

    void Awake()
    {
        _game = FindObjectOfType<Game>();
        _game.onGameOver = OnGameOver;
        _game.onScoreChange = OnScoreChange;

        _start = transform.Find("Start").gameObject;
        _buttons = transform.Find("Buttons").gameObject;
        _tutorial = transform.Find("Tutorial").gameObject;
        _score = transform.Find("Score").gameObject;

        _buttonPlay = _buttons.transform.Find("ButtonPlay").GetComponent<Button>();
        _buttonPlay.onClick.AddListener(OnButtonPlay);

        _buttonTutorial = _tutorial.transform.Find("ButtonTutorial").GetComponent<Button>();
        _buttonTutorial.onClick.AddListener(OnButtonTutorial);

        _textScore = _score.transform.Find("TextScore").GetComponent<Text>();

        _flasher = transform.Find("Flasher").GetComponent<Flasher>();
        _switcher = transform.Find("Switcher").GetComponent<Switcher>();
        _balance = transform.Find("Balance").GetComponent<Balance>();

        _start.SetActive(true);
        _buttons.SetActive(true);
        _tutorial.SetActive(false);
        _score.SetActive(false);

        _flasher.gameObject.SetActive(false);
        _switcher.gameObject.SetActive(false);
        _balance.gameObject.SetActive(false);
        _balance.onAnimationFinish = OnBalanceAnimationFinish;
    }

    private void OnBalanceAnimationFinish()
    {
        _buttons.SetActive(true);
    }

    private void OnScoreChange()
    {
        _textScore.text = _game.score.ToString();
    }

    private void OnGameOver()
    {
        _flasher.Flash();
        _balance.gameObject.SetActive(true);
        _balance.score = _game.score;
        _buttons.SetActive(false);
        _score.SetActive(false);
    }

    private void OnButtonPlay()
    {
        if (_game.gameState == GameState.Idle || _game.gameState == GameState.GameOver)
        {
            Audio.PlayOneShot(AudioEnum.Swooshing);
            _switcher.Switch(() =>
            {
                _game.GameReady();
                _start.SetActive(false);
                _buttons.SetActive(false);
                _tutorial.SetActive(true);
                _score.SetActive(true);
                _balance.gameObject.SetActive(false);
                _textScore.text = "0";
            });
        }
    }

    private void OnButtonTutorial()
    {
        if (_game.gameState == GameState.Ready)
        {
            _game.GameStart();
            _start.SetActive(false);
            _buttons.SetActive(false);
            _tutorial.SetActive(false);
            _score.SetActive(true);
            _balance.gameObject.SetActive(false);
            _textScore.text = "0";
        }
    }
}