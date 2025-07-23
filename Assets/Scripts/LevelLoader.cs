using UnityEngine;
using System.Collections.Generic;

namespace Sayan.CardGame
{
    public class LevelLoader : MonoBehaviour
    {
        public string levelName = "Level1";

        private void Start()
        {
            LoadLevel(levelName);
        }

        void LoadLevel(string name)
        {
            TextAsset jsonFile = Resources.Load<TextAsset>($"Levels/{name}");
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
        }

        List<Sprite> GetShuffledCardImages(int count)
        {
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
