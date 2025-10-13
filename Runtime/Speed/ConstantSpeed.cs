using System;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace RinaBullet.Speed {
    [Serializable]
    [LabelText("等速")]
    public class ConstantSpeed : IBulletSpeedHolder {
        
        [SerializeField]
        [LabelText("速度")]
        private float m_speed;
        
        public float Speed => m_speed;

        public void Initialize(IObjectResolver resolver, GameObject bullet) {
            
        }

        public void Start() {
            if (m_speed < 0) {
                m_speed *= -1.0f;
            }
        }
    }
}