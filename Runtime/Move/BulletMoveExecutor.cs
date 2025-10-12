using System;
using RinaBullet.Direction;
using RinaBullet.Speed;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using VContainer;

namespace RinaBullet.Move {
    public class BulletMoveExecutor : SerializedMonoBehaviour {

        [OdinSerialize]
        private IBulletSpeedHolder m_speed;

        [OdinSerialize]
        private IBulletDirectionHolder m_direction;
        
        [SerializeField]
        private Vector3 m_totalVelocity = Vector3.zero;
        
        private IObjectResolver m_resolver;

        [Inject]
        public void Construct(IObjectResolver resolver) {
            m_resolver = resolver ?? throw new ArgumentNullException();
        }

        private void Awake() {
            m_speed?.Initialize(m_resolver);
            m_direction?.Initialize(m_resolver);
        }

        private void Start() {
            m_speed?.Start();
            m_direction?.Start();
        }
        
        public void FixedUpdate() {
            m_totalVelocity = m_direction.Direction.normalized * m_speed.Speed;
        }
    }
}