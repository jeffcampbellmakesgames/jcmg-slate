using JCMG.Slate;
using UnityEngine;

#if USE_NAUGHTY_ATTR
using NaughtyAttributes;
#endif

namespace JCMG_Slate_Demo
{
    /// <summary>
    /// An example reactive <see cref="UIScreenBase"/>
    /// </summary>
    public sealed class ExampleReactiveUIScreen : UIScreenBase
    {
        #if USE_NAUGHTY_ATTR
        [BoxGroup(RuntimeConstants.UI_REFS)]
        [Required]
        #endif
        [SerializeField]
        private RectTransform _mainContentRectTransform;

        #if USE_NAUGHTY_ATTR
        [BoxGroup(RuntimeConstants.DATA)]
        [Required]
        #endif
        [SerializeField]
        private AdjacentScreenTransition _enterScreenTransition;

        #if USE_NAUGHTY_ATTR
        [BoxGroup(RuntimeConstants.DATA)]
        [Required]
        #endif
        [SerializeField]
        private AdjacentScreenTransition _exitScreenTransition;

        protected override void Awake()
        {
            _enterScreenTransition.SetTargetRectTransform(_mainContentRectTransform);
            _exitScreenTransition.SetTargetRectTransform(_mainContentRectTransform);

            base.Awake();
        }
    }
}
