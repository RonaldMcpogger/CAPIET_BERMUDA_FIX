using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldData : MonoBehaviour
{
    [SerializeField] private float drag;
    [SerializeField] private float gravity;
    [SerializeField] private float jumpHeight;

    public float Drag { get => drag; }
    public float Gravity { get => gravity; }

    public float JumpHeight { get => jumpHeight; }
}
