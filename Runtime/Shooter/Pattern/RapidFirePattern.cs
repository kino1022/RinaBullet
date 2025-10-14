using System;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using RinaBullet.Symbol;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Random = UnityEngine.Random;

namespace RinaBullet.Shooter.Pattern {
    [CreateAssetMenu(menuName = "RinaBullet/ShootPattern/連射")]
    public class RapidFirePattern : SerializedScriptableObject, IShootPattern {

        [SerializeField]
        [LabelText("発射間隔")]
        private float m_interval = 0.1f;

        [SerializeField]
        [LabelText("発射数")]
        private int m_amount = 10;

        [SerializeField]
        [LabelText("散布界")]
        private float m_spreadAngle = 0.0f;

        public void Shoot([NotNull] Bullet prefab, [NotNull] IObjectResolver resolver, Vector3 pos, Quaternion rot) {
            if (prefab == null) throw new ArgumentNullException(nameof(prefab));
            if (resolver == null) throw new ArgumentNullException(nameof(resolver));
            RapidFire(prefab, resolver, pos, rot).Forget();
        }

        private async UniTask RapidFire(Bullet prefab, IObjectResolver resolver, Vector3 pos, Quaternion rot) {
            for (int i = 0; i < m_amount; ++i) {
                await UniTask.Delay(TimeSpan.FromSeconds(m_interval));
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