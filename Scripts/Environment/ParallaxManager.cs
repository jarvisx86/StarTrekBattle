using UnityEngine;

public class ParallaxManager : MonoBehaviour
{
    [Header("Parallax Objects")]
    [SerializeField] private Transform shipPosition;
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject midground;
    
    [Header("Tuning Ratios")]
    [SerializeField] private float backgroundParallax;

    [SerializeField] private float migroundParallax;

    // Update is called once per frame
    void Update()
    {
        var position = shipPosition.position;
        var posX = position.x;
        var posY = position.y;

        var preParallaxPosition = new Vector3(posX, posY, 0f);
        
        // setting Background parallax rate
        background.transform.position = preParallaxPosition * backgroundParallax;
        
        // setting mid-ground parallax rate
        midground.transform.position = preParallaxPosition * migroundParallax;
    }
}
