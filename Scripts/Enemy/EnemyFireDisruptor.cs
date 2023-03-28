using UnityEngine;

namespace Enemy
{
    public class EnemyFireDisruptor : MonoBehaviour
    {
        public Transform rightBlaster;
        public Transform leftBlaster;
        public DisruptorBolt bolt;
        public float firingDistance = 4f;
        public float fireSpeed = 2f;
        public float firingCooldown = 3f;

        private AudioSource _disruptorSfx;

        private EnemyPathFinder _enemyPathFinder;
        private Rigidbody2D _rb2d;
        private float _timer;
        private float _timeBetweenRightAndLeft;
        private EnemyCollisionHandler _collisionHandler;
    
        // private bool _firedRight = false;
        private int _fireCount = 0;

        // Start is called before the first frame update
        private void Start()
        {
            _enemyPathFinder = GetComponent<EnemyPathFinder>();
            _rb2d = GetComponent<Rigidbody2D>();
            _collisionHandler = GetComponent<EnemyCollisionHandler>();
            _disruptorSfx = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        private void Update()
        {
            if (_collisionHandler.isExploding) return;
        
            var distanceFromTarget = Vector3.Distance(_rb2d.position, _enemyPathFinder.TargetTransform.position);
            if (distanceFromTarget > firingDistance) return;
        
            _timer += Time.deltaTime;

            if (!(_timer >= firingCooldown)) return;
        
            _timeBetweenRightAndLeft += Time.deltaTime;
            if (_timeBetweenRightAndLeft > 0.2f && _fireCount < 6)
            {
                FireLeftDisruptor();
                FireRightDisruptor();
                _timeBetweenRightAndLeft = 0f;
                _fireCount++;
                _disruptorSfx.Play();
            }
        
            if (_fireCount >= 5)
            {
                _fireCount = 0;
                _timeBetweenRightAndLeft = 0f;
                _timer = 0f;
            }
        
        }

        private void FireRightDisruptor()
        {
            var newLeftBolt = Instantiate(bolt, rightBlaster.position, rightBlaster.rotation);
            newLeftBolt.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0f, fireSpeed * Time.fixedDeltaTime));
            // _firedRight = true;
        }

        private void FireLeftDisruptor()
        {
            var newLeftBolt = Instantiate(bolt, leftBlaster.position, leftBlaster.rotation);
            newLeftBolt.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0f, fireSpeed * Time.fixedDeltaTime));
            // _firedRight = false;
        }
    }
}
