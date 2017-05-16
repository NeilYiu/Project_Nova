using UnityEngine;
using System.Collections;

public class RangedState : IEnemyState
{
    private Enemy enemy;
    public void Execute()
    {
        if (enemy.target == null)
        {
            enemy.ChangeState(new PatrolState());
        }
        if (enemy.InMeleeRange)
        {
            enemy.ChangeState(new MeleeState());
        }
        if (enemy.coolDownTimer <= 0)
        {
            enemy.isMelee = false;
            enemy.Attack();
        }
        if (enemy.target!=null&&!enemy.isAttacking)
        {
            enemy.Move();
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
            enemy.target = null;
            enemy.ChangeDirection();
            enemy.ChangeState(new PatrolState());
        }
    }
}
