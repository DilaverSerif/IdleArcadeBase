using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class MeleeWeapon : Weapon
{
    public float weaponAttackRange = 1;
    public float weaponAttackAngle = 90;
    
    public virtual bool CheckContact(Vector3 position)
    {
        return CheckAngle(position) && CheckDistance(position);
    }
    
    protected virtual bool CheckDistance(Vector3 position)
    {
        return Vector3.Distance(position, firePoint.position) < weaponAttackRange;
    }
    
    protected virtual bool CheckAngle(Vector3 position)
    {
        return Vector3.Angle(firePoint.forward, position - firePoint.position) < weaponAttackAngle;
    }

    void OnDrawGizmosSelected()
    {
        #if UNITY_EDITOR
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, Vector3.up, weaponAttackRange);
        var direction = transform.forward * weaponAttackRange;
        var rotatedDirection = Quaternion.AngleAxis(weaponAttackAngle, transform.up) * direction;
        var rotatedLine = transform.position + rotatedDirection;

        Handles.color = Color.white;
        Handles.DrawLine(transform.position, rotatedLine);
        rotatedDirection = Quaternion.AngleAxis(weaponAttackAngle, -1 * transform.up) * direction;
        rotatedLine = transform.position + rotatedDirection;
        Handles.DrawLine(transform.position, rotatedLine);
        
        Handles.color = Color.green;
        Handles.Label(transform.position + weaponAttackRange * transform.forward, "Range: " + weaponAttackRange);
#endif
        
        
    }

}