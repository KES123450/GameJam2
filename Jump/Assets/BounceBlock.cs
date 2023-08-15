using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBlock : MonoBehaviour
{
    [SerializeField] public float force;
    [SerializeField] [Range(0f, 360f)] public float deg;
    private float directionX;
    private float directionY;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            directionX = Mathf.Sin(ConvertDegToRad(deg));
            directionY = Mathf.Cos(ConvertDegToRad(deg));
            Vector2 direction = new Vector2(directionX, directionY);
            Vector2 bounceVector = direction.normalized * force;
            collision.rigidbody.AddForce(bounceVector, ForceMode2D.Impulse);
        }
    }

    float ConvertDegToRad(float deg)
    {
        return (deg * (Mathf.PI / 180));
    }

}
