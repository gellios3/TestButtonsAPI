using System.Collections.Generic;
using ButtonsAPI.Enums;
using ButtonsAPI.Interfaces;
using ButtonsAPI.Services;
using ButtonsAPI.Settings;
using ButtonsAPI.Views;
using UnityEngine;
using UnityEngine.UI;

namespace ButtonsAPI.Controllers
{
    public class ButtonsController : MonoBehaviour
    {
        [SerializeField] private ApiSettings m_apiSettings;
        [SerializeField] private ButtonView m_buttonPrefab;
        [Header("Buttons")] 
        [SerializeField] private Button m_addButton;
        [SerializeField] private Button m_editButton;
        [SerializeField] private Button m_updateButton;
        [SerializeField] private Button m_deleteButton;
        [Header("Popup")] 
        [SerializeField] private PopupView m_popup;

        private readonly List<IButtonView> _buttonViews = new List<IButtonView>();

        private List<IButton> _buttons;
        private IButtonsApi _service;

        private void Start()
        {
            _service = new ButtonsApiService(m_apiSettings.baseUrl);

            SubscribeOnHeaderButtons();
            ReloadAllButtons();
        }

        private void SubscribeOnHeaderButtons()
        {
            m_addButton.onClick.AddListener(OnAdd);
            m_editButton.onClick.AddListener(OnEdit);
            m_updateButton.onClick.AddListener(ReloadAllButtons);
            m_deleteButton.onClick.AddListener(OnDelete);
        }

        private void OnAdd()
        {
            IButton button = _service.AddButton();
            CreateButton(button);
        }

        private void OnEdit()
        {
            m_popup.Setup(RequestType.Put, (id, request) =>
            {
                IButton data = _service.EditButton(id, request);
                foreach (IButtonView buttonView in _buttonViews)
                {
                    if (buttonView.id == id)
                    {
                        buttonView.Setup(data);
                    }
                }
            });
        }

        private void OnDelete()
        {
            m_popup.Setup(RequestType.Delete, (id, _) =>
            {
                _service.DeleteButton(id);
                
                foreach (IButtonView buttonView in _buttonViews)
                {
                    if (buttonView.id != id)
                    {
                        continue;
                    }
                    
                    buttonView.Destroy();
                    _buttonViews.Remove(buttonView);
                    return;
                }
                
            });
        }

        private void ReloadAllButtons()
        {
            DeleteAllButtons();

            _buttons = _service.GetAllButtons();

            foreach (IButton button in _buttons)
            {
                CreateButton(button);
            }
        }

        private void DeleteAllButtons()
        {
            foreach (IButtonView view in _buttonViews)
            {
                view.Destroy();
            }

            _buttonViews.Clear();
        }

        private void CreateButton(IButton button)
        {
            ButtonView view = Instantiate(m_buttonPrefab, transform);
            view.Setup(button);
            _buttonViews.Add(view);
        }
    }
}