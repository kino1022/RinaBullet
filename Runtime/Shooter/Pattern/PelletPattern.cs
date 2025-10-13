using System;
using JetBrains.Annotations;
using RinaBullet.Symbol;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Random = UnityEngine.Random;

namespace RinaBullet.Shooter.Pattern {
    [CreateAssetMenu(menuName = "RinaBullet/ShootPattern/散弾")]
    public class PelletPattern : SerializedScriptableObject, IShootPattern {

        [SerializeField]
        [LabelText("同時発射数")] [ProgressBar(0, 30)]
        private int m_pelletAmount = 12;

        [SerializeField]
        [LabelText("弾幕散布界")] [ProgressBar(0.0f, 90.0f)]
        private float m_spreadAngle = 12.0f;

        public void Shoot([NotNull] Bullet prefab, [NotNull] IObjectResolver resolver, Vector3 pos, Quaternion rot) {
            if (prefab == null) throw new ArgumentNullException(nameof(prefab));
            if (resolver == null) throw new ArgumentNullException(nameof(resolver));

            for (int i = 0; i < m_pelletAmount; i++) {
                resolver.Instantiate(
                    prefab,
                    pos,
                    rot * CalculateRandomAngle()
                );
            }
        }

        private Quaternion CalculateRandomAngle() {
            float z = Random.Range(0.0f, 360.0f);
            float y = Random.Range(0.0f, m_spreadAngle / 2.0f);
            return Quaternion.Euler(0.0f, y, z);
        }
    }
}