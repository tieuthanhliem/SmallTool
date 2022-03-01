using System.Collections.Concurrent;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using VTPServer.Models;

namespace GTransactions
{
    


    public partial class Form1 : Form
    {
        static string _cons = @"Data Source=localhost;Initial Catalog=VTP;User ID=sa;Password=VTP@2021";
        private static readonly ConcurrentDictionary<string, AddTransactionResult> userUpdateBalancesResult = new ConcurrentDictionary<string, AddTransactionResult>();

        public Form1()
        {
            InitializeComponent();

            //SqlConnection cnn = new(connetionString);
            //cnn.Open();


            ////string sql = "SELECT Username, Balance FROM Account";
            //string username = "liem1";
            //long subtract = 7;
            ////string sql = $"UPDATE Account SET Balance = Balance - {subtract} WHERE Username = '{username}' AND  Balance >= {subtract}";
            //string sql = $"SELECT Balance FROM Account WhERE Username = '{username}'";

            //SqlCommand sqlCommand = new SqlCommand(sql, cnn);

            //SqlDataReader dataReader = sqlCommand.ExecuteReader();

            //string output = "";

            //List<object> list = new List<object>();

            //while (dataReader.Read())
            //{
            //    output = output + dataReader.GetValue(0) + " - " + dataReader.GetValue(1) + "\n";
            //    var username = dataReader.GetValue(0);
            //    var balance = dataReader.GetValue(1);
            //    list.Add(new object[] { username, balance });   


            //}

            //string username = "liem";
            //AddTransactionResult res;
            //while(true)
            //if (EnoughBalance(username))
            //{
            //    res = UpdateTrans(new Transaction("liem", "TransferMM", 10000, 200, DateTime.Now));
            //}



            //CreateTransactionTable("tu");
            //res = UpdateTrans(new Transaction("liem", "TransferMM", "0328069411", "0923818818", 10000, 200, DateTime.Now, "OK 0923818818 0923818818 0923818818 0923818818 0923818818 0923818818"));

            //string tablename = getTransTableName("tu");

            //DBHelper dbHelper = null;
            //try
            //{
            //    dbHelper = new DBHelper(_cons);

            //    var transactions = dbHelper.GetList<Transaction>($"SELECT * FROM {tablename}");
            //}
            //catch
            //{
            //}
            //finally
            //{
            //    dbHelper?.Close();
            //}


















            //try
            //{
            //    using (SqlConnection conn = new SqlConnection(_cons))
            //    {
            //        conn.Open();
            //        using (SqlCommand cmd = conn.CreateCommand())
            //        {
            //            cmd.CommandText = $"ALTER TABLE {tablename} ADD UUID nvarchar(50) NOT NULL DEFAULT ''";

            //            cmd.ExecuteNonQuery();
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{ }


            //TaskCompletionSource<string> source = new TaskCompletionSource<string>();


            //ThreadPool.QueueUserWorkItem(u => {
            //    Thread.Sleep(3000);
            //    source.SetResult(tablename);
            //});

            //string sres = GetSomething(source);

            //Console.WriteLine(sres);


            //Console.WriteLine(tablename);

        }

