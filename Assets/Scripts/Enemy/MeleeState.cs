using UnityEngine;
using System.Collections;


public class MeleeState : IEnemyState
{
    private Enemy enemy;
    public void Execute()
    {
        if (enemy.target == null)
        {
            enemy.ChangeState(new PatrolState());
        }
        if (enemy.InMeleeRange && enemy.coolDownTimer <= 0)
        {
            enemy.isMelee = true;
            enemy.Attack();
        }
        else
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
        //if (enemy.target != null)
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
