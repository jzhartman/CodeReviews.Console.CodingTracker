namespace CodingTracker.Views.Interfaces;
public interface IMenuView
{
    string PrintEntryViewOptionsAndGetSelection();
    string PrintGoalOptionsAndGetSelection();
    string PrintMainMenuAndGetSelection();
    string PrintTrackingMenuAndGetSelection();
    string PrintUpdateOrDeleteOptionsAndGetSelection();
}