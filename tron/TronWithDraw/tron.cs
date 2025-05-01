using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace BankXor
{
    internal class Bank
    {
        private static string dataFileName = "BankAccounts.txt";
        private static readonly string encryptionKey = "encryptionKey1234";
        private static Dictionary<string, BankAccount> bankAccounts = new Dictionary<string, BankAccount>();
        private static bool closeBank;

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            string dataDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(dataDirectory, dataFileName);
            LoadBankAccounts(filePath);
            while(!closeBank)
            {
                Console.WriteLine("Welcome to the Bank");
                Console.WriteLine("1. Log In");
                Console.WriteLine("2. Create Account");
                Console.WriteLine("3. Exit");
                string userInput = Console.ReadLine();
                switch(userInput)
                {
                    case "1":
                        LogIn(filePath);
                        break;
                    case "2":
                        CreateAccount(filePath);
                        break;
                    case "3":
                        closeBank = true;
                        break;
                    default:
                        Console.WriteLine("Invalid input. Please try again.");
                        break;
                }
            }
        }

        private static void LogIn(string filePath)
        {
            Console.Write("Enter your account name: ");
            string accountName = Console.ReadLine();
            Console.Write("Enter your password: ");
            string password = Console.ReadLine();
            if(bankAccounts.ContainsKey(accountName))
            {
                BankAccount account = bankAccounts[accountName];
                if(account.VerifyPassword(password))
                {
                    Console.WriteLine($"Welcome, {accountName}!");
                    DisplayAccountMenu(account, filePath);
                }
                else
                {
                    Console.WriteLine("Invalid password, Try again.");
                }
            }
            else
            {
                Console.WriteLine("Account not found.");
            }
        }

        private static void CreateAccount(string filePath)
        {
            Console.Write("Enter your account name: ");
            string accountName = Console.ReadLine();
            if(bankAccounts.ContainsKey(accountName))
            {
                Console.WriteLine("Account with this name already exists.");
                return;
            }
            Console.Write("Enter a password: ");
            string password = Console.ReadLine();
            BankAccount newAccount = new BankAccount(accountName, password, 0m);
            bankAccounts.Add(accountName, newAccount);
            SaveBankAccounts(filePath);
            Console.WriteLine("Account created successfully.");
        }

        private static void DisplayAccountMenu(BankAccount account, string filePath)
        {
            bool exitAccountMenu = false;
            while(!exitAccountMenu)
            {
                Console.WriteLine("1. View Balance");
                Console.WriteLine("2. Deposit");
                Console.WriteLine("3. Withdraw");
                Console.WriteLine("4. Log Out");
                string userInput = Console.ReadLine();
                switch(userInput)
                {
                    case "1":
                        Console.WriteLine("Balance: " + account.Balance.ToString("F2") + " €");
                        break;
                    case "2":
                        Deposit(account, filePath);
                        break;
                    case "3":
                        Withdraw(account, filePath);
                        break;
                    case "4":
                        exitAccountMenu = true;
                        break;
                    default:
                        Console.WriteLine("Invalid input. Please try again");
                        break;
                }
            }
        }

        private static void Deposit(BankAccount account, string filePath)
        {
            Console.Write("Enter the amount to deposit: ");
            if(decimal.TryParse(Console.ReadLine(), out decimal depositAmount))
            {
                account.Deposit(depositAmount);
                SaveBankAccounts(filePath);
                Console.WriteLine("Money deposited: " + depositAmount.ToString("F2") + " €");
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid amount.");
            }
        }

        private static void Withdraw(BankAccount account, string filePath)
        {
            Console.Write("Enter the amount to withdraw: ");
            if(decimal.TryParse(Console.ReadLine(), out decimal withdrawAmount))
            {
                if(account.WithDraw(withdrawAmount))
                {
                    SaveBankAccounts(filePath);
                    Console.WriteLine("Money withdrawn: " + withdrawAmount.ToString("F2") + " €");
                }
                else
                {
                    Console.WriteLine("Insufficient balance.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid amount.");
            }
        }

        private static void LoadBankAccounts(string filePath)
        {
            if(File.Exists(filePath))
            {
                try
                {
                    string[] lines = File.ReadAllLines(filePath);
                    foreach(var line in lines)
                    {
                        string decryptedLine = EncryptedData(line);
                        var accountData = decryptedLine.Split(',');
                        if(accountData.Length == 3)
                        {
                            string accountName = accountData[0];
                            string encryptedPassword = accountData[1];
                            decimal balance = decimal.Parse(accountData[2], CultureInfo.InvariantCulture);
                            string password = EncryptedData(encryptedPassword);
                            bankAccounts.Add(accountName, new BankAccount(accountName, password, balance));
                        }
                    }
                }
                catch(Exception error)
                {
                    Console.WriteLine("Error loading accounts: " + error.Message);
                    bankAccounts = new Dictionary<string, BankAccount>();
                }
            }
            else
            {
                bankAccounts = new Dictionary<string, BankAccount>();
            }
        }

        private static void SaveBankAccounts(string filePath)
        {
            try
            {
                List<string> lines = new List<string>();
                foreach(var account in bankAccounts.Values)
                {
                    string encryptedPassword = EncryptedData(account.Password);
                    string accountData = $"{account.AccountName},{encryptedPassword},{account.Balance.ToString(CultureInfo.InvariantCulture)}";
                    string encryptedLine = EncryptedData(accountData);
                    lines.Add(encryptedLine);
                }
                File.WriteAllLines(filePath, lines);
            }
            catch(Exception error)
            {
                Console.WriteLine("Error saving accounts: " + error.Message);
            }
        }

        public static string EncryptedData(string data)
        {
            string encryptedData = "";
            for (int i = 0; i < data.Length; i++)
            {
                encryptedData += (char)(data[i] ^ encryptionKey[i % encryptionKey.Length]);
            }
            return encryptedData;
        }
    }
}
