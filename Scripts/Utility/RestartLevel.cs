using System.Collections;
using Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utility
{
    public class RestartLevel : ICoroutine 
    {
        public IEnumerator Coroutine { get; set; }

        IEnumerator ICoroutine.RestartLevel(float delay)
        {
            yield return new WaitForSeconds(delay);

            var currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name, LoadSceneMode.Single);
            GameData.PlayerHasDied = false;
        }

        public IEnumerator GetNextLevel(float delay)
        {
            yield return new WaitForSeconds(delay);

            var currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.buildIndex + 1, LoadSceneMode.Single);
        }
    }
}