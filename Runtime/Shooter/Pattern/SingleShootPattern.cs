using System;
using RinaBullet.Symbol;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Random = UnityEngine.Random;

namespace RinaBullet.Shooter.Pattern {
    [CreateAssetMenu(menuName = "RinaBullet/ShootPattern/単発")]
    public class SingleShootPattern : SerializedScriptableObject, IShootPattern {

        private float m_randomizeAngle = 0.0f;

        public void Shoot(Bullet prefab, IObjectResolver resolver, Vector3 pos, Quaternion rot) {

            if (prefab is null) {
                throw new ArgumentNullException();
            }

            if (resolver is null) {
                throw new ArgumentNullException();
            }

            resolver.Instantiate(
                prefab,
                pos,
                CalculateRandomAngle() * rot
                );
        }

        private Quaternion CalculateRandomAngle() {
            float z = Random.Range(0.0f, 360.0f);
            float y = Random.Range(0.0f, m_randomizeAngle / 2.0f);
            return Quaternion.Euler(0.0f, y, z);
        }
    }
}