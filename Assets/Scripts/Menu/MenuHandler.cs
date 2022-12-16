using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Menu
{
    public class MenuHandler : MonoBehaviour
    {
        [field: SerializeField] private Text BestScoreText { get; set; }
        [field: SerializeField] private InputField NameInputField { get; set; }

        private void Start()
        {
            var bestScore = DataManager.Data.BestScore;
            if (!string.IsNullOrWhiteSpace(bestScore?.PlayerName))
            {
                BestScoreText.text = $"Best Score : {bestScore.PlayerName} : {bestScore.Value}";
            }

            NameInputField.text = DataManager.Data.LastPlayerName;
        }

        public void StartGame()
        {
            if (!string.IsNullOrWhiteSpace(NameInputField.text))
            {
                DataManager.Data.LastPlayerName = NameInputField.text;
                SceneManager.LoadScene("Scenes/main");
            }
            else
            {
                // alert
            }
        }

        public void QuitGame()
        {
            #if UNITY_EDITOR
                EditorApplication.ExitPlaymode();
            #else
                Application.Quit();
            #endif
        }
    }
}