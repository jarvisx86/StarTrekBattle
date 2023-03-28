using System;
using System.Diagnostics;
using UnityEngine;
using Utility;

namespace Enemy
{
    public class EnemyCollisionHandler : MonoBehaviour
    {
        [SerializeField] private float shields;
        [SerializeField] private float boltDamage;
        [SerializeField] private ParticleSystem explosionPs;
        [SerializeField] private GameObject spriteObject;
        [SerializeField] private ParticleSystem engineTrail;
        
        private AudioSource _explosionSfx;
        public bool isExploding = false;
        private float _timer;

        private void Start()
        {
            _explosionSfx = GameObject.FindGameObjectWithTag("ShipExplosion").GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (shields > 0) return;
            
            _timer += Time.deltaTime;
            if (engineTrail.isPlaying)
            {
                engineTrail.Stop();
            }
            
            if (_timer < 3f) return;
            
            spriteObject.SetActive(false);

            if (_timer < 4f) return;
            
            Destroy(gameObject);
            isExploding = false;
            _timer = 0f;
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Bolt"))
            {
                shields -= boltDamage;
            }

            if (shields > 0 || isExploding) return;

            ProcessExplosion();
        }

        private void ProcessExplosion()
        {
            isExploding = true;
            GameData.PlayerScore += 20;

            if (!explosionPs.isPlaying)
            {
                explosionPs.Play();
            }

            _explosionSfx.Play();
        }
    }
}
