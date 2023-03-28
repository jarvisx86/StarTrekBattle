using Pathfinding;
using UnityEngine;

namespace Enemy
{
    public class EnemyPathFinder : MonoBehaviour
    {
        [Header("Path Tuning")]
        public float speed = 200f;
        public float rotationSpeed = 10f;
        public float nextWaypointDistance = 3f;
        public float finalDistanceFromTarget = 1f;

        [Header("FX")] public ParticleSystem enginePs;
    
        public Transform TargetTransform { get; set; }
        
        private Path _path;
        private AIPath _pathData;
        private int _currentWaypoint;
        // private bool _reachedEndOfPath = false;
        private Seeker _seeker;
        private Rigidbody2D _rb2d;
        private Vector3 _movementDirection;
        private EnemyCollisionHandler _collisionHandler;
    
        // Start is called before the first frame update
        private void Start()
        {
            _seeker = GetComponent<Seeker>();
            _rb2d = GetComponent<Rigidbody2D>();
            _collisionHandler = GetComponent<EnemyCollisionHandler>();

            InvokeRepeating(nameof(UpdatePath), 0f, 0.5f);
        }

        private void UpdatePath()
        {
            if (_seeker.IsDone())
                _seeker.StartPath(_rb2d.position, TargetTransform.position, OnPathComplete);
        }

        private void OnPathComplete(Path p)
        {
            if (p.error) return;

            _path = p;
            _currentWaypoint = 0;
        }
    
        private void FixedUpdate()
        {
            if (_collisionHandler.isExploding) return;
            
            if (_path is null || _currentWaypoint >= _path.vectorPath.Count)
            {
                enginePs.Stop();
                return;
            }
        
            ProcessMovement();
        }
    
        private void ProcessMovement()
        {
            var position = _rb2d.position;
            var targetPosition = TargetTransform.position;
            Vector2 facingDirection;
        
            // check to see if we have reached the end of our current path
            var minDistance = Vector3.Distance(position, targetPosition);
            if (minDistance <= finalDistanceFromTarget)
            {
                // rotation applied to our enemy to make it face the travel direction
                facingDirection = ((Vector2)targetPosition - position).normalized;
                ProcessRotation(facingDirection);
                enginePs.Stop();
            }
            else 
            {
                facingDirection = ((Vector2)_path.vectorPath[_currentWaypoint] - position).normalized;
                ProcessRotation(facingDirection);

                // force applied to our enemy to make it move
                var force = facingDirection * (speed * Time.deltaTime);
                _rb2d.AddForce(force);
        
                if (!enginePs.isPlaying) enginePs.Play();
        
                var distanceFromWayPoint = Vector2.Distance(position, _path.vectorPath[_currentWaypoint]);
                if (distanceFromWayPoint < nextWaypointDistance) _currentWaypoint++;
            }
        }

        private void ProcessRotation(Vector2 direction)
        {
            var rotation = Quaternion.LookRotation(Vector3.forward, direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }
    }
}
