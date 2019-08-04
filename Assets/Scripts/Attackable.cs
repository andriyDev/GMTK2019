using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Attackable : MonoBehaviour
{
    public abstract void onAttack(GameObject attacker);
}
