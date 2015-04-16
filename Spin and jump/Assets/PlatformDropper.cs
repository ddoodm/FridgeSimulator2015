using UnityEngine;
using System.Collections;

public class PlatformDropper : MonoBehaviour
{
    public float acceleration = 10.0f;
    public float destroyDistanceY = 25.0f;

    private bool dropping = false;
    private Vector3 velocity = Vector3.zero;
    private PlayerController player;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    public void drop()
    {
        dropping = true;
    }

    void Update()
    {
        if(dropping)
        {
            // Newton-Euler integration
            velocity += Vector3.down * acceleration * Time.deltaTime;
            transform.position += velocity * Time.deltaTime;
        }

        // Canculate Y distance from the player
        float yDist = Mathf.Abs(player.transform.position.y - this.transform.position.y);
        if (yDist >= 25.0f)
            Destroy(this.gameObject);
    }
}