using UnityEngine;
using System.Collections;
using JetBrains.Annotations;

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
        idleDuration = UnityEngine.Random.Range(1, 3);
        this.enemy = enemy;
    }

    public void Exit()
    {

    }

    public void OnTriggerEnter(Collider2D other)
    {
        if (other.tag == "Bullet")
        {


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
