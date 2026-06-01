namespace Expense_Manager.Models
{
    public class DashboardViewModel
    {
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal NetSavings => TotalIncome - TotalExpense;
    }
}
