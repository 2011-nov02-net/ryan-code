using System;

namespace ClassesPractice
{
    //Class : Transaction
    //Used to create transaction objects to record withdrawls and deposits and info on each transaction
    public class Transaction
    {
        //Vars
        public decimal Amount { get; }
        public DateTime Date { get; }
        public string Notes { get; }

        //Constructor
        public Transaction(decimal amount, DateTime date, string note)
        {
            this.Amount = amount;
            this.Date = date;
            this.Notes = note;
        }
    }
}