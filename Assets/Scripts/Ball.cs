using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public struct Ball
{
    public Bounds position;
    public Vector3 direction;
    public Ball(Bounds ball)
    {
        this.position = ball;
        this.direction = Vector3.zero;
    }
}
