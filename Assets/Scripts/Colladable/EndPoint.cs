using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour, IColladable
{
    [SerializeField]
    ParticleSystem winningEffect;
    public void OnCollision(Collision collision, GameObject collisionWith)
    {
        Debug.Log("Finish the level");
        Player player;
        if(collisionWith.TryGetComponent(out player)) {
            player.OnCollisionWithEndPoint();
            winningEffect.Play();
        }
    }
}
