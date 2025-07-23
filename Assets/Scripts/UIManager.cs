using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Sayan.CardGame
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;
        [SerializeField] private GameObject gameOver;
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TMP_Text turnText;
        [SerializeField] private TMP_Text levelText;
        [SerializeField] private Button nextButton, resetButton;

        void Awake() => Instance = this;

        void Start()
        {
            gameOver.SetActive(false);
            nextButton.onClick.AddListener(ButtonPress);
            resetButton.onClick.AddListener(ResetGame);
            NextButtonActivation(false);
            ShowLevel(GameManager.Instance.Level + 1);
        }

        private void ButtonPress()
        {
            SoundManager.Instance.PlaySound(SoundType.Click);
            NextButtonActivation(false);
            GameManager.Instance.UpdateLevel();
            ShowScore(0);
            ShowTurn(0);
            ShowLevel(GameManager.Instance.Level + 1);
        }

        private void ResetGame()
        {
            SoundManager.Instance.PlaySound(SoundType.Click);
            ShowScore(0);
            ShowTurn(0);
            ShowLevel(GameManager.Instance.Level + 1);
            gameOver.SetActive(false);
            GameManager.Instance.UpdateLevel();
        }

        public void ShowScore(int sc)
        {
            if (scoreText != null)
                scoreText.text = "Score:" + sc;
        }

        public void ShowTurn(int turn)
        {
            if (turnText != null)
                turnText.text = "Turn:" + turn;
        }

        public void ShowLevel(int level)
        {
            if (levelText != null)
                levelText.text = "Level " + level;
        }

        public void NextButtonActivation(bool isActive)
        {
            if (nextButton != null)
                nextButton.gameObject.SetActive(isActive);
        }

        public void GameOver() => gameOver.SetActive(true);
    }
}
