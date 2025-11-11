using CodingTracker.Models.Entities;

namespace CodingTracker.Views.Interfaces;
public interface IConsoleOutputView
{
    void ActionCancelledMessage(string action);
    void ActionCompleteMessage(bool isSuccess, string state, string message);
    void ConfirmationMessage(string valueText);
    void ErrorMessage(string parameter, string message);
    void PrintCodingSessionListAsTable(List<CodingSessionDataRecord> sessions);
    void PrintReportDataAsTable(ReportModel report);
    void PrintCodingSessionToUpdateById(CodingSessionDataRecord session, int rowId);
    void WelcomeMessage();
    void PrintGoalListAsTable(List<GoalDTO> goals);
    void NoRecordsMessage(string recordType);
}