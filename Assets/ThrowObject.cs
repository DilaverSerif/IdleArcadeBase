using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class ThrowObject : MonoBehaviour
{
    private Rigidbody rb;
    

    
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    public float angle;
    public async void Launch(LaunchData launchData)
    {
        var TargetObjectTF = launchData.targetPosition;

        while (Vector3.Distance(transform.position , TargetObjectTF) > 0.25f)
        {

            await UniTask.Yield();
            float targetYPos = TargetObjectTF.y;

            // Projektilin başlangıç pozisyonunu ve hedefin x ve z pozisyonlarını alın
            Vector3 projectilePos = transform.position;
            Vector3 targetXZPos = new Vector3(TargetObjectTF.x, 0.0f, TargetObjectTF.z);

            // Projektilin hedefe doğru bakmasını sağlayın
            transform.LookAt(targetXZPos);

            // Formülde kullanılan kısaltmaları hesaplayın
            float R = Vector3.Distance(projectilePos, targetXZPos);
            float G = Physics.gravity.y;
            float tanAlpha = Mathf.Tan(35 * Mathf.Deg2Rad);

            // Hedefin y pozisyonunu projektilin y pozisyonu olarak ayarlayın
            float H = targetYPos - projectilePos.y;

            // Projektilin hedefe ulaşması için gereken yerel uzay bileşenlerini hesaplayın
            float Vz = Mathf.Sqrt(G * R * R / (2.0f * (H - R * tanAlpha)));
            float Vy = tanAlpha * Vz;

            // Hesaplanan hız bileşenlerini yerel uzayda oluşturun ve global uzaya dönüştürün
            Vector3 localVelocity = new Vector3(0f, Vy, Vz);
            Vector3 globalVelocity = transform.TransformDirection(localVelocity);

            // Projektili başlatmak için başlangıç hızını ayarlayın ve durumunu değiştirin
            rb.velocity = globalVelocity;
        }
        
        Destroy(gameObject);

    }
}