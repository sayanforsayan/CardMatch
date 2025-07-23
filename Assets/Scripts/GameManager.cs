using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Sayan.CardGame
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public List<Sprite> cardImages; // take all sprites
        public GameObject cardPrefab; // card prefab
        public Transform cardParent; // Card holder
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

        // Crad flip when card press
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

        // Check card matching
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
                SoundManager.Instance.PlaySound(SoundType.Match);
                UIManager.Instance.ShowScore(score);
                Destroy(firstCard.gameObject);
                Destroy(secondCard.gameObject);
                CheckLevel();
            }
            else
            {
                firstCard.FlipCard(false);
                secondCard.FlipCard(false);
                turn++;
                SoundManager.Instance.PlaySound(SoundType.Wrong);
                UIManager.Instance.ShowTurn(turn);
            }

            firstCard = null;
            secondCard = null;
            isChecking = false;
        }


        // Check level completion
        void CheckLevel()
        {
            if (TotalCard == 0)
            {
                Level++;
                score = 0;
                turn = 0;
                if (levelLoader.allLevels.Length == Level)
                {
                    SoundManager.Instance.PlaySound(SoundType.GameOver);
                    UIManager.Instance.GameOver();
                    Level = 0;
                    Debug.Log("GameOver");
                }
                else
                    UIManager.Instance.NextButtonActivation(true);
            }
        }

        // Load Level from LevelLoader
        public void UpdateLevel()
        {
            levelLoader.CallLevel();
        }
    }
}
