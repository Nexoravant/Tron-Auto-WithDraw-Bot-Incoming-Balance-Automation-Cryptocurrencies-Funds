using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BankXor
{
    public class BankAccount
    {
        public string AccountName { get; set; }
        public string Password { get; set; }
        public decimal Balance { get; private set; }

        public BankAccount(string accountName, string password, decimal balance)
        {
            AccountName = accountName;
            Password = password;
            Balance = balance;
        }

        public bool VerifyPassword(string password)
        {
            return Password == password;
        }

        public void Deposit(decimal amount)
        {
            Balance += amount;
        }

        public bool WithDraw(decimal amount)
        {
            if(amount <= Balance)
            {
                Balance -= amount;
                return true;
            }
            return false;
        }
    }
}
