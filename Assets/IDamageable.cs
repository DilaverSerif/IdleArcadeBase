
using System;
using UnityEngine;
public interface IDamageable
{
    void TakeDamage(HitData hitData);
    Transform transform { get; }
}