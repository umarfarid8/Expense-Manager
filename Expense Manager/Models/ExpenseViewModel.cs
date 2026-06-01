namespace Expense_Manager.Models
{
    public class ExpenseViewModel
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
