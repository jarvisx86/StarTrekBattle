using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace UI
{
    public class UIData : MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private Image playerLives;

        private const float FillAmountMultiplier = 0.334f;

        // Start is called before the first frame update
        private void Start()
        {
            scoreText.text = GameData.PlayerScore.ToString();
            playerLives.fillAmount = GameData.PlayerLives * FillAmountMultiplier;

        }

        // Update is called once per frame
        private void Update()
        {
            scoreText.text = GameData.PlayerScore.ToString();
            playerLives.fillAmount = GameData.PlayerLives * FillAmountMultiplier;
        }
    }
}
