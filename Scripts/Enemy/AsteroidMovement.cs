using UnityEngine;

namespace Enemy
{
    public class AsteroidMovement : MonoBehaviour
    {
        private Collider2D _boundary;
    
        private void Start()
        {
            _boundary = GameObject.FindGameObjectWithTag("Boundary").GetComponent<Collider2D>();
        }

        // Update is called once per frame
        private void Update()
        {
            transform.position = MovementHelper.ProcessPassBoundary(_boundary, transform.position);
        }
    }
}
