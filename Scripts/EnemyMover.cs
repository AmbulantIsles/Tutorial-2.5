using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    // animate the game object from -1 to +1 and back

    public float startPoint = -1.0F;
    public float endPoint = 1.0F;

    void Update()
    {
        gameObject.transform.localPosition = new Vector2(Mathf.Lerp(startPoint, endPoint, Mathf.PingPong(Time.time, 1)), transform.localPosition.y);
    }
}
