using UnityEngine;
using System.Collections.Generic;

namespace Sayan.CardGame
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public List<Sprite> cardImages;
        public GameObject cardPrefab;
        public Transform cardParent;
        public int score = 0;

        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(gameObject);
            else
                Instance = this;
        }

        public void AddScore(int amount)
        {
            score += amount;
            Debug.Log("Score: " + score);
        }
    }
}
