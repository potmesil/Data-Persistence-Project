using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Main
{
    public class MainManager : MonoBehaviour
    {
        [field: SerializeField] private Brick BrickPrefab { get; set; }
        [field: SerializeField] private int LineCount { get; set; } = 6;
        [field: SerializeField] private Rigidbody BallRb { get; set; }
        [field: SerializeField] private Text BestScoreText { get; set; }
        [field: SerializeField] private Text ScoreText { get; set; }
        [field: SerializeField] private GameObject GameOverText { get; set; }

        private bool Started { get; set; }
        private int Points { get; set; }

        private void Start()
        {
            SetBestScoreText();

            const float step = 0.6f;
            var perLine = Mathf.FloorToInt(4.0f / step);
            var pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };

            for (var i = 0; i < LineCount; ++i)
            {
                for (var x = 0; x < perLine; ++x)
                {
                    var position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                    var brick = Instantiate(BrickPrefab, position, BrickPrefab.transform.rotation);
                    brick.OnDestroyed += AddPoint;
                    brick.PointValue = pointCountArray[i];
                }
            }
        }

        private void Update()
        {
            if (!Started)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Started = true;
                    var randomDirection = Random.Range(-1.0f, 1.0f);
                    var forceDir = new Vector3(randomDirection, 1, 0);
                    forceDir.Normalize();

                    BallRb.transform.SetParent(null);
                    BallRb.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
                }
            }
            else if (GameOverText.activeInHierarchy)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }
        }

        public void GameOver()
        {
            GameOverText.SetActive(true);
            DataManager.Data.AddScore(DataManager.Data.LastPlayerName, Points);
            SetBestScoreText();
        }

        private void AddPoint(int point)
        {
            Points += point;
            ScoreText.text = $"Score : {Points}";
        }

        private void SetBestScoreText()
        {
            var bestScore = DataManager.Data.BestScore;
            if (!string.IsNullOrWhiteSpace(bestScore?.PlayerName))
            {
                BestScoreText.text = $"Best Score : {bestScore.PlayerName} : {bestScore.Value}";
            }
        }
    }
}