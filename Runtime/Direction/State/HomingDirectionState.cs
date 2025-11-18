using System;
using RinaBullet.Target;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using VContainer;

namespace RinaBullet.Direction.State {
    [Serializable]
    public class HomingDirectionState : ADirectionState {

        private ITargetProvider _targetProvider;

        private GameObject _target;
        
        [FormerlySerializedAs("verticalHoming")]
        [Title("誘導率")]

        [SerializeField]
        [Range(0f, 1f)]
        private float _verticalHoming;
        
        [FormerlySerializedAs("horizontalHoming")]
        [SerializeField]
        [Range(0f, 1f)]
        private float _horizontalHoming;

        [FormerlySerializedAs("targetUpdate")]
        [SerializeField]
        [InfoBox("ターゲットの変更を毎フレーム受け付けるか")]
        private bool _targetUpdate;

        protected override void OnInitialize() {

            _targetProvider = _resolver?.Resolve<ITargetProvider>() ?? _bullet.GetComponent<ITargetProvider>();
            
        }

        protected override void OnPreUpdate(float deltaTime) {
            if (_targetUpdate) {
                _targetProvider ??= _resolver.Resolve<ITargetProvider>();

                GetTarget();
            }

            _direction = CalculateDirection();
        }

        protected override void OnPostUpdate(float deltaTime) {

        }

        public override void Enter() {
            GetTarget();
        }

        public override void Exit() {

        }

        private void GetTarget () => _target = _targetProvider.Target.CurrentValue;

        private Vector3 CalculateDirection() {
            // Calculate the raw direction vector
            var rawDirection = (_target.transform.position - _bullet.transform.position).normalized;

            // Apply vertical and horizontal homing factors
            _direction = new Vector3(
                rawDirection.x * _horizontalHoming, // Horizontal adjustment
                rawDirection.y * _verticalHoming,   // Vertical adjustment
                rawDirection.z // Keep Z as is (if applicable, e.g., in 3D space)
            ).normalized;

            // Normalize the adjusted direction vector
            return _direction;
        }
    }
}