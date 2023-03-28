using UnityEngine;
using UnityEngine.EventSystems;
using Utility;

namespace Player
{
    public class ShipMovement : MonoBehaviour
    {
        [SerializeField] private float shipSpeed = 0f;
        [SerializeField] private float shipRotation = 0f;
        
        private GameObject[] _engineTrails;
        private Rigidbody2D _shipRb2d;
        private ShipCollisionHandler _collisionHandler;
        private Collider2D _boundary;

        private bool _enableForceQuit = false; // for debug only
    
        // Start is called before the first frame update
        private void Start()
        {
            _boundary = GameObject.FindGameObjectWithTag("Boundary").GetComponent<Collider2D>();
            _shipRb2d = GetComponent<Rigidbody2D>();
            _engineTrails = GameObject.FindGameObjectsWithTag("Engine");
            _collisionHandler = GetComponent<ShipCollisionHandler>();
        }

        // Update is called once per frame
        private void Update()
        {
            if (GameData.PlayerHasDied)
            {
                PlayEngineTrail(false);
                return;
            }
        
            ProcessInput();
            transform.position = MovementHelper.ProcessPassBoundary(_boundary, transform.position);
        }

        

        private void ProcessInput()
        {
            // Enable this to exit application while in-game using escape key
            if (Input.GetKey(KeyCode.Q))
            {
                _enableForceQuit = true;
            }
            
            if (Input.GetKey(KeyCode.UpArrow))
            {
                _shipRb2d.AddRelativeForce(new Vector2(0, shipSpeed * Time.deltaTime));
                PlayEngineTrail(true);
            }
        
            if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                PlayEngineTrail(false);
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                _shipRb2d.AddTorque(shipRotation * Time.deltaTime, ForceMode2D.Force);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                _shipRb2d.AddTorque(-shipRotation * Time.deltaTime, ForceMode2D.Force);
            }

            if (Input.GetKey(KeyCode.Escape) && _enableForceQuit)
            {
                Application.Quit();
            }
        }

        private void PlayEngineTrail(bool startEngines)
        {
            foreach (var obj in _engineTrails)
            {
                var trailEmission = obj.GetComponent<ParticleSystem>();

                switch (trailEmission.isPlaying)
                {
                    case false when startEngines:
                        trailEmission.Play();
                        break;
                    case true when !startEngines:
                        trailEmission.Stop();
                        break;
                }
            }
        }
    }
}
