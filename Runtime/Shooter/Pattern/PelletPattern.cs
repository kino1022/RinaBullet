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
    public class PelletPattern : AShootPattern {

        [SerializeField]
        [LabelText("同時発射数")] [ProgressBar(0, 30)]
        private int m_pelletAmount = 12;

        public override void Shoot([NotNull] Bullet prefab, [NotNull] IObjectResolver resolver, Vector3 pos, Quaternion rot) {
            if (prefab == null) throw new ArgumentNullException(nameof(prefab));
            if (resolver == null) throw new ArgumentNullException(nameof(resolver));

            for (int i = 0; i < m_pelletAmount; i++) {
                resolver.Instantiate(
                    prefab,
                    pos,
                    rot * prefab.transform.rotation * CalculateSpread()
                );
            }
        }
    }
}