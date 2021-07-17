using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Obstacle : MonoBehaviour, IColladable
{
    public void OnCollision(Collision collision, GameObject collisionWith)
    {
       Player player;
       if(collisionWith.TryGetComponent(out player)) {
           player.OnCollisionWithObstacle();
       }
    }
    
}
