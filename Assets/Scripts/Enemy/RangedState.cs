using UnityEngine;
using System.Collections;

public class RangedState : IEnemyState
{
    private Enemy enemy;
    private float shootTimer;
    private float bulletCoolDown=10;
    public void Execute()
    {
        if (enemy.coolDownTimer <= 0)
        {
            if (enemy.target==null)
            {
                enemy.ChangeState(new PatrolState());
            }
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
        if (other.tag == "Edge")
        {
            enemy.target = null;
            enemy.ChangeDirection();
            enemy.ChangeState(new PatrolState());
        }
    }
}
