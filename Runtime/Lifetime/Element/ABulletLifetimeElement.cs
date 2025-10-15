using System;
using RinaBullet.Lifetime.Element.Interface;
using UnityEngine;
using VContainer;

namespace RinaBullet.Lifetime.Element
{
    public abstract class ABulletLifetimeElement : IBulletLifetimeElement
    {

        protected IObjectResolver m_resolver;

        protected GameObject m_bullet;

        public Action IsDead { get; set; }

        public void Initialize(GameObject bullet,IObjectResolver resolver)
        {
            m_resolver = resolver ?? throw new ArgumentNullException();
            
            m_bullet = bullet ?? throw new ArgumentNullException();
        }

        public virtual void Start()
        {
            ResolveDependency();
        }

        public virtual void Dispose()
        {

        }

        /// <summary>
        /// オブジェクトの終了条件を満たした際に呼び出されるメソッド
        /// </summary>
        protected void OnDead()
        {

        }
        
        /// <summary>
        /// フィールドのIObjectResolverを利用して依存制の解決を行うメソッド
        /// </summary>
        protected virtual void ResolveDependency()
        {

        }
    }
}