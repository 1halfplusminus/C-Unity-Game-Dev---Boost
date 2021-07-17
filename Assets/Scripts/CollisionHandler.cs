using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {
        HandleCollision(other);
    }

    private void HandleCollision(Collision other) {
        IColladable colladable;
        if(other.gameObject.TryGetComponent<IColladable>(out colladable)) {
            colladable.OnCollision(other, gameObject);
        } 
    }
}
