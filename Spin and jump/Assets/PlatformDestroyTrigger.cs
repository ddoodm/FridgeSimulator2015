using UnityEngine;
using System.Collections;

public class PlatformDestroyTrigger : MonoBehaviour
{
    /// <summary>
    /// Previous and current platform objects (per frame)
    /// </summary>
    private GameObject pPlatform, cPlatform;

    private PlayerController playerController;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void dropPlatform(GameObject plat)
    {
        // Obtain the "dropper controller" for the old object, if it has one
        PlatformDropper dropper = pPlatform.GetComponent<PlatformDropper>();

        if (dropper == null)
            return;

        dropper.drop();
    }

    void Update()
    {
        cPlatform = playerController.objectUnderPlayer;

        // The player is no longer on the (previous) platform, so tell it to go away
        if (cPlatform != pPlatform && pPlatform != null)
            dropPlatform(pPlatform);

        pPlatform = cPlatform;
    }
}