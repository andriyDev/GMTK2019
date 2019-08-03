using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Attackable
{
    public override void onAttack() {
        Destroy(this.gameObject);
    }
}
