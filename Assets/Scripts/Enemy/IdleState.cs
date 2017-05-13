using UnityEngine;
using System.Collections;

public class IdleState : IEnemyState
{
    private Enemy enemy;
    private float idleTimer;
    private float idleDuration=2f;
    public void Execute()
    {
        Idle();
        if (enemy.target!=null)
        {
            enemy.ChangeState(new PatrolState());
        }
    }

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Exit()
    {

    }

    public void OnTriggerEnter(Collider2D other)
    {

    }

    void OnTriggerStay(Collider2D other)
    {
        if (other.tag == "Edge")
        {
            //enemy.ChangeDirection();
        }
    }

    private void Idle()
    {
        enemy.GetComponent<Animator>().SetBool("isWalking",false);
        idleTimer += Time.deltaTime;
        if (idleTimer>=idleDuration)
        {
            enemy.ChangeState(new PatrolState());
        }
    }
}
