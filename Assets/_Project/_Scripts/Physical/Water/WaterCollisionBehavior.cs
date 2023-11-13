using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagnetFishing
{
    public class WaterCollisionBehavior : MonoBehaviour
    {
        private static GameObject currentBobber;
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Bobber") && (currentBobber == null || currentBobber != collision.gameObject))
            {
                currentBobber = collision.gameObject;
                Rigidbody thingRigidbody = collision.gameObject.transform.GetComponent<Rigidbody>();
                thingRigidbody.velocity = new Vector3(0, 0, 0);
                Debug.Log("Bobber stopped in its tracks! Bouncing now:");
                Hook currentHook = currentBobber.GetComponent<Hook>();
                currentHook.EnterWaterBehavior();
            }
        }
    }
}
