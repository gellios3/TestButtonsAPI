using UnityEngine;

namespace ButtonsAPI.Interfaces
{
    public interface IButton
    {
        int buttonId { get; }
        Color? buttonColor { get; }
        bool isAnimation { get; }
        string buttonText { get; }
    }
}