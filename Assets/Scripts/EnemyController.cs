using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Attackable
{
    public override void onAttack() {
        Destroy(this.gameObject);
    }
}
