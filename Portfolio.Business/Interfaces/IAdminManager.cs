using System;
using System.Collections.Generic;
using Portfolio.Business.ViewModels;
using Portfolio.Context.Models;

namespace Portfolio.Business.Interfaces
{
    public interface IAdminManager
    {
        List<TrafficLog> GetTrafficLogs(DateTime? startDate, DateTime? endDate);
        List<TrafficSummaryViewModel> GetTrafficSummary(string range, DateTime? startDate, DateTime? endDate);

        // Error log support
        List<ErrorLog> GetErrorLogs(DateTime? startDate, DateTime? endDate);
        List<TrafficSummaryViewModel> GetErrorSummary(DateTime? startDate, DateTime? endDate);
    }
}