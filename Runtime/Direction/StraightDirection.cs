using System;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace RinaBullet.Direction {
    [Serializable]
    [LabelText("直進")]
    public class StraightDirection : IBulletDirectionHolder {

        [SerializeField]
        [ReadOnly]
        private Vector3 m_direction;

        public Vector3 Direction => m_direction;

        public void Initialize(IObjectResolver resolver, GameObject bullet) {
            m_direction = bullet.transform.forward;
        }

        public void Start() {
            
        }
    }
}