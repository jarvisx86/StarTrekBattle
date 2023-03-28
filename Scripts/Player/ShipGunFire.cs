using Player;
using UnityEngine;
using Utility;

public class ShipGunFire : MonoBehaviour
{
    [SerializeField] private float boltSpeed = 0f;
    [SerializeField] private GameObject phaserBolt;

    public Transform transformOffset;
    private ParticleSystem _phaserObject;
    private AudioSource _phaserSfx;
    private ShipCollisionHandler _collisionHandler;

    // Start is called before the first frame update
    private void Start()
    {
        _phaserObject = GameObject.FindGameObjectWithTag("Phaser").GetComponent<ParticleSystem>();
        _collisionHandler = GetComponent<ShipCollisionHandler>();
        _phaserSfx = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (GameData.PlayerHasDied) return;
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
           ShootPhasers();
        }
    }
    

    private void ShootPhasers()
    {
        _phaserObject.Play();

        var newPhaserBolt = Instantiate(phaserBolt, transformOffset.position, transform.rotation);
        newPhaserBolt.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0, boltSpeed * Time.fixedDeltaTime));
        _phaserSfx.Play();
    }
}
