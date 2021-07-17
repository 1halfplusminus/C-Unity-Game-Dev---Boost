
using System;
using System.ComponentModel;
using System.Reflection;
using UnityEngine;

public interface IUseInput
{
    Vector3 inputDirection { get; set; }
    bool spacePressed { get; set; }
}

public interface IUsePhysics
{
    Rigidbody rigidbody { get; }
}
public static class GameExtensions
{
    public static void UpdateInput(this IUseInput inputUser)
    {
        inputUser.inputDirection = Vector3.zero;
        inputUser.spacePressed = false;
        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Press space - Thrust Forward");
            inputUser.spacePressed = true;
            inputUser.inputDirection = Vector3.up;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Debug.Log("Press Left - Rotate Left");
            inputUser.inputDirection = new Vector3(inputUser.inputDirection.x, 0, 1);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            Debug.Log("Press Right - Rotate right");
            inputUser.inputDirection = new Vector3(inputUser.inputDirection.x, 0, -1);
        }
    }
}

public static class TagExtensions
{
    public static string GetDescription(this Enum value)
    {
        Type type = value.GetType();
        string name = Enum.GetName(type, value);
        if (name != null)
        {
            FieldInfo field = type.GetField(name);
            if (field != null)
            {
                DescriptionAttribute attr =
                       Attribute.GetCustomAttribute(field,
                         typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attr != null)
                {
                    return attr.Description;
                }
            }
        }
        return null;
    }
}
