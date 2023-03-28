using UnityEngine;
using Utility;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class AsteroidCollisionHandler : MonoBehaviour
    {
        private const string MediumAsteroidPath = "Prefabs/MediumAsteroid";
        private const string SmallAsteroidPath = "Prefabs/SmallAsteroid";

        private AudioSource _explosionSfx;
        private GameObject _asteroid;
        private Transform _parent;

        private void Start()
        {
            _parent = GameObject.FindGameObjectWithTag("AsteroidParent").transform;
            _explosionSfx = GameObject.FindGameObjectWithTag("AsteroidExplosion").GetComponent<AudioSource>();
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (!col.gameObject.CompareTag("Bolt")) return;

            GameData.PlayerScore += 5;

            if (gameObject.CompareTag("SmallAsteroid"))
            {
                ProcessSmallAsteroidCollision();
                return;
            }

            _explosionSfx.Play();
            GetAsteroidPrefab();
            GenerateAsteroids();
        }

        private void ProcessSmallAsteroidCollision()
        {
            _explosionSfx.Play();
            PlayExplosionPS(GetComponent<ParticleSystem>());
            GetComponentInChildren<SpriteRenderer>().enabled = false;
            Invoke(nameof(DestroySmallAsteroid), 0.15f);
        }

        private void GenerateAsteroids()
        {
            var position = transform.position;
            // var direction = 1.5f;
            
            var rb2d = GetComponent<Rigidbody2D>();
            var velocity = rb2d.velocity;
            var currentXVelocity = velocity.x;
            var currentYVelocity = velocity.y;
            
            // if position.x > 0 then -x velocity otherwise +x velocity
            // if position.y > 0 then -y velocity otherwise +y velocity

            for (var i = 0; i <= 1; i++)
            {
                var newAsteroid = Instantiate(_asteroid, _parent);
                var asteroidRb2d = newAsteroid.GetComponent<Rigidbody2D>();
                var asteroidPs = newAsteroid.GetComponent<ParticleSystem>();
                var asteroidSize = newAsteroid.GetComponentInChildren<SpriteRenderer>().bounds.size;

                var positionOffsetX = asteroidSize.x ;
                
                newAsteroid.transform.position = i > 0 ? 
                    new Vector3((position.x - (positionOffsetX * 0.4f)), position.y, 0f) : 
                    new Vector3((position.x + (positionOffsetX * 0.4f)), position.y, 0f);
                

                asteroidRb2d.AddForce(
                    i == 1 ? new Vector2(Random.Range(-2f, 2f), currentYVelocity * 1.5f) : 
                        new Vector2(currentXVelocity * 1.5f, Random.Range(-2f, 2f)), ForceMode2D.Impulse);

                PlayExplosionPS(asteroidPs);
            }
            
            Destroy(gameObject);
        }
        
        private void GetAsteroidPrefab()
        {
            _asteroid = transform.tag switch
            {
                "LargeAsteroid" => Resources.Load<GameObject>(MediumAsteroidPath),
                _ => Resources.Load<GameObject>(SmallAsteroidPath)
            };
        }

        private void PlayExplosionPS(ParticleSystem ps)
        {
            if (!ps.isPlaying) ps.Play();
        }

        private void DestroySmallAsteroid()
        {
            Destroy(gameObject);
        }
    }
}
