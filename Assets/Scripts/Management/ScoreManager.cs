using UnityEngine.SceneManagement;
using TMPro;
using UI;
using UnityEngine;

namespace Management
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] private int reducePoints = 10;
        [SerializeField] private int redBallPoints = 5;
        [SerializeField] private int greenBallPoints = 10;
        [SerializeField] private int blueBallPoints = 15;
        [SerializeField] private TextMeshProUGUI scoreText;
        public static ScoreManager Instance;

        public int Score { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                SceneManager.sceneLoaded += OnSceneLoaded;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            var textObj = GameObject.FindWithTag("ScoreText");
            if (textObj != null)
            {
                scoreText = textObj.GetComponent<TextMeshProUGUI>();
                UpdateScoreText();
            }
            if (SceneManager.GetActiveScene().name == "Game") ResetScore();
        }

        public void AddScore(Color color)
        {
            var points = color == Color.red ? redBallPoints
                : color == Color.green ? greenBallPoints
                : color == Color.blue ? blueBallPoints : reducePoints;

            Score += points;
            scoreText.text = Score.ToString();
        }

        public void ReduceScore()
        {
            Score -= reducePoints;
            scoreText.text = Score.ToString();
        }

        public void ResetScore()
        {
            Score = 0;
            scoreText.text = Score.ToString();
        }

        private void UpdateScoreText()
        {
            if (scoreText != null)
                scoreText.text = Score.ToString();
        }
    }
}