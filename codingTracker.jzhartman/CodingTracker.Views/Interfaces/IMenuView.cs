namespace CodingTracker.Views.Interfaces;
public interface IMenuView
{
    string PrintEntryViewOptionsAndGetSelection();
    string PrintGoalOptionsAndGetSelection();
    string PrintGoalTypesAndGetSelection();
    string PrintMainMenuAndGetSelection();
    string PrintTrackingMenuAndGetSelection();
    string PrintUpdateOrDeleteOptionsAndGetSelection();
}