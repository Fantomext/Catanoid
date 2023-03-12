using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private int _level = 1;
    private int _maxLevel = 3;
    private string _playSceneName = "Level";

    private void Start()
    {
        Time.timeScale = 1;
        _level = PlayerPrefs.GetInt("lvl", 1);
    }

    public void PlayGame()
    {
        _level = 1;
        SceneManager.LoadScene($"{_playSceneName}{_level}");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene($"{_playSceneName}{_level}");
        Score.ScoreGame = 0;
        Score.TotalScoreGame = 0;
    }

    public void NextLevel()
    {
        _level++;

        if (_level > _maxLevel)
        {
            _level = 1;
        }

        Score.ScoreGame = 0;
        PlayerPrefs.SetInt("lvl", _level);

        SceneManager.LoadScene($"{_playSceneName}{_level}");
    }

    public void GoToMenu()
    {
        Score.ScoreGame = 0;
        Score.TotalScoreGame = 0;
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Start");
    }
}
