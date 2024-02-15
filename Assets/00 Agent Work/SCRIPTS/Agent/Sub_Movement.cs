using UnityEngine;
using DG.Tweening;

public class Sub_Movement : MonoBehaviour
{
    public float swimSpeed = 1.0f;
    public float directionChangeInterval = 3.0f;
    public GameObject followTarget;
    public float minFollowDistance = 1.0f;
    public MasterEmotion MasterEmotion;



    private Vector3 targetPosition;
    private float timeSinceDirectionChange = 0.0f;
    private Tween moveTween;
    private bool isMoving = false; // Track whether movement is active
    private bool isFollowing = false;
    private float adjustedSpeed;

    void Start()
    {
        targetPosition = transform.position;
        // Optionally start moving immediately; else, call startMovement externally
        // startMovement();
    }

    void Update()
    {

        adjustedSpeed = swimSpeed * MasterEmotion.emotionIntensity;

        // Removed the return statement to allow following behavior to execute independently
        if (isMoving)
        {
            // Change direction periodically 
            timeSinceDirectionChange += Time.deltaTime; 
            if (timeSinceDirectionChange >= directionChangeInterval)
            {
                ChangeSwimDirection();
                timeSinceDirectionChange = 0f;
            }
        }

        if (isFollowing)
        {
            FollowTarget();
        }
    }

    public void followTargetManager()
    {
        if (isFollowing)
        {
            stopFollow();
        }
        else
        {
            startFollow();
        }
    }

    void FollowTarget()
    {
        if (followTarget == null) return;

        Vector3 directionToTarget = followTarget.transform.position - transform.position;
        float distance = directionToTarget.magnitude;

        // Check if we are closer than the minimum distance
        if (distance < minFollowDistance)
        {
            if (moveTween != null && moveTween.IsActive()) moveTween.Kill();
            return; // Stop moving if too close
        }

        Vector3 newPosition = followTarget.transform.position - directionToTarget.normalized * minFollowDistance;
        if (moveTween == null || !moveTween.IsActive())
        {
            // Move towards the new position with easing
            moveTween = transform.DOMove(newPosition, (distance / adjustedSpeed)).SetEase(Ease.InOutQuad);
        }
    }

    public void startFollow()
    {
        isFollowing = true;
        stopMovement(); // Explicitly stop any random movement
        if (moveTween != null && moveTween.IsActive()) moveTween.Kill(); // Ensure any movement tween is stopped
    }


    public void stopFollow()
    {
        isFollowing = false;
        if (moveTween != null && moveTween.IsActive()) moveTween.Kill();
    }

    public bool IsFollow()
    {
        return isFollowing;
    }

    void ChangeSwimDirection()
    {
        if (moveTween != null && moveTween.IsActive()) moveTween.Kill(); // Kill existing tween

        // Calculate a new target position based on the desired direction and distance
        Vector3 direction = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.2f, 0.2f), Random.Range(-0.5f, 0.5f)).normalized;
        targetPosition += direction * adjustedSpeed; // Adjust target position based on the swim speed and direction

        // Create a new tween to move towards the target position with easing
        moveTween = transform.DOMove(targetPosition, directionChangeInterval).SetEase(Ease.InOutSine);
    }

    public void startMovement()
    {
        isMoving = true;
        stopFollow(); // Explicitly stop following
        ChangeSwimDirection(); // Start or resume random movement
    }

    public void stopMovement()
    {
        if (moveTween != null && moveTween.IsActive()) moveTween.Kill(); // Stop current movement tween
        isMoving = false; // Disable movement updates
    }

    public bool IsMoving()
    {
        return isMoving;
    }
}



