using NUnit.Framework;
using SGBank.Data;
using SGBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.Tests
{
    [TestFixture]
    public class MapperTest
    {
        [TestCase("12345,Free Account,100,F", "12345", "Free Account", 100, AccountType.Free)]
        [TestCase("33333,Basic Account,500,B", "33333", "Basic Account", 500, AccountType.Basic)]
        [TestCase("66666,Premium Account,1000,P", "66666", "Premium Account", 1000, AccountType.Premium)]
        public void ToAccountMapperTest(string row, string accountNumber, string name, decimal balance, AccountType accountType)
        {
            Account account = new Account();

            account.AccountNumber = accountNumber;
            account.Name = name;
            account.Balance = balance;
            account.Type = accountType;
            
            Account result = AccountMapper.StringToAccount(row);

            Assert.AreEqual(result.AccountNumber, account.AccountNumber);
            Assert.AreEqual(result.Name, account.Name);
            Assert.AreEqual(result.Balance, account.Balance);
            Assert.AreEqual(result.Type, account.Type);
        }

        [TestCase("12345", "Free Account", 100, AccountType.Free, "12345,Free Account,100,F")]
        [TestCase("33333", "Basic Account", 500, AccountType.Basic, "33333,Basic Account,500,B")]
        [TestCase("66666", "Premium Account", 1000, AccountType.Premium, "66666,Premium Account,1000,P")]
        public void ToStringMapperTest(string accountNumber, string name, decimal balance, AccountType accountType, string row)
        {
            Account account = new Account();

            account.AccountNumber = accountNumber;
            account.Name = name;
            account.Balance = balance;
            account.Type = accountType;

            string result = AccountMapper.AccountToString(account);

            Assert.AreEqual(result, row);
        }
    }
}
