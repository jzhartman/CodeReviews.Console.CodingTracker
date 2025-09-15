﻿using CodingTracker.Models.Entities;
using CodingTracker.Models.Validation;

namespace CodingTracker.Services.Interfaces;
public interface ICodingSessionDataService
{
    void AddSession(CodingSession session);
    void DeleteSessionById(int id);
    List<CodingSessionDataRecord> GetAllCodingSessions();
    List<CodingSessionDataRecord> GetSessionListByDateRange(DateTime startTime, DateTime endTime);
    CodingSessionDataRecord GetSessionById(int id);
    void UpdateSession(CodingSessionDataRecord session);
    ValidationResult<DateTime> ValidateEndTime(DateTime input, DateTime startTime);
    ValidationResult<DateTime> ValidateStartTime(DateTime input);
    ValidationResult<DateTime> ValidateUpdatedEndTime(CodingSessionDataRecord session, DateTime newStartTime, DateTime newEndTime);
    ValidationResult<DateTime> ValidateUpdatedStartTime(CodingSessionDataRecord session, DateTime updatedStartTime);
}