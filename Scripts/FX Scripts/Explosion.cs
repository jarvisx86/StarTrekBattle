using UnityEngine;

namespace FX_Scripts
{
    public class Explosion : MonoBehaviour
    {
        [SerializeField] private Transform target;

        // Update is called once per frame
        private void Update()
        {
            if (target.gameObject.activeSelf)
            {
                transform.position = target.position;
            }
        }
    }
}
