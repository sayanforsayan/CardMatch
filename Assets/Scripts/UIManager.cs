using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Sayan.CardGame
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TMP_Text turnText;
        [SerializeField] private Button nextButton;

        void Awake() => Instance = this;

        public void ShowScore(string sc)
        {
            if (scoreText != null)
                scoreText.text = "Score:" + sc;
        }

        public void ShowTurn(string turn)
        {
            if (turnText != null)
                turnText.text = "Turn:" + turn;
        }
    }
}
