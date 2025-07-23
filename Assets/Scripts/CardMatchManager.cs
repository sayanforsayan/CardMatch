using UnityEngine;
using System.Collections;

namespace Sayan.CardGame
{
    public class CardMatchManager : MonoBehaviour
    {
        public static CardMatchManager Instance;

        private Card firstCard;
        private Card secondCard;
        private bool isChecking = false;

        private void Awake() => Instance = this;


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
                firstCard.SetMatched();
                secondCard.SetMatched();
                GameManager.Instance.AddScore(1);
            }
            else
            {
                firstCard.FlipCard(false);
                secondCard.FlipCard(false);
            }

            firstCard = null;
            secondCard = null;
            isChecking = false;
        }
    }
}
