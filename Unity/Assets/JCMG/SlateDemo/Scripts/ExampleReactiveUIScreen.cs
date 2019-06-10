using JCMG.Slate;
using NaughtyAttributes;
using UnityEngine;

namespace Demo
{
    /// <summary>
    /// An example reactive <see cref="UIScreen"/>
    /// </summary>
    public sealed class ExampleReactiveUIScreen : UIScreen
    {
        [BoxGroup(RuntimeConstants.UI_REFS)]
        [SerializeField, Required]
        private RectTransform _mainContentRectTransform;

        [BoxGroup(RuntimeConstants.DATA)]
        [SerializeField, Required]
        private AdjacentScreenTransition _enterScreenTransition;

        [BoxGroup(RuntimeConstants.DATA)]
        [SerializeField, Required]
        private AdjacentScreenTransition _exitScreenTransition;

        protected override void Awake()
        {
            _enterScreenTransition.SetTargetRectTransform(_mainContentRectTransform);
            _exitScreenTransition.SetTargetRectTransform(_mainContentRectTransform);

            base.Awake();
        }
    }
}
