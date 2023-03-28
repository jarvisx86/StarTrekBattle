using UnityEngine;
using TMPro;

namespace Player
{
    public class PhaserBolt : MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreText;
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Enterprise")) return;
            Destroy(gameObject);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            Destroy(gameObject);
        }
    }
}
