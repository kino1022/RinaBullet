using System;
using JetBrains.Annotations;
using RinaBullet.Shooter.Interface;
using RinaBullet.Shooter.Pattern;
using RinaBullet.Symbol;
using Sirenix.OdinInspector;
using VContainer;

namespace RinaBullet.Shooter {
    public class ShooterBehaviour : SerializedMonoBehaviour, IBulletShooter {
        
        private IObjectResolver m_resolver;

        [Inject]
        public void Construct(IObjectResolver resolver) {
            m_resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
        }
    
        [Button("射撃")]
        public void Shoot([NotNull] Bullet prefab, [NotNull] IShootPattern pattern) {
            if (prefab == null) throw new ArgumentNullException(nameof(prefab));
            if (pattern == null) throw new ArgumentNullException(nameof(pattern));
            
            pattern.Shoot(
                prefab,
                m_resolver,
                gameObject.transform.position,
                gameObject.transform.rotation
            );
        }
    }
}