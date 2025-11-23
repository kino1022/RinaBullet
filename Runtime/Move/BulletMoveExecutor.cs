using System;
using System.Collections.Generic;
using RinaBullet.Direction;
using RinaBullet.Move.Modifiier;
using RinaBullet.Speed;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using VContainer;

namespace RinaBullet.Move {
    [RequireComponent(typeof(Rigidbody))]
    public class BulletMoveExecutor : SerializedMonoBehaviour {
        
        [Title("参照")]

        [OdinSerialize]
        [LabelText("進行方向管理")]
        private IBulletDirectionHolder _direction;
        
        [OdinSerialize]
        [LabelText("速度管理")]
        private IBulletSpeedHolder _speed;

        [OdinSerialize]
        [LabelText("移動補正")]
        private List<IMoveModifier> _modifiers = new();
        
        [OdinSerialize]
        [LabelText("")]
        [ReadOnly]
        private Rigidbody _rigidbody;

        private void Awake() {
            
            Debug.Assert(_direction is null);
            
            Debug.Assert(_speed is null);
            
        }

        private void Start() {
            
            _rigidbody = gameObject.transform.root.GetComponentInChildren<Rigidbody>();
            
            _rigidbody ??= gameObject.AddComponent<Rigidbody>();
            
            _rigidbody.useGravity = false;
            
        }

        private void Update() {
            
            var movement = CalculateMovement();
            
            movement = ApplyModifier(movement);
            
            _rigidbody.linearVelocity = movement;
            
        }

        private Vector3 CalculateMovement() {
            
            var result = Vector3.zero;
            
            result += _direction.Direction.normalized * _speed.Speed;

            return result;
            
        }

        private Vector3 ApplyModifier(Vector3 movement) {
            var result = movement;
            
            if (_modifiers is null || _modifiers.Count is 0) {
                return result;
            }

            foreach (var modifier in _modifiers) {
                result += modifier.Movement;
            }
            
            return result;
        }

    }
}