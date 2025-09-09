namespace CodingTracker.Views.Interfaces
{
    public interface IMenuView
    {
        string RenderEntryViewOptionsAndGetSelection();
        string RenderMainMenuAndGetSelection();
        string RenderTrackingMenuAndGetSelection();
        string RenderUpdateOrDeleteOptionsAndGetSelection();
        string RenderUpdateTimeFeildSelector();
    }
}