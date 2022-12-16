using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public static class DataManager
{
    public static PersistenceData Data { get; } = new();
    
    [Serializable]
    public class PersistenceData
    {
        [SerializeField] private string lastPlayerName;
        public string LastPlayerName
        {
            get => lastPlayerName;
            set
            {
                lastPlayerName = value;
                Save();
            }
        }

        [SerializeField] private List<Score> highScores = new();
        public Score BestScore => highScores.OrderByDescending(x => x.Value).FirstOrDefault();

        public PersistenceData()
        {
            Load();
        }

        public void AddScore(string playerName, int value)
        {
            highScores.Add(new Score(playerName, value));
            Save();
        }

        private void Save()
        {
            var json = JsonUtility.ToJson(this);
            File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        }

        private void Load()
        {
            var path = Application.persistentDataPath + "/savefile.json";

            if (File.Exists(path))
            {
                var json = File.ReadAllText(path);
                JsonUtility.FromJsonOverwrite(json, this);
            }
        }

        [Serializable]
        public class Score
        {
            public Score(string playerName, int value)
            {
                this.playerName = playerName;
                this.value = value;
            }

            [SerializeField] private string playerName;
            public string PlayerName
            {
                get => playerName;
                private set => playerName = value;
            }

            [SerializeField] private int value;
            public int Value
            {
                get => value;
                private set => this.value = value;
            }
        }
    }
}