using System;
using System.Runtime.InteropServices.ComTypes;
using Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utility;

namespace Player
{
    public class ShipCollisionHandler : MonoBehaviour
    {
        [Header("UI Sprites")]
        [SerializeField] private Image shieldBar;
        [SerializeField] private GameObject shipSprite;
        
        [Header("Damage Tuning")]
        [SerializeField] [Range(0,0.5f)] private float disruptorDamage;
        [SerializeField] [Range(0,0.5f)] private float asteroidDamage;
        [SerializeField] [Range(0,0.5f)] private float obstacleDamage;
        
        [Header("Other Tuning")]
        [SerializeField] private ParticleSystem explosionPs;
        [SerializeField] private float finishDelay;
        
        private float _timer = 0f;
        private readonly RestartLevel _restartLevel = new ();
        private AudioSource _asteroidCollisionSfx;
        private AudioSource _shipExplosionSfx;

        private void Start()
        {
            _asteroidCollisionSfx = GameObject.FindGameObjectWithTag("AsteroidCollision").GetComponent<AudioSource>();
            _shipExplosionSfx = GameObject.FindGameObjectWithTag("ShipExplosion").GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (shieldBar.fillAmount > 0f) return;
            
            _timer += Time.deltaTime;   
            if (_timer < 2f) return;
            
            shipSprite.SetActive(false);
            _timer = 0f;
        }
        
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (GameData.PlayerHasDied) return;
            
            switch (col.gameObject.tag)
            {
                case "Disruptor":
                    ReduceShields(disruptorDamage);
                    break;  
                case "LargeAsteroid":
                    _asteroidCollisionSfx.Play();
                    ReduceShields(asteroidDamage);
                    break;
                case "MediumAsteroid":
                    _asteroidCollisionSfx.Play();
                    ReduceShields(asteroidDamage * 0.5f);
                    break;
                case "SmallAsteroid":
                    _asteroidCollisionSfx.Play();
                    ReduceShields(asteroidDamage * 0.25f);
                    break;
                case "Obstacle":
                    _asteroidCollisionSfx.Play();
                    ReduceShields(obstacleDamage);
                    break;
            }
            
            if (shieldBar.fillAmount > 0f) return;
            
            ProcessExplosion();
            RestartLevel();
        }

        private void ReduceShields(float amount)
        {
            shieldBar.fillAmount -= amount;
        }

        private void ProcessExplosion()
        {
            GameData.PlayerHasDied = true;
            GameData.PlayerLives -= 1;
            
            if (!explosionPs.isPlaying)
            {
                explosionPs.Play();
            }
            
            _shipExplosionSfx.Play();
        }
        
        private void RestartLevel()
        {
            _restartLevel.Coroutine = ((ICoroutine)_restartLevel).RestartLevel(finishDelay);
            StartCoroutine(_restartLevel.Coroutine);
        }
    }
}
