using System;
using UnityEngine;
using UnityEngine.Splines;

public class AnimateUnitsSpline : MonoBehaviour
{
    public Animator animator;
    private Vector3 lastPosition;
    private float verticalThreshold = 0.1f; // adjust to sensitivity
    public SplineAnimate SplineAnimate;
    public bool isComplete;

    void Start()
    {
        lastPosition = transform.position;
        SplineAnimate.Completed += SplineAnimateOnCompleted;

    }

    private void OnDestroy()
    {
        SplineAnimate.Completed -= SplineAnimateOnCompleted;

    }

    private void SplineAnimateOnCompleted()
    {
        Debug.Log("SplineAnimateOnCompleted");
        animator.Play("Right");

       isComplete = true;
    }

    void Update()
    {
        if (!isComplete)
        {
            Vector3 currentPosition = transform.position;
            Vector3 direction = (currentPosition - lastPosition).normalized;

            // Flip on X if moving left
            if (direction.x > -0.01f)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f); // Facing left
            }
            else if (direction.x < 0.01f)
            {
                transform.localScale = new Vector3(1f, 1f, 1f); // Facing right
            }

            // Animation selection
            if (Mathf.Abs(direction.y) > verticalThreshold)
            {
                if (direction.y > 0)
                    animator.Play("Up");
                else
                    animator.Play("Down");
            }
            else
            {
                animator.Play("Right");
            }

            lastPosition = currentPosition;
        }
       
    }
}
