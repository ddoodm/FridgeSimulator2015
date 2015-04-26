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

    void LateUpdate()
    {
        cPlatform = playerController.objectUnderPlayer;

        int cPlatformID = cPlatform == null ? -1 : cPlatform.GetInstanceID();
        int pPlatformID = pPlatform == null ? -1 : pPlatform.GetInstanceID();

        if(cPlatformID != pPlatformID && cPlatform != pPlatform)
        {
            if (pPlatform != null)
            {
                bool pPlatformIsValid = false;
                foreach (string tag in playerController.platformTags)
                    if (tag == pPlatform.tag)
                        pPlatformIsValid = true;

                if (pPlatformIsValid)
                {
                    Debug.Log("Dropping platform " + pPlatform);
                    dropPlatform(pPlatform);
                }
            }
        }

        /*
        // The player is no longer on the (previous) platform, so tell it to go away
        if (cPlatformID != pPlatformID && pPlatform != null)
        {
            // Check whether we're over a gate (or similar)
            bool cPlatformIsInvalid = true;
            if (cPlatform == null)
                cPlatformIsInvalid = false;
            else
                foreach (string tag in playerController.platformTags)
                    if (tag == cPlatform.tag)
                        cPlatformIsInvalid = false;

            if (!cPlatformIsInvalid)
            {
                Debug.Log(string.Format("PlatformDestroyTrigger - pPlatform:{0}, cPlatform:{1}", pPlatform, cPlatform));
                dropPlatform(pPlatform);
            }
        }*/

        pPlatform = cPlatform;
    }
}