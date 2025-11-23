using System;
using UnityEngine;

namespace RinaBullet.Move.Modifiier {
    [Serializable]
    public class GravityModifier : AMoveModifier {
        
        [SerializeField]
        private float m_gravity = Physics.gravity.y;

        [SerializeField]
        private Vector3 m_direction = new Vector3(0, -1, 0);

        protected override Vector3 CalculateMovement() {
            return m_direction * m_gravity;
        }
    }
}