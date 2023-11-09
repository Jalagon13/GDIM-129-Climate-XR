using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagnetFishing
{
    public class WaterCollisionBehavior : MonoBehaviour
    {
        private void OnTriggerEnter(Collider thingEntering)
        {
            if (thingEntering.gameObject.CompareTag("Bobber"))
            {
                Rigidbody thingRigidbody = thingEntering.GetComponent<Rigidbody>();
                thingRigidbody.velocity = new Vector3(0, 0, 0);
                Debug.Log("Bobber stopped in its tracks! Bouncing now:");
                StartCoroutine(BobberBounceCoroutine(thingEntering.gameObject));
                Debug.Log("Bobber done bouncing!");
            }
        }

        private IEnumerator BobberBounceCoroutine(GameObject bobber)
        {
            Vector3 originalPosition = bobber.transform.position;
            while (bobber.transform.position.y < originalPosition.y + 0.5f)
            {
                bobber.transform.position = bobber.transform.position + new Vector3(0, 0.05f, 0);
                yield return null;
            }
            while (bobber.transform.position.y > originalPosition.y - 0.3f)
            {
                bobber.transform.position = bobber.transform.position - new Vector3(0, 0.05f, 0);
                yield return null;
            }
            while(bobber.transform.position.y < originalPosition.y + 0.1f)
            {
                bobber.transform.position = bobber.transform.position + new Vector3(0, 0.05f, 0);
                yield return null;
            }
            while (bobber.transform.position.y > originalPosition.y-0.005f)
            {
                bobber.transform.position = bobber.transform.position - new Vector3(0, 0.05f, 0);
                yield return null;
            }

        }
    }
}
