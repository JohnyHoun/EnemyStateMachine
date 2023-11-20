using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenSlimeEnemy : Enemy
{
    //Start the game on the StartMoveState, that makes the player moves for some time on the opposite direction of the spawn to avoid the walls
    private void Start()
    {
        StateMachine.Initialize(StartMoveState);
    }
}
