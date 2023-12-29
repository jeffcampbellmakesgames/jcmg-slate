using JCMG.Slate;
using UnityEngine.UIElements;

namespace JCMG_Slate_Demo
{
    public class ModeSelectionUIScreenDocument : UIScreenDocumentBase
    {
        private Button _backButton;

        protected override void Awake()
        {
            base.Awake();

            _backButton = _uiDocument.rootVisualElement.Q<Button>("BackButton");

            _backButton.clicked += OnBackButtonClicked;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            _backButton.clicked -= OnBackButtonClicked;
        }

        private void OnBackButtonClicked()
        {
            var startUIScreen = UIScreenControl.GetScreen<StartUIScreenDocument>();
            startUIScreen.Show();

            Hide();
        }
    }
}
