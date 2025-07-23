using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

namespace Sayan.CardGame
{
    public class LevelEditorWindow : EditorWindow
    {
        private string levelName = "Level1";

        [MenuItem("Tools/Level Editor")]
        public static void ShowWindow()
        {
            GetWindow<LevelEditorWindow>("Level Editor");
        }

        void OnGUI()
        {
            GUILayout.Label("Export Card Positions to JSON", EditorStyles.boldLabel);

            levelName = EditorGUILayout.TextField("Level Name", levelName);

            if (GUILayout.Button("Save Level JSON"))
            {
                SaveLevel();
            }
        }

        void SaveLevel()
        {
            Card[] cards = FindObjectsOfType<Card>();
            if (cards.Length == 0)
            {
                Debug.LogError("No cards found in scene.");
                return;
            }

            LevelData data = new LevelData
            {
                levelName = levelName,
                positions = new List<PositionData>()
            };

            foreach (Card card in cards)
            {
                Vector2 pos = card.transform.position;
                data.positions.Add(new PositionData { x = pos.x, y = pos.y });
            }

            string json = JsonUtility.ToJson(data, true);
            string folderPath = "Assets/Resources/Levels";

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            string path = Path.Combine(folderPath, levelName + ".json");
            File.WriteAllText(path, json);
            AssetDatabase.Refresh();
            Debug.Log("Level saved: " + path);
        }
    }
}
