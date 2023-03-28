using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace Scores
{
    public class SaveLoadScores : MonoBehaviour
    {
        private readonly string _path = Path.Combine(Application.persistentDataPath, "scores.json");
        private const string ScoreObjectTag = "Score";
        private const string ScoreContainerTab = "ScoreContainer";
        private List<GameObject> _scores;

        private void Awake()
        {
            var highScoresString = File.ReadAllText(_path);
            
        }

        // Start is called before the first frame update
        private void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
