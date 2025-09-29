using System;
using System.Collections.Generic;
using RinaBullet.Lifetime.Element;
using RinaBullet.Lifetime.Element.Interface;
using RinaBullet.Lifetime.Interface;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace RinaBullet.Lifetime
{
    public class BulletLifetime : SerializedMonoBehaviour, IBulletLifetimeManager
    {
        private List<IBulletLifetimeElement> m_elements = new()
        {
            new TimeLimitter(),
        };

        private IObjectResolver m_resolver;

        [Inject]
        public void Construct(IObjectResolver resolver)
        {
            m_resolver = resolver ?? throw new ArgumentNullException();
        }

        private void Start()
        {

        }

        private void RegisterOnDead()
        {
            m_elements.ForEach(x => x.IsDead += OnDead);
        }

        private void DisRegisterOnDead()
        {
            m_elements.ForEach(x => x.IsDead -= OnDead);
        }

        private void OnDead()
        {
            GameObject.Destroy(gameObject);
        }
    }
}