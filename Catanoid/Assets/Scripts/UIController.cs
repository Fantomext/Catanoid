using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private AudioSource _winSound;
    [SerializeField] private AudioSource _loseSound;
    [SerializeField] private AudioSource _pauseSound;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _nextLevelMenu;
    [SerializeField] private GameObject _restartmenu;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _totalScoreText;

    private void OnEnable()
    {
        BrickController.AllBricksBroken += NextLevel;
        BallMove.BallBehindMap += Restart;
        Brick.OnBrickBroken += UpdateScore;
    }

    private void OnDisable()
    {
        BrickController.AllBricksBroken -= NextLevel;
        BallMove.BallBehindMap -= Restart;
        Brick.OnBrickBroken -= UpdateScore;
    }

    private void Start()
    {
        ShowText();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ResumeGame();
        }
    }

    private void NextLevel()
    {
        _winSound.Play();
        Time.timeScale = 0;
        _nextLevelMenu.SetActive(true);
    }

    private void Restart()
    {
        _loseSound.Play();
        Time.timeScale = 0;
        _restartmenu.SetActive(true);
    }

    private void UpdateScore(Brick b, int score)
    {
        Score.ScoreGame += score;
        Score.TotalScoreGame += score;
        ShowText();
    }

    private void ShowText()
    {
        _scoreText.text = $"{Score.ScoreGame}";
        _totalScoreText.text = $"Total score: {Score.TotalScoreGame}";
    }

    public void ResumeGame()
    {
        _pauseMenu.SetActive(!_pauseMenu.activeSelf);

        if (_pauseMenu.activeSelf)
        {
            _pauseSound.Play();
        }

        Time.timeScale = _pauseMenu.activeSelf ? 0 : 1;
    }
}
