namespace Gameo.Services
{
    public interface IRevenueService
    {
        MonthlyRevenue ComputeMonthlyRevenue(string branchName, int year, int month);
    }
}