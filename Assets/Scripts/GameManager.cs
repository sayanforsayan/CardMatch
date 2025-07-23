using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Sayan.CardGame
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public List<Sprite> cardImages;
        public GameObject cardPrefab;
        public Transform cardParent;
        public int Level { get; set; } = 0;
        public int TotalCard { get; set; }

        private Card firstCard, secondCard;
        private LevelLoader levelLoader;
        private bool isChecking = false;
        private int score = 0;
        private int turn = 0;

        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(gameObject);
            else
                Instance = this;
        }
        void Start()
        {
            levelLoader = GetComponent<LevelLoader>();
        }

        public void OnCardFlipped(Card card)
        {
            if (isChecking) return;

            if (firstCard == null)
                firstCard = card;
            else if (secondCard == null)
            {
                secondCard = card;
                StartCoroutine(CheckMatch());
            }
        }

        IEnumerator CheckMatch()
        {
            isChecking = true;
            yield return new WaitForSeconds(0.5f);

            if (firstCard.GetCardImage() == secondCard.GetCardImage())
            {
                score++;
                TotalCard--;
                firstCard.SetMatched();
                secondCard.SetMatched();
                UIManager.Instance.ShowScore(score);
                Destroy(firstCard.gameObject);
                Destroy(secondCard.gameObject);
                if (TotalCard == 0)
                {
                    Level++;
                    score = 0;
                    turn = 0;
                    if (levelLoader.allLevels.Length == Level)
                    {
                        UIManager.Instance.GameOver();
                        Debug.Log("GameOver");
                    }
                    else
                        UIManager.Instance.NextButtonActivation(true);
                }
            }
            else
            {
                firstCard.FlipCard(false);
                secondCard.FlipCard(false);
                turn++;
                UIManager.Instance.ShowTurn(turn);
            }

            firstCard = null;
            secondCard = null;
            isChecking = false;
        }

        public void UpdateLevel(bool isReset = false)
        {
            if (isReset)
                Level = 0;
            levelLoader.CallLevel();
        }
    }
}
