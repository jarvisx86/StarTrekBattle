using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Asteroid")]
public class Asteroid : ScriptableObject
{
    public Sprite image;
    public float movementSpeed;
    public float rotationSpeed;
}
