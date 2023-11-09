using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagnetFishing
{
    public class RodTip : MonoBehaviour
    {
        [SerializeField] private VectorVariable _rodTipPosition;

        private Vector3 previousPosition;
        private float previousTime;

        private void Awake()
        {
            // Initialize the previous position and time.
            previousPosition = transform.position;
            previousTime = Time.time;
        }

        private void FixedUpdate()
        {
            _rodTipPosition.SetVector(transform.position);

            // Calculate the change in position.
            Vector3 currentPosition = transform.position;
            float currentTime = Time.time;
            Vector3 displacement = currentPosition - previousPosition;

            // Calculate the time elapsed.
            float deltaTime = currentTime - previousTime;

            // Calculate velocity using the formula: velocity = displacement / time
            Vector3 velocity = displacement / deltaTime;

            // Now you have the velocity of the GameObject.
            float speed = velocity.magnitude;
            //Debug.Log("Speed: " + speed);

            // Update the previous position and time for the next frame.
            previousPosition = currentPosition;
            previousTime = currentTime;
        }
    }
}
