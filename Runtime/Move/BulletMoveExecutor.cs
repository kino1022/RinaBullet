using System;
using RinaBullet.Direction;
using RinaBullet.Speed;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using VContainer;

namespace RinaBullet.Move {
    [RequireComponent(typeof(Rigidbody))]
    public class BulletMoveExecutor : SerializedMonoBehaviour {

        [OdinSerialize]
        private IBulletSpeedHolder m_speed;

        [OdinSerialize]
        private IBulletDirectionHolder m_direction;
        
        [SerializeField]
        private Rigidbody m_rigidbody;
        
        [SerializeField]
        private Vector3 m_totalVelocity = Vector3.zero;
        
        private IObjectResolver m_resolver;

        [Inject]
        public void Construct(IObjectResolver resolver) {
            m_resolver = resolver ?? throw new ArgumentNullException();
        }

        private void Awake() {

            if (m_speed is null) {
                throw new NullReferenceException();
            }

            if (m_direction is null) {
                throw new NullReferenceException();
            }

            m_speed?.Initialize(m_resolver, gameObject);
            m_direction?.Initialize(m_resolver, gameObject);
        }

        private void Start() {
            m_speed?.Start();
            m_direction?.Start();

            m_rigidbody = transform.root.GetComponentInChildren<Rigidbody>();
        }
        
        public void FixedUpdate() {
            m_totalVelocity = m_direction.Direction.normalized * m_speed.Speed;
            
            m_rigidbody.linearVelocity = m_totalVelocity;
        }
    }
}