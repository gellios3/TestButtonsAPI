namespace ButtonsAPI.Interfaces
{
    public interface IButtonView
    {
        int id { get; }

        void Setup(IButton button);

        void Destroy();
    }
}