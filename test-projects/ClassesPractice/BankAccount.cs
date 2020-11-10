using System;
using System.Collections.Generic;

namespace ClassesPractice
{
    //Class : BankAccount
    //Used to create bank account object and handle functions for withdrawing/depositing/logging
    public class BankAccount
    {
        //Vars
        //Account seed so account numbers will always be different
        private static int accountNumberSeed = 1234567890;
        public string Number { get; }
        public string Owner { get; set; }
        
        //Get balance by adding up all the transactions
        public decimal Balance
        {
            get
            {
                decimal balance = 0;
                foreach (var item in allTransactions)
                {
                    balance += item.Amount;
                }

                return balance;
            }
        }
        //List to contain log of all transactions
        private List<Transaction> allTransactions = new List<Transaction>();

        //Constructor to create an account
        public BankAccount(string name, decimal initialBalance)
        {
            this.Owner = name;
            this.Number = accountNumberSeed.ToString();
            accountNumberSeed++; //increment string so next account num is different

            //Make initial deposit
            MakeDeposit(initialBalance, DateTime.Now, "Initial balance");
        }

        //Method : MakeDeposit
        //Used to create deposit transactions
        public void MakeDeposit(decimal amount, DateTime date, string note)
        {
            //Check if not negative
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount of deposit must be positive");
            }
            //create deposit transaction and add to list
            var deposit = new Transaction(amount, date, note);
            allTransactions.Add(deposit);
            Console.WriteLine("$" + amount + " Has been deposited at " + date + " with note: " + note);
        }

        //Method : MakeWithdrawal
        //Used to create withdrawl transactions
        public void MakeWithdrawal(decimal amount, DateTime date, string note)
        {   //check if not neg and have availible funds
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount of withdrawal must be positive");
            }
            if (Balance - amount < 0)
            {
                throw new InvalidOperationException("Not sufficient funds for this withdrawal");
            }

            //create object and add to list
            var withdrawal = new Transaction(-amount, date, note);
            allTransactions.Add(withdrawal);
            Console.WriteLine("$" + amount + " Has been withdrawn at " + date + " with note: " + note);
        }

        //Method : GetAccountHistory
        //Used to build output from transaction list
        public string GetAccountHistory()
        {
            var report = new System.Text.StringBuilder();

            decimal balance = 0;

            //Header line
            report.AppendLine("Date\t\tAmount\tBalance\tNote");

            //Add each list transaction to string
            foreach (var item in allTransactions)
            {
                balance += item.Amount;
                report.AppendLine($"{item.Date.ToShortDateString()}\t{item.Amount}\t{balance}\t{item.Notes}");
            }

            //return string
            return report.ToString();
        }
    }
}