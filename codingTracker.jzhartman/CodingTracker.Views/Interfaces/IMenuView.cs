namespace CodingTracker.Views.Interfaces;
public interface IMenuView
{
    string PrintEntryViewOptionsAndGetSelection();
    string PrintMainMenuAndGetSelection();
    string PrintTrackingMenuAndGetSelection();
    string PrintUpdateOrDeleteOptionsAndGetSelection();
}