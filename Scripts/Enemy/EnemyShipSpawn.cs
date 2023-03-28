using UnityEngine;

namespace Enemy
{
    public class EnemyShipSpawn : MonoBehaviour
    {
        [SerializeField] private GameObject enemyShip;
        [SerializeField] private float spawnDelay;
        [SerializeField] private float repeatRate;
        [SerializeField] private Transform targetTransform;

        private Bounds _bounds;
        // Start is called before the first frame update
        private void Start()
        {
            _bounds = GameObject.FindGameObjectWithTag("Boundary").GetComponent<Collider2D>().bounds;
            InvokeRepeating(nameof(SpawnNewEnemyShip), spawnDelay, repeatRate);
        }

        // // Update is called once per frame
        // void Update()
        // {
        //
        // }

        private void SpawnNewEnemyShip()
        {
            var newEnemyShip = Instantiate(enemyShip, transform);
            newEnemyShip.GetComponent<EnemyPathFinder>().TargetTransform = targetTransform;
            newEnemyShip.transform.position = MovementHelper.GeneratePosition(_bounds);
        }
        
        
    }
}
