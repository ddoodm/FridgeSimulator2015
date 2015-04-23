using UnityEngine;
using System.Collections;

/// <summary>
/// Obstacle spawning for 'S' paths
/// </summary>
public class ObstableGen : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;

    public float probability = 0.25f;
    public float pathWidth = 2.5f;
    public float pathLength = 10.0f;
    public float obstacleWidth = 1.5f;

    void Start()
    {
        if (obstaclePrefabs.Length == 0)
            throw new MissingComponentException("ObstableGen - No obstacle prefabs defined");

        // Determine whether to place an obstacle on this path segment
        if (Random.value > probability)
            return;

        // Determine the prefab to use
        int prefabId = Random.Range(0, obstaclePrefabs.Length-1);
        GameObject prefab = obstaclePrefabs[prefabId];

        float side = Mathf.Round(Random.value) * 2.0f - 1.0f;
        Vector3 xOffset = this.transform.right * side * (pathWidth - obstacleWidth);
        Vector3 zOffset = this.transform.forward * (pathLength - obstacleWidth);
        Vector3 position = this.transform.position + xOffset + zOffset;

        GameObject obstInst = Instantiate(prefab, position, Quaternion.identity) as GameObject;
        obstInst.transform.parent = this.transform;

        Debug.Log(string.Format("Obstacle - side:{0}, prefabID:{1}", side, prefabId));
    }
}
