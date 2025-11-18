using System;
using RinaBullet.Range.Interface;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RinaBullet.Direction.State.Condition {
    [Serializable]
    public class TargetDistanceCondition : AStateTransitionCondition {
        
        private IRangeRecorder _rangeRecorder;

        [SerializeField]
        [LabelText("遷移距離")]
        private float _range = 0.0f;

        protected override void OnInitialize() {
            
            base.OnInitialize();

            _rangeRecorder = _bullet.transform.root.GetComponentInChildren<IRangeRecorder>();
            
        }

        protected override bool EvaluateCondition() {
            if (_rangeRecorder == null) {
                Debug.LogWarning($"[RinaBullet] RangeRecorderが見つかりません。TargetDistanceConditionの条件評価に失敗しました。");
                return false;
            }
            
            return _rangeRecorder.Range.CurrentValue >= _range;
        }
    }
}