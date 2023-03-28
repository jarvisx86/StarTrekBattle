using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public static class MovementHelper
{
    public static Vector3 ProcessPassBoundary(Collider2D boundary, Vector3 position)
    {
        var bounds = boundary.bounds;

        if (bounds.Contains(position)) return position;

        var currentPosY = position.y;
        var currentPosX = position.x;

        if (currentPosX > bounds.max.x)
        {
            currentPosX = bounds.min.x;
        }
        else if (currentPosX < bounds.min.x)
        {
            currentPosX = bounds.max.x;
        }

        if (currentPosY > bounds.max.y)
        {
            currentPosY = bounds.min.y;
        }
        else if (currentPosY < bounds.min.y)
        {
            currentPosY = bounds.max.y;
        }

        return new Vector3(currentPosX, currentPosY);
    }
    
    public static Vector3 GeneratePosition(Bounds bounds)
    {
        // need an offset so the asteroid does not show up on the screen
        const float positionOffset = 5f;
            
        // choose which side the asteroid will be generated on 
        // 0 = top, 1 = bottom, 2 = left side, 3 = right side
        var topOrSide = Math.Floor((double)Random.Range(0, 4));

        // switch expression to generate our position
        var position = (int)topOrSide switch
        {
            // top side
            (0) => new Vector3(Random.Range(bounds.max.x, bounds.min.x), bounds.max.y + positionOffset),

            // bottom side
            (1) => new Vector3(Random.Range(bounds.max.x, bounds.min.x), bounds.min.y - positionOffset),

            // left side
            (2) => new Vector3(bounds.min.x - positionOffset, Random.Range(bounds.min.y, bounds.max.y)),

            // right side
            (3) => new Vector3(bounds.max.x + positionOffset, Random.Range(bounds.min.y, bounds.max.y)),

            // default is top side
            _ => new Vector3(Random.Range(bounds.max.x, bounds.min.x), bounds.max.y + positionOffset)
        };

        return position;
    }
}
