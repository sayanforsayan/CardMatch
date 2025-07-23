using UnityEngine;

namespace Sayan.CardGame
{
    public class Card : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer frontRenderer;
        [SerializeField] private SpriteRenderer backRenderer;
        [SerializeField] private BoxCollider2D coll;

        private Sprite cardImage;
        private bool isFlipped = false;
        private bool isMatched = false;

        public void SetCard(Sprite image)
        {
            cardImage = image;
            frontRenderer.sprite = image;
        }

        private void OnMouseDown()
        {
            if (!isFlipped && !isMatched)
            {
                FlipCard(true);
                CardMatchManager.Instance.OnCardFlipped(this);
            }
        }

        public void FlipCard(bool isActive)
        {
            isFlipped = isActive;
            frontRenderer.gameObject.SetActive(isActive);
            backRenderer.gameObject.SetActive(!isActive);
        }

        public void IsCollider(bool isEnabled)
        {
            if (coll != null)
                coll.enabled = isEnabled;
        }

        public Sprite GetCardImage() => cardImage;
        public void SetMatched() => isMatched = true;
    }

}
