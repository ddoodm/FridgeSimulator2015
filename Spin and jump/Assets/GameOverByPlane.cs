using UnityEngine;
using System.Collections;

public class GameOverByPlane : MonoBehaviour
{
    /// <summary>
    /// The distance of the plane (along its normal) from (0,0,0) (D)
    /// </summary>
    public float distance = -1.0f;

    public GameController gameController;

    void Update()
    {
        // Destroy if the object intersects the plane
        if (this.transform.position.y < distance)
            gameController.GameOver();
    }
}
