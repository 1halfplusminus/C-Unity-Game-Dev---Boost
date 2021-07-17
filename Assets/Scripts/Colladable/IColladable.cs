using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IColladable {    
    void OnCollision(Collision collision, GameObject collisionWith);
}


public static class ColladableExtensions {
  
}