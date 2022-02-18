using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GTransactions
{
    public enum AddTransactionResult
    {
        SUCCESS = 1,
        NOT_ENOUGH_BALANCE = 2,
    }

    public class Transaction
    {
        public Transaction()
        {
        }

        public string Username { get; set; } = "";
        public int TransactionID { get; set; } = 0;
        public string TransactionName { get; set; } = "";
        public string Sender { get; set; } = "";
        public string Receiver { get; set; } = "";
        public long TransactionCost { get; set; } = 0;
        public long TransactionFee { get; set; } = 0;
        public DateTime TransactionTime { get; set; } = DateTime.Now;
        public bool IsSuccess { get; set; } = true;
        public string TransactionStatus { get; set; } = "";

        public static string GetCsvHeader()
        {
            PropertyInfo[] properties = typeof(Transaction).GetProperties();

            StringBuilder linie = new StringBuilder();

            foreach (var f in properties)
            {
                if (IsByPassProperty(f))
                {
                    continue;
                }

                if (linie.Length > 0)
                    linie.Append(',');

                linie.Append(f.Name);
            }

            return linie.ToString();
        }

        private static bool IsByPassProperty(PropertyInfo f)
        {
            if (f.Name == nameof(Username) || f.Name == nameof(IsSuccess))
            {
                return true;
            }
            return false;
        }

        public string ToCsvRow()
        {
            PropertyInfo[] properties = typeof(Transaction).GetProperties();

            StringBuilder linie = new StringBuilder();

            foreach (var f in properties)
            {
                if (IsByPassProperty(f))
                {
                    continue;
                }

                if (linie.Length > 0)
                    linie.Append(',');

                var x = f.GetValue(this);

                //if (f.Name == nameof(Sender) || f.Name == nameof(Receiver))
                //{
                //    linie.Append('\t');
                //}

                if (x != null)
                    linie.Append(x.ToString());
            }

            return linie.ToString();
        }
    }
}