        private void statistic_Click(object sender, EventArgs e)
        {
            statistic.Enabled = false;

            List<string> listTableName = new List<string>();
            try
            {
                using (SqlConnection conn = new SqlConnection(_cons))
                {
                    conn.Open();
                    listTableName = ListTables(conn);
                    listTableName = listTableName.Where(x => x.EndsWith("_Transactions")).ToList();
                }
            }
            catch (Exception ex)
            { }


            // We have all table names now.
            // now we need to export all transactions to csv file
            // we do this on server
            // and then copy all csv file back to where we have excel install
            // to convert csv file to excel file

            List<Transaction> transactionSummary = new List<Transaction>();


            DateTime startTimeTransaction = DateTime.ParseExact("2022-01-31 23:59:59,999", "yyyy-MM-dd HH:mm:ss,fff",
                        System.Globalization.CultureInfo.InvariantCulture);

            DateTime endTimeTransaction = DateTime.ParseExact("2022-02-28 23:59:59,999", "yyyy-MM-dd HH:mm:ss,fff",
                    System.Globalization.CultureInfo.InvariantCulture);

            if (rbYesterday.Checked)
            {
                startTimeTransaction = DateTime.Today.AddDays(-1);
                endTimeTransaction = DateTime.Today;
            }
            else if (rbToday.Checked)
            {
                startTimeTransaction = DateTime.Today;
                endTimeTransaction = DateTime.Now;
            }
            else if (rbLastMonth.Checked)
            {
                var today = DateTime.Today;
                var month = new DateTime(today.Year, today.Month, 1);
                startTimeTransaction = month.AddMonths(-1);
                endTimeTransaction = month;
            }
            else if (rbThisMonth.Checked)
            {
                var now = DateTime.Now;
                startTimeTransaction = new DateTime(now.Year, now.Month, 1);
                endTimeTransaction = DateTime.Now;
            }
            else if (rbSelectday.Checked)
            {
                startTimeTransaction = monthCalendar1.SelectionRange.Start;
                endTimeTransaction = monthCalendar1.SelectionRange.End.AddDays(1);
            }

            Transaction sumTran = new Transaction();
            try
            {
                Dictionary<string, List<Transaction>> usersTransactions = new Dictionary<string, List<Transaction>>();
                List<string> transactionName = new List<string>();
                foreach (string tableName in listTableName)
                {
                    var trans = GetTransactions(tableName, startTimeTransaction, endTimeTransaction);
                    if (trans == null)
                    {
                        continue;
                    }
                    usersTransactions.Add(tableName, trans);
                    transactionName.AddRange(trans.Select(x=>x.TransactionName).ToList());
                    transactionName = transactionName.Distinct().ToList();

                    sumTran.Username = tableName;
                    sumTran.Sender = tableName;
                    sumTran.Receiver = tableName;

                    using (StreamWriter outputFile = new StreamWriter($"{tableName}.csv", false, Encoding.UTF8))
                    {
                        outputFile.WriteLine(Transaction.GetCsvHeader());

                        foreach (var tran in trans)
                        {
                            outputFile.WriteLine(tran.ToCsvRow());
                            sumTran.TransactionFee += tran.TransactionFee;
                            sumTran.TransactionCost += tran.TransactionCost;
                        }
                    }

                    transactionSummary.Add(sumTran);
                }

                using (StreamWriter outputFile = new StreamWriter($"ZZZ-Sumary-{startTimeTransaction:yyyy-MM-dd}--{endTimeTransaction.AddMilliseconds(-1):yyyy-MM-dd}.csv", false, Encoding.UTF8))
                {
                    outputFile.WriteLine(Transaction.GetCsvHeader());

                    foreach (var tran in transactionSummary)
                    {
                        outputFile.WriteLine(tran.ToCsvRow());
                    }
                }

                Dictionary<string,long> totalCategories = new Dictionary<string,long>();
                using (StreamWriter outputFile = new StreamWriter($"ZZZ-Sumary-detail-{startTimeTransaction:yyyy-MM-dd}--{endTimeTransaction.AddMilliseconds(-1):yyyy-MM-dd}.csv", false, Encoding.UTF8))
                {
                    string totalKey = "Total";
                    string firstLine = $",{totalKey}";
                    foreach(var name in transactionName)
                    {
                        firstLine += "," + name; 
                    }
                    outputFile.WriteLine(firstLine);

                    foreach (var utrans in usersTransactions)
                    {
                        string line = utrans.Key;
                        var sumFee = utrans.Value.Sum(x => x.TransactionFee);
                        line += "," + sumFee.ToString();

                        if (!totalCategories.ContainsKey(totalKey))
                        {
                            totalCategories.Add(totalKey, 0);
                        }
                        totalCategories[totalKey] += sumFee;

                        var trans = utrans.Value;

                        foreach (var name in transactionName)
                        {
                            long nameFee = 0;
                            foreach(var tran in trans)
                            {
                                if(tran.TransactionName == name)
                                {
                                    nameFee += tran.TransactionFee;
                                }
                            }
                            line += "," + nameFee.ToString();
                            if (!totalCategories.ContainsKey(name))
                            {
                                totalCategories.Add(name, 0);
                            }
                            totalCategories[name] += nameFee;
                        }

                        outputFile.WriteLine(line);
                    }
                    string totalLine = "Total,";
                    totalLine += totalCategories[totalKey].ToString();
                    foreach (var name in transactionName)
                    {
                        totalLine += "," + totalCategories[name].ToString();
                    }
                    outputFile.WriteLine(totalLine);
                }

                statistic.Enabled = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        internal List<Transaction> GetTransactions(string tablename, DateTime startTimeTransaction, DateTime endTimeTransaction)
        {
            DBHelper dbHelper = null;
            try
            {
                dbHelper = new DBHelper(_cons);

 

                SqlParameter[] sqlParameterArray = new SqlParameter[2]
                {
                    new SqlParameter("@StartTimeValue",  startTimeTransaction),
                    new SqlParameter("@LastTimeValue",  endTimeTransaction)
                };

                var transactions = dbHelper.GetList<Transaction>($"SELECT * FROM {tablename} WHERE TransactionTime BETWEEN @StartTimeValue AND @LastTimeValue", sqlParameterArray);
                return transactions;
            }
            catch
            {
            }
            finally
            {
                dbHelper?.Close();
            }
            return new List<Transaction>();
        }

        string GetSomething(TaskCompletionSource<string> source)
        {
            source.Task.Wait(10000);
            if (source.Task.IsCompleted)
            {
                return source.Task.Result;
            }
            else
            {
                return "";
            }
        }

        public List<string> ListTables(SqlConnection connection)
        {
            List<string> tables = new List<string>();
            DataTable dt = connection.GetSchema("Tables");
            foreach (DataRow row in dt.Rows)
            {
                string tablename = (string)row[2];
                tables.Add(tablename);
            }
            return tables;
        }








        public bool EnoughBalance(string username)
        {
            var res = userUpdateBalancesResult.GetOrAdd(username, _ => AddTransactionResult.NOT_ENOUGH_BALANCE);
            if (res == AddTransactionResult.SUCCESS)
            {
                return true;
            }
            else
            {
                int balance = 0;

                try
                {
                    balance = ExecuteSqlQueryBalance(username);
                }
                catch { }

                if (balance > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public AddTransactionResult UpdateTrans(Transaction tran)
        {
            AddTransaction(tran);
            string sql = $"UPDATE Account SET Balance = Balance - {tran.TransactionFee} WHERE Username = '{tran.Username}' AND  Balance > 0";
            int rowAffected = ExecuteSqlUpdateCmd(sql);

            AddTransactionResult res = AddTransactionResult.NOT_ENOUGH_BALANCE;
            if (rowAffected > 0)
            {
                res = AddTransactionResult.SUCCESS;
            }

            userUpdateBalancesResult.AddOrUpdate(tran.Username, res, (key, oldValue) => res);
            return res;
        }

        private string getTransTableName(string username)
        {
            return username + "_Transactions";
        }

        private void AddTransaction(Transaction tran)
        {
            string tableName = getTransTableName(tran.Username);
            if (!TableExists(tableName))
            {
                CreateTransactionTable(tableName);
            }

            try
            {
                using (var con = new SqlConnection(_cons))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand($"INSERT INTO {tableName} (TransactionName, Sender, Receiver, TransactionCost, TransactionFee, TransactionTime, TransactionStatus)" +
                        $" VALUES (" +
                        $"@TransactionName, " +
                        $"@Sender, " +
                        $"@Receiver, " +
                        $"@TransactionCost, " +
                        $"@TransactionFee, " +
                        $"@TransactionTime, " +
                        $"@TransactionStatus" +
                        $")", con))
                    {
                        cmd.Parameters.AddWithValue("@TransactionName", tran.TransactionName);
                        cmd.Parameters.AddWithValue("@Sender", tran.Sender);
                        cmd.Parameters.AddWithValue("@Receiver", tran.Receiver);
                        cmd.Parameters.AddWithValue("@TransactionCost", tran.TransactionCost);
                        cmd.Parameters.AddWithValue("@TransactionFee", tran.TransactionFee);
                        cmd.Parameters.AddWithValue("@TransactionTime", tran.TransactionTime);
                        cmd.Parameters.AddWithValue("@TransactionStatus", tran.TransactionStatus);

                        cmd.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
            }


        }

        public bool TableExists(string tableName)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                return false;
            }

            try
            {
                using (var con = new SqlConnection(_cons))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand($"IF EXISTS( " +
                    $"SELECT 1 FROM INFORMATION_SCHEMA.TABLES " +
                    $"WHERE TABLE_NAME = @tableName) " +
                    $"SELECT 1 ELSE SELECT 0", con))
                    {
                        cmd.Parameters.AddWithValue("@tableName", tableName);

                        int exists = (int)cmd.ExecuteScalar();
                        return exists == 1;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return false;
        }



        public bool CreateTransactionTable(string tableName)
        {
            string sql = $"CREATE TABLE {tableName} (" +
                $"TransactionID int IDENTITY(1, 1) NOT NULL," +
                $"TransactionName nvarchar(50) NOT NULL," +
                $"Sender nvarchar(50) NOT NULL," +
                $"Receiver nvarchar(50) NOT NULL," +
                $"TransactionCost int NOT NULL," +
                $"TransactionFee int NOT NULL," +
                $"TransactionTime datetime NOT NULL," +
                $"TransactionStatus nvarchar(max) NOT NULL" +
                $")";


            try
            {
                using (var cnn = new SqlConnection(_cons))
                {
                    cnn.Open();
                    SqlCommand sqlCommand = new SqlCommand(sql, cnn);

                    SqlDataReader dataReader = sqlCommand.ExecuteReader();
                }
            }
            catch
            {
            }
            finally
            {
            }

            return false;
        }



        public int ExecuteSqlUpdateCmd(string cmd)
        {
            int rowAffected = 0;
            SqlConnection cnn = null;
            try
            {
                cnn = new SqlConnection(_cons);
                cnn.Open();
                SqlCommand sqlCommand = new SqlCommand(cmd, cnn);

                SqlDataReader dataReader = sqlCommand.ExecuteReader();
                rowAffected = dataReader.RecordsAffected;
            }
            catch
            {
                rowAffected = -1;
            }
            finally
            {
                if (cnn != null)
                {
                    if (cnn.State == ConnectionState.Open)
                    {
                        cnn.Close();
                    }
                }
            }
            return rowAffected;
        }

        public int ExecuteSqlQueryBalance(string username)
        {
            int balance = 0;
            SqlConnection cnn = null;
            try
            {
                cnn = new SqlConnection(_cons);
                cnn.Open();

                string sql = $"SELECT Balance FROM Account WhERE Username = '{username}'";
                SqlCommand sqlCommand = new SqlCommand(sql, cnn);

                SqlDataReader dataReader = sqlCommand.ExecuteReader();
                while (dataReader.Read())
                {
                    balance = (int)dataReader.GetValue(0);
                    break;
                }
            }
            catch
            {
            }
            finally
            {
                if (cnn != null)
                {
                    if (cnn.State == ConnectionState.Open)
                    {
                        cnn.Close();
                    }
                }
            }
            return balance;
        }


    }
}