
using UnityEngine;

public interface IUseInput {
    Vector3 inputDirection { get; set; }
}

public interface IUsePhysics {
    Rigidbody rigidbody { get;  }
}
public static class GameExtensions {
    public static void UpdateInput(this IUseInput inputUser) {
        inputUser.inputDirection = Vector3.zero;
        if(Input.GetKey(KeyCode.Space)) {
            Debug.Log("Press space - Thrust Forward");
            inputUser.inputDirection = Vector3.up;
        }
        if(Input.GetKey(KeyCode.LeftArrow)) {
            Debug.Log("Press Left - Rotate Left");
            inputUser.inputDirection = new Vector3(inputUser.inputDirection.x,0,1);
        }
        else if(Input.GetKey(KeyCode.RightArrow)) {
            Debug.Log("Press Right - Rotate right");
            inputUser.inputDirection = new Vector3(inputUser.inputDirection.x,0,-1);
        }
    }
}