using UnityEngine;
using System.Collections;
using System.Security.AccessControl;

public class PatrolState : IEnemyState
{
    private Enemy enemy;
    private float patrolTimer;
    private float patrolDuration = 10;
    public void Execute()
    {
        Patrol();
        
        enemy.Move();
        
        if (enemy.target!=null && enemy.InShootRange)
        {
            enemy.ChangeState(new RangedState());
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
        //if (enemy.target!=null)
        //{
        //    return;
        //}
        if (other.tag == "Edge")
        {
            enemy.ChangeDirection();
        }
    }
    private void Patrol()
    {
        patrolTimer += Time.deltaTime;
        if (patrolTimer>=patrolDuration)
        {
            patrolTimer = 0;
            enemy.ChangeState(new IdleState());
        }
    }
}
