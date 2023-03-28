using UnityEngine;

public class DisruptorBolt : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Warbird")) return;
        Destroy(gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Destroy(gameObject);
    }
}
