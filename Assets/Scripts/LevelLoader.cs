using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Sayan.CardGame
{
    public class LevelLoader : MonoBehaviour
    {
        public TextAsset[] allLevels;

        private void Start()
        {
            CallLevel();
        }

        public void CallLevel()
        {
            LoadLevel(allLevels[GameManager.Instance.Level]);
        }
        // Load Level from json file
        void LoadLevel(TextAsset jsonFile)
        {
            if (jsonFile == null)
            {
                Debug.LogError("JSON not found: " + name);
                return;
            }

            LevelData data = JsonUtility.FromJson<LevelData>(jsonFile.text);
            List<Sprite> images = GetShuffledCardImages(data.positions.Count);

            for (int i = 0; i < data.positions.Count; i++)
            {
                Vector2 pos = new Vector2(data.positions[i].x, data.positions[i].y);
                GameObject cardObj = Instantiate(GameManager.Instance.cardPrefab, pos, Quaternion.identity, GameManager.Instance.cardParent);
                Card card = cardObj.GetComponent<Card>();
                card.SetCard(images[i]);
            }

            GameManager.Instance.TotalCard = images.Count / 2;
        }

        // Shuffle Images and assign
        List<Sprite> GetShuffledCardImages(int count)
        {
            // Shuffle Images
            GameManager.Instance.cardImages = GameManager.Instance.cardImages.OrderBy(x => System.Guid.NewGuid()).ToList();
            List<Sprite> pool = new List<Sprite>();
            int pairCount = count / 2;

            for (int i = 0; i < pairCount; i++)
            {
                Sprite s = GameManager.Instance.cardImages[i];
                pool.Add(s);
                pool.Add(s);
            }

            for (int i = 0; i < pool.Count; i++)
            {
                int rand = Random.Range(i, pool.Count);
                (pool[i], pool[rand]) = (pool[rand], pool[i]);
            }

            return pool;
        }
    }

}
