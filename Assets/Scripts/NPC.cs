using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.EventSystems.EventTrigger;

public class NPC : MonoBehaviour
{
    public float Health = 100;
    public float HealthReducePerMeter = 0.5f;
    public NavMeshAgent Agent;
    public float Epsilon = 0.33f;
    public UI UISystem;
    public AudioSource CollectionSound;
    public Animator Anim;
    public CameraFollower Follower;

    float distanceTraveled = 0;
    float remainingHealth;
    bool isCollectingBalls;
    Ball[] ballsOnTheField;
    Ball currentTarget = null;
    int score = 0;
    Vector3 lastPosition;

    public float RemainingHealth
    {
        get { return remainingHealth; }
    }

    public void TakeAction()
    {
        ballsOnTheField = FindObjectsOfType<Ball>();
        isCollectingBalls = true;
        Anim.CrossFade("Run", 0.1f);

        Follower.Target = transform;
    }

    private Ball ChooseNextTarget()
    {
        Ball bestBall = null;
        float bestScoreRatio = 0f;

        foreach (var ball in ballsOnTheField)
        {
            if (ball == null)
            {
                continue;
            }

            float dist = Vector3.Distance(transform.position, ball.transform.position);
            float cost = dist * HealthReducePerMeter;

            if (cost > Health) continue;

            float ratio = ball.Points / cost;

            if (ratio > bestScoreRatio)
            {
                bestScoreRatio = ratio;
                bestBall = ball;
            }
        }

        return bestBall;
    }

    void Start()
    {
        remainingHealth = Health;
        isCollectingBalls = false;
        lastPosition = transform.position;
        UISystem.SetMaxHealth(Health);
    }

    private void Update()
    {
        if (!isCollectingBalls)
        {
            return;
        }

        float distanceTraveledFromLastFrame = Vector3.Distance(transform.position, lastPosition);
        remainingHealth -= distanceTraveledFromLastFrame;
        lastPosition = transform.position;

        if (remainingHealth <= 0)
        {
            isCollectingBalls = false;
            UISystem.DisplayDead(score);
            Agent.isStopped = true;
            Anim.CrossFade("Idle", 0.1f);
        }

        distanceTraveled += distanceTraveledFromLastFrame;
        UISystem.DisplayDistance(distanceTraveled);
        UISystem.DisplayHealth(remainingHealth);

        if (currentTarget == null)
        {
            currentTarget = ChooseNextTarget();
            if (currentTarget == null)
            {
                Debug.Log("Ran out of balls X(");
                isCollectingBalls = false;

                return;
            }
        }

        Agent.SetDestination(currentTarget.transform.position);

        // Hedefe yeterince yaklaştıysa, topu topla
        if (Vector3.Distance(transform.position, currentTarget.transform.position) < Epsilon)
        {
            CollectBall(currentTarget);
            currentTarget = null;
        }
    }

    private void CollectBall(Ball ball)
    {
        CollectionSound.Play();
        score += ball.Points;
        UISystem.DisplayScore(score);
        Destroy(ball.gameObject);

        ballsOnTheField = FindObjectsOfType<Ball>();
    }
}
