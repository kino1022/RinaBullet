using System;
using RinaBullet.Collision;
using RinaBullet.Range.Interface;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RinaBullet.Lifetime.Element {
    [Serializable]
    public class CollisionOther :ABulletLifetimeElement, ICollisionCallBackElement {

        [SerializeField]
        [LabelText("衝突時処理優先度")]
        private int m_priority = 10;

        public int Priority => m_priority;

        public override void Start() {
            base.Start();
            
            var callBackManager = m_bullet.transform.root.GetComponentInChildren<ICollisionCallBackManager>()
                ?? m_bullet.AddComponent<CollisionCallBackManager>();
            
            callBackManager.Add(this);
        }

        public void OnCollisionEnterCallBack(UnityEngine.Collision other) {
            IsDead?.Invoke();
        }
        
    }
}