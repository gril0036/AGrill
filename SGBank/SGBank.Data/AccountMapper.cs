using SGBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.Data
{
    public class AccountMapper
    {
        public static Account StringToAccount(string row)
        {
            Account a = new Account();
            string[] fields = row.Split(',');

            a.AccountNumber = fields[0];
            a.Name = fields[1];
            a.Balance = int.Parse(fields[2]);
            a.Type = AccountStringToTypeConverter(fields[3]);

            return a;
        }

        public static AccountType AccountStringToTypeConverter(string acctType)
        {
            switch (acctType.ToUpper())
            {
                case "F":
                    return AccountType.Free;
                case "B":
                    return AccountType.Basic;
                case "P":
                    return AccountType.Premium;
                default:
                    throw new Exception("Account type not found");
            }
        }

        public static string AccountToString(Account a)
        {
            string type = AccountTypeToStringConverter(a.Type);

            string row = $"{a.AccountNumber},{a.Name},{a.Balance},{type}";

            return row;
        }

        public static string AccountTypeToStringConverter(AccountType accountType)
        {
            switch (accountType)
            {
                case AccountType.Free:
                    return "F";
                case AccountType.Basic:
                    return "B";
                case AccountType.Premium:
                    return "P";
                default:
                    throw new Exception("Account type not found");
            }

        }
    }
}
