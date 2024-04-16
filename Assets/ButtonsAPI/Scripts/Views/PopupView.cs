using System;
using ButtonsAPI.Enums;
using ButtonsAPI.Models;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ButtonsAPI.Views
{
    public class PopupView : MonoBehaviour
    {
        private const string deleteText = "Enter ID for Delete button";
        private const string editText = "Enter ID for Edit button";

        [SerializeField] private TMP_Text m_title;
        [SerializeField] private TMP_InputField m_inputField;
        [SerializeField] private TMP_InputField m_editNameField;
        [SerializeField] private Button m_doButton;

        public void Setup(RequestType type, Action<int, ButtonRequest> onDoAction)
        {
            gameObject.SetActive(true);

            m_doButton.onClick.AddListener(() =>
            {
                string id = m_inputField.text;
                if (string.IsNullOrEmpty(id))
                {
                    return;
                }

                ButtonRequest request = null;
                if (type == RequestType.Put)
                {
                    request = new ButtonRequest
                    {
                        text = m_editNameField.text
                    };
                }

                onDoAction?.Invoke(Convert.ToInt32(id), request);
                gameObject.SetActive(false);
            });

            switch (type)
            {
                case RequestType.Put:
                    m_editNameField.gameObject.SetActive(true);
                    m_title.text = editText;
                    break;
                case RequestType.Delete:
                    m_editNameField.gameObject.SetActive(false);
                    m_title.text = deleteText;
                    break;
            }
        }

        private void OnDisable()
        {
            m_doButton.onClick.RemoveAllListeners();
        }
    }
}