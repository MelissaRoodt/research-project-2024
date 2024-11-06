using System;
using UnityEngine;

/// Controls the dog game object behaviour using the DogAI()
/// 
/// Lifecycle: idle - (received bone) -> eat - (done eating) -> move - (reached destination) -> idle
public class DogController : MonoBehaviour
{
    public event EventHandler onReachEnd; /// event triggered when dog reached the next point
    public event EventHandler OnAddDoggyPoints; /// event triggered when dog gets a treat 

    private enum State { idle, move, eat, gameEnd }
    private State currentState;
    private State lastState;

    //animations
    [SerializeField] private Animator anim;
    [SerializeField] private AnimatorController animatorController; /// see AnimatorController()

    //eat
    private float waitingForBoneTimeElapsed; //return this to the ai
    private bool hasEatenBone = false;
    [SerializeField] private LayerMask boneLayer;
    [SerializeField] private float collideRadius = 0.2f;

    //move
    [SerializeField] private float speed = 5f;

    //AI
    [SerializeField] private DogAI ai; /// see DogAI()
    private Vector3 nextPosition;

    private void Start()
    {
        InitialState();

        animatorController.OnAnimationHasFinished += AnimatorController_OnAnimationHasFinished;
    }

    /// When game starts sets the initial state of the dog to idle
    public void InitialState()
    {
        currentState = State.idle;
        lastState = currentState;
        anim.Play("idle");
    }

    /// Handles the onAnimationHasFinished event from AnimatorController()
    /// 
    /// When the animation finished set eaten bone to true, this result in change in state -> state = move

    private void AnimatorController_OnAnimationHasFinished(object sender, System.EventArgs e)
    {
        hasEatenBone = true;
    }

    private void FixedUpdate()
    {
        switch (currentState)
        {
            case State.idle:
                Idle();
                break;
            case State.move:
                Move();
                break;
            case State.eat:
                Eat();
                break;
            case State.gameEnd:
                GameEnd();
                break;
        }
    }

    /// Set the dog to idle animation and wait for a bone collision
    public void Idle()
    {
        if (lastState != currentState) anim.Play("idle");

        bool recievedBone = Physics2D.OverlapCircle(gameObject.transform.position, collideRadius, boneLayer);

        if (recievedBone)
        {
            currentState = State.eat;
        }
    }

    /// Set the dog to move animation and move towards next point 
    public void Move()
    {
        if (lastState != currentState)
        {
            anim.Play("run");
            lastState = currentState;

            ///ai calculates the next point and moves to that point
            ai.CalculateNewDistance();
            nextPosition = ai.getNextPosition(transform.position);
        }

        if (Vector3.Distance(nextPosition, gameObject.transform.position) < 0.01)
        {
            currentState = State.idle;
        }
        else
        {
            ///move to next predicted point
            transform.position = Vector3.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);

            //game ends
            if (transform.position.x >= 4.5f)
            {
                transform.position = new Vector3(4.5f, transform.position.y);
                currentState = State.gameEnd;
                ///game end here -> reset game && go to main menu
                onReachEnd?.Invoke(this, new EventArgs());  
            }
        }

    }

    /// Set the dog to eat animation and waits for AnimatorController_OnAnimationHasFinished() event
    public void Eat()
    {
        if (lastState != currentState)
        {
            anim.Play("eat");
            lastState = currentState;
            OnAddDoggyPoints?.Invoke(this, new EventArgs());
        }

        if (hasEatenBone)
        {
            currentState = State.move;
            hasEatenBone = false;
        }
    }

    /// Set the dog to idle animation
    private void GameEnd()
    {
        if (lastState != currentState)
        {
            anim.Play("idle");
            lastState = currentState;
        }
    }

}
