/* ===================================
 *     PATH GENERATION CONTROLLER
 * ===================================
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum PathType { PATH_S, PATH_L, PATH_R, SPINNER, WALL, GAP, PATH_S_NOOBS, STICKY };

[System.Serializable]
public class PathTile
{
    public GameObject gameObject;
    public GameObject instance;
    public PathType type;

    public PathTile(GameObject gameObject, PathType type)
    {
        this.gameObject = gameObject;
        this.type = type;
    }

    public Vector3 size
    {
        get
        {
            BoxCollider collider = gameObject.GetComponent<BoxCollider>();
            return collider.size;
        }
    }

	public Vector3 scale
	{
		get
		{
            return gameObject.transform.localScale;
		}
	}
}

public class DPathGen : MonoBehaviour
{
    /// <summary>
    /// Path tile prefab objects
    /// </summary>
    public PathTile
        prefab_path_s,
        prefab_path_l,
        prefab_path_r,
        prefab_spinner,
        prefab_gap,
        prefab_wall,
		prefab_sticky,
        prefab_path_s_noobs;

    /// <summary>
    /// The maximum distance, from the player, that new tiles will spawn
    /// </summary>
    public float spawnDistance = 64.0f;

    /// <summary>
    /// The maximum number of new platforms that may be created per update
    /// </summary>
    public int maxPerFrame = 3;

    /// <summary>
    /// The direction in which the path is currently travelling
    /// </summary>
    private Vector3 direction = Vector3.forward;
    private Vector3 tileIdx = Vector3.zero;
    private Vector3 worldPos = Vector3.zero;

    private Stack<PathTile> tiles;
    PathType lastType = PathType.PATH_S;

    public int maxBoringPaths = 3;
    private int boringPathCount = 0;

    private PlayerController player;

    public void Start()
    {
        tiles = new Stack<PathTile>();
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

        // The first tile will spawn as if there was an imaginary 'S' path behind it
        tiles.Push(prefab_path_s);
        lastType = tiles.Peek().type;

        // Spawn at least two S paths first
        for (int i = 0; i < 2; i++)
            spawnType(PathType.PATH_S_NOOBS);
    }

    public void Update()
    {
        int genCount = maxPerFrame;
        bool collision = false;

        /*
         * New tiles are spawned each frame, so long as the following conditions are met:
         * - The player is near enough to the last tile
         * - We have not exceeded the 'max tiles per frame' limit
         * - There is no collision with the world (exceptional case)
         */
        while (
            (player.transform.position - worldPos).magnitude < spawnDistance
            && genCount --> 0
            && !collision)
                collision = !fsmPathGen();
    }

    private bool fsmPathGen()
    {
        PathType nextType = PathType.PATH_S;

        switch (lastType = tiles.Peek().type)
        {
            case PathType.PATH_S:
            case PathType.PATH_S_NOOBS: nextType = newPathFrom_PathS(); break;
            case PathType.PATH_L:
            case PathType.PATH_R: nextType = newPathFrom_PathLR(); break;
            case PathType.SPINNER: nextType = newPathFrom_Spinner(); break;
            case PathType.GAP: nextType = newPathFrom_Gap(); break;
            case PathType.WALL: nextType = newPathFrom_Wall(); break;
			case PathType.STICKY: nextType = newPathFrom_Sticky(); break;
        }

        lastType = nextType;

        return spawnType(nextType);
    }

    private PathType newPathFrom_PathS()
    {
        const int options = 7;

        int rand = (int)(Random.value * (float)(options) - Mathf.Epsilon);
        switch(rand)
        {
            case 0:
                if (boringPathCount > maxBoringPaths)
                    return newPathFrom_PathS(); // Try again
                return PathType.PATH_S;
            case 1: return PathType.PATH_L;
            case 2: return PathType.PATH_R;
            case 3: return PathType.GAP;
            case 4: return PathType.SPINNER;
            case 5: return PathType.WALL;
			//case 6: return PathType.STICKY;
            default: return PathType.PATH_S;
        }
    }

    private PathType newPathFrom_PathLR()
    {
        const int options = 3;

        int rand = (int)(Random.value * (float)(options) - Mathf.Epsilon);
        switch (rand)
        {
            case 0: return PathType.PATH_S;
            case 1: return PathType.GAP;
            case 2: return PathType.SPINNER;
            default: return PathType.PATH_S;
        }
    }

    private PathType newPathFrom_Spinner()
    {
        const int options = 3;

        int rand = (int)(Random.value * (float)(options) - Mathf.Epsilon);
        switch (rand)
        {
            case 0: return PathType.PATH_S;
            //case 1: return PathType.PATH_L;
            //case 2: return PathType.PATH_R;
            default: return PathType.PATH_S;
        }
    }

    private PathType newPathFrom_Gap()
    {
        const int options = 3;

        int rand = (int)(Random.value * (float)(options) - Mathf.Epsilon);
        switch (rand)
        {
            case 0:
                if (boringPathCount > maxBoringPaths)
                    return newPathFrom_Gap(); // Try again
                return PathType.PATH_S;
            case 1: return PathType.PATH_L;
            case 2: return PathType.PATH_R;
            default: return PathType.PATH_S;
        }
    }

    private PathType newPathFrom_Wall()
    {
        return PathType.PATH_S_NOOBS;
    }


	private PathType newPathFrom_Sticky()
	{
		return PathType.PATH_S_NOOBS;
	}



    private bool spawnType(PathType type)
    {
        switch(type)
        {
            case PathType.PATH_S:
                boringPathCount++;
                return spawn(prefab_path_s, 0.0f);
            case PathType.PATH_L:
                return spawn(prefab_path_l, -90.0f);
            case PathType.PATH_R:
                return spawn(prefab_path_r, 90.0f);
            case PathType.SPINNER:
                int attempts = 3;
                boringPathCount = 0;
                while (attempts --> 0)
                {
                    float randomDirection = Random.Range(-1, 1) * 90.0f;
                    if (spawn(prefab_spinner, randomDirection))
                        return true;
                }
                return false;
            case PathType.GAP:
                return spawn(prefab_gap, 0.0f);
            case PathType.WALL:
                boringPathCount = 0;
                return spawn(prefab_wall, 0.0f);
			case PathType.STICKY:
				boringPathCount = 0;
				return spawn (prefab_sticky, 0.0f);
            case PathType.PATH_S_NOOBS:
                boringPathCount++;
                return spawn(prefab_path_s_noobs, 0.0f);
            default:
                return false;
        }
    }

    /// <summary>
    /// Spawn the specified path tile, heading in the specitied direction.
    /// </summary>
    /// <param name="prefab">The path tile to spawn</param>
    /// <param name="newDirection">The angle that the new tile should face (-90', 90')</param>
    /// <returns>False if the spawn would result in an intersection with an existing tile, and therefore, was not created.</returns>
    private bool spawn(PathTile prefab, float newDirection)
    {
        // Check whether instantiation would result in a collision with another path
        if (willCollide(prefab, newDirection))
            return false;

        Debug.Log(string.Format("Spawning {0}", prefab.gameObject));

        GameObject instance = Instantiate(prefab.gameObject, worldPos, Quaternion.LookRotation(direction)) as GameObject;
        prefab.instance = instance;

        // Handle spinner direction
        if (prefab.type == PathType.SPINNER)
            giveSpinnerDirection(instance, newDirection);

        // Make the tile most recent
        tiles.Push(prefab);
        tileIdx += direction;

        // Update the world position
        switch(prefab.type)
        {
            // Straight paths add only the direction in which they travel
		case PathType.PATH_S: case PathType.PATH_S_NOOBS: case PathType.GAP: case PathType.WALL: case PathType.STICKY:
                worldPos += prefab.size.z * prefab.scale.z * direction;
                break;

            case PathType.PATH_L: case PathType.PATH_R: case PathType.SPINNER:
                worldPos += Vector3.Scale(prefab.size, direction * 0.5f);
                changeDirection(newDirection);
                worldPos += Vector3.Scale(prefab.size, direction * 0.5f);
                break;
        }

        return true;
    }

    private void giveSpinnerDirection(GameObject instance, float newDirection)
    {
        Rotator rotator = instance.transform.GetChild(0).GetComponent<Rotator>();
        rotator.direction = (int)Mathf.Sign(newDirection);
    }

    private bool willCollide(PathTile prefab, float newDirectionAngle)
    {
        Vector3 ro = worldPos;
        Vector3 rd = direction;
        float rayLength = Vector3.Scale(prefab.size, rd).magnitude * 2.5f;

        // Vector orthonormal to the direction and up vectors (right vector)
        Vector3 right = Vector3.Cross(direction, Vector3.up);
        right *= prefab.size.x * 0.75f;

        RaycastHit? frontHit;
        if ((frontHit = threeRayCollision(ro, rd, right, rayLength)).HasValue)
            return willCollideAfterPanic(frontHit.Value);

        // For paths that will modify the direction, check the new direction, too
        if(newDirectionAngle != 0.0f)
        {
            Vector3 newDirection = Quaternion.AngleAxis(newDirectionAngle, Vector3.up) * direction;
            right = Vector3.Cross(newDirection, Vector3.up);

            // TODO: Bad constant, but that is the length of an S + an L
            RaycastHit? sideHit;
            if ((sideHit = threeRayCollision(ro + direction * prefab.size.z, newDirection, right, 15.0f)).HasValue)
                return willCollideAfterPanic(sideHit.Value);
        }

        return false;

        /*
        // Create four rays in a '+' form
        int rays = 1;
        for(int i=0; i<rays; i++)
        {
            float theta = (float)i / (float)rays * Mathf.PI * 360.0f;
            Quaternion rot = Quaternion.AngleAxis(theta, Vector3.up);

            Vector3 rd = rot * Vector3.forward;

            float rayLength = Vector3.Scale(prefab.size, rd).magnitude;

            Ray ray = new Ray(ro, rd);
            RaycastHit hit;
            if (!Physics.Raycast(ray, out hit, rayLength))
                continue;

            return true;
        }
        
        return false; */
    }

    /// <summary>
    /// Check whether we need to panic, and if so, remove the obstructing object.
    /// If the function removes the obstruct, it returns 'false', as in 'there is no longer a collision'.
    /// Otherwise, there is still a collision.
    /// </summary>
    private bool willCollideAfterPanic(RaycastHit hit)
    {
        // Check whether the player is within panic range
        //if((player.transform.position - worldPos).magnitude < panicDistance)
        if(player.objectUnderPlayer == tiles.Peek().instance)
        {
            // Get the root parent GameObject for the one that is causing the collision
            GameObject hitRoot = hit.collider.gameObject.transform.root.gameObject;

            Debug.Log("PATH GENERATOR PANIC ==== On platform: " + player.currentPlatform + ", Last platform: " + tiles.Peek().instance + ", Destroying platform: " + hitRoot);

            // If so, destroy the obstruction, and allow the generator to continue on this frame
            Destroy(hitRoot);
            return false;
        }

        return true;
    }

    private RaycastHit? threeRayCollision(Vector3 ro, Vector3 rd, Vector3 rightOffset, float rayLength)
    {
        Ray rayL = new Ray(ro - rightOffset, rd);
        Ray rayC = new Ray(ro, rd);
        Ray rayR = new Ray(ro + rightOffset, rd);

        RaycastHit outHit;

        if (Physics.Raycast(rayL, out outHit, rayLength))
            return outHit;
        if (Physics.Raycast(rayC, out outHit, rayLength))
            return outHit;
        if (Physics.Raycast(rayR, out outHit, rayLength))
            return outHit;

        return null;
    }

    private void changeDirection(float angle)
    {
        direction = Quaternion.AngleAxis(angle, Vector3.up) * direction;
    }
}
