using UnityEngine;
using UnityEngine.SceneManagement;
using Utility;

namespace Environment
{
    public class GameManager : MonoBehaviour
    {
        private const int MainMenuIndex = 0;
        private const int InGameIndex = 1;
        
        private GameObject _inGameUI;
        private GameObject _gameOverUI;
        private GameObject _pauseMenuUI;
        private GameObject _player;
        private Scene _currentScene;
        // Start is called before the first frame update
        private void Start()
        {
            _currentScene = SceneManager.GetActiveScene();
            if (_currentScene.buildIndex != InGameIndex) return;
            
            _inGameUI = GameObject.FindGameObjectWithTag("GameUI");
            _gameOverUI = GameObject.FindGameObjectWithTag("GameOverUI");
            _pauseMenuUI = GameObject.FindGameObjectWithTag("PauseMenuUI");
            _pauseMenuUI.SetActive(false);
            _gameOverUI.SetActive(false);
            _player =  GameObject.FindGameObjectWithTag("Enterprise");
        }

        // Update is called once per frame
        private void Update()
        {
            if (_currentScene.buildIndex == MainMenuIndex) return;
            
            if (GameData.PlayerLives == 0)
            {
                Invoke(nameof(GameOverMenu), 3f);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePauseMenu();
            }
        }

        private void GameOverMenu()
        {
            _inGameUI.SetActive(false);
            _player.SetActive(false);
            _gameOverUI.SetActive(true);
            Time.timeScale = 0f;
        }

        private void TogglePauseMenu()
        {
            _pauseMenuUI.SetActive(!_pauseMenuUI.activeSelf);
            Time.timeScale = _pauseMenuUI.activeSelf ? 0f : 1f;
        }
        
        public void PlayGame()
        {
            GameData.PlayerLives = 3;
            GameData.PlayerScore = 0;
            GameData.PlayerHasDied = false;
            SceneManager.LoadScene(InGameIndex, LoadSceneMode.Single);
        }
        
        public void ReturnToMainMenu()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(MainMenuIndex, LoadSceneMode.Single);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
