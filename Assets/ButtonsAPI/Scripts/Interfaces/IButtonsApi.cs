using System.Collections.Generic;
using ButtonsAPI.Models;

namespace ButtonsAPI.Interfaces
{
    public interface IButtonsApi
    {
        void DeleteButton(int id);
        IButton EditButton(int id, ButtonRequest buttonData = null);
        IButton AddButton(ButtonRequest buttonData = null);
        List<IButton> GetAllButtons();
    }
}