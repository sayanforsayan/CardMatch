using UnityEngine;
using DG.Tweening;

namespace Sayan.CardGame
{
    /// <summary>
    /// Card Properties are handling 
    /// </summary>
    public class Card : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer frontRenderer;
        [SerializeField] private SpriteRenderer backRenderer;
        [SerializeField] private BoxCollider2D coll;

        private Sprite cardImage;
        private bool isFlipped = false;
        private bool isMatched = false;

        private void OnMouseDown()
        {
            if (!isFlipped && !isMatched)
            {
                FlipCard(true);
                SoundManager.Instance.PlaySound(SoundType.Flip);
                GameManager.Instance.OnCardFlipped(this);
            }
        }

        public Sprite GetCardImage() => cardImage;
        public void SetMatched() => isMatched = true;

        public void SetCard(Sprite image)
        {
            cardImage = image;
            frontRenderer.sprite = image;
        }

        public void FlipCard(bool isActive)
        {
            IsCollider(false); // Disable collider during flip
            isFlipped = isActive;

            // Animate to 90° Y
            transform.DORotate(new Vector3(0, 90, 0), 0.15f).OnComplete(() =>
            {
                // Switch visuals at half-flip
                frontRenderer.gameObject.SetActive(isActive);
                backRenderer.gameObject.SetActive(!isActive);

                // Animate back to 0° from 270°
                transform.DORotate(new Vector3(0, 0, 0), 0.15f).From(new Vector3(0, 270, 0)).OnComplete(() =>
                {
                    IsCollider(true); // Re-enable collider after flip
                });
            });
        }

        public void IsCollider(bool isEnabled)
        {
            if (coll != null)
                coll.enabled = isEnabled;
        }
    }
}
