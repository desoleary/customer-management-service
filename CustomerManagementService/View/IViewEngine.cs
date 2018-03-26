namespace CustomerManagement.View
{
    public interface IViewEngine
    {
        string Render<T>(T objectToRender);
    }
}