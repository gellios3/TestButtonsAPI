using UnityEngine;

namespace ButtonsAPI.Settings
{
    [CreateAssetMenu(menuName = "ButtonsAPI/ApiSettings")]
    public class ApiSettings: ScriptableObject
    {
        [SerializeField] private string m_baseUrl = "https://661e612198427bbbef0460b4.mockapi.io/api/test/buttons";

        public string baseUrl => m_baseUrl;
    }
}