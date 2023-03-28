using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class AsteroidSpawn : MonoBehaviour
    {
        [SerializeField] private List<GameObject> asteroids;
        [SerializeField] private float spawnDelay;
        [SerializeField] private float repeatRate;

        [SerializeField] private float asteroidMinSpeed;
        [SerializeField] private float asteroidMaxSpeed;

        private float _asteroidSpeed;
        private Bounds _bounds;

        // Start is called before the first frame update
        private void Start()
        {
            _bounds = GameObject.FindGameObjectWithTag("Boundary").GetComponent<Collider2D>().bounds;
            Invoke(nameof(SpawnInitialAsteroids), spawnDelay);
            InvokeRepeating(nameof(SpawnNewAsteroids), spawnDelay * 2, repeatRate);
        }

        private void SpawnInitialAsteroids()
        {
            var initialCount = Random.Range(1, 10);

            for (var i = 0; i < initialCount; i++)
            {
                SpawnNewAsteroids();
            }
        }
        
        private void SpawnNewAsteroids()
        {
            var asteroidType = Math.Floor((double)Random.Range(1, 3));
            var newAsteroid = asteroidType switch
            {
                (1) => // large asteroid
                    Instantiate(asteroids[0], transform),
                (2) => // medium asteroid
                    Instantiate(asteroids[1], transform),
                (3) => // small asteroid
                    Instantiate(asteroids[2], transform),
                _ => // default to large asteroid 
                    Instantiate(asteroids[0], transform)
            };

            newAsteroid.transform.position = MovementHelper.GeneratePosition(_bounds);
            GenerateVelocity(newAsteroid);
        }

        private void GenerateVelocity(GameObject newAsteroid)
        {
            var rb2d = newAsteroid.GetComponent<Rigidbody2D>();
            var position = newAsteroid.transform.position;
            
            var velocityX = position.x > 0 ? -1 : 1;
            var velocityY = position.y > 0 ? -1 : 1;

            _asteroidSpeed = Random.Range(asteroidMinSpeed, asteroidMaxSpeed);

            var adjustedVelocityX = velocityX * _asteroidSpeed * Time.deltaTime;
            var adjustedVelocityY = velocityY * _asteroidSpeed * Time.deltaTime;
            
            rb2d.AddForce(new Vector2(adjustedVelocityX, adjustedVelocityY), ForceMode2D.Impulse);
            
        }

    }
}
