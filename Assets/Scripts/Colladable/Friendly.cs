using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friendly : MonoBehaviour, IColladable
{
    public void OnCollision(Collision collision, GameObject collisionWith)
    {
        Debug.Log("Collide on friendly");
    }
}
