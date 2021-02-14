using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Operation_In_ADO_DOTNET
{
    class InsertData
    {
        SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ConnectionString.ToString());
        SqlCommand sqlCommand = null;

        public void ShowTableData()
        {
            Console.WriteLine("Employee Table Record are:");
            try
            {
                sqlConnection.Open();
                using(sqlCommand= new SqlCommand("select * from Employees",sqlConnection))
                {
                    using(SqlDataReader sqlDataReader= sqlCommand.ExecuteReader())
                    {
                        Console.WriteLine("EmpId\tEMpName\t\tEmpSalary\tDeptNo");
                        Console.WriteLine("-----------------------------------------------");
                        while (sqlDataReader.Read())
                        {
                            Console.WriteLine($"{sqlDataReader["EmpId"]}\t{sqlDataReader["EmpName"]}\t{sqlDataReader["Salary"]}\t\t{sqlDataReader["DeptNo"]}");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        public void InsertWithParameter()
        {
            Console.WriteLine("Insert Record With Parameter");
            try
            {
                sqlConnection.Open();
                Console.WriteLine("Enter Employee Name");
                var empName = Console.ReadLine();
                Console.WriteLine("Enter Employee Salary");
                var empSalary = Convert.ToSingle(Console.ReadLine());
                Console.WriteLine("Enter Employee departmet Id");
                var empDeptId = Convert.ToDouble(Console.ReadLine());
                using(sqlCommand= new SqlCommand("insert into Employees values(@empName,@empSalary,@empDeptId)",sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@empName",empName);
                    sqlCommand.Parameters.AddWithValue("@empSalary", empSalary);
                    sqlCommand.Parameters.AddWithValue("@empDeptId",empDeptId);
                    object i = sqlCommand.ExecuteNonQuery();
                    Console.WriteLine("One Record is addedd");
                    ShowTableData();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        public void InsertWithoutParameter()
        {
            Console.WriteLine("Insert Record Without Parameter");
            try
            {
                sqlConnection.Open();
                Console.WriteLine("Enter Employee Name");
                var empName = Console.ReadLine();
                Console.WriteLine("Enter Employee Salary");
                var empSalary = Convert.ToSingle(Console.ReadLine());
                Console.WriteLine("Enter Employee departmet Id");
                var empDeptId = Convert.ToDouble(Console.ReadLine());
                using (sqlCommand= new SqlCommand("insert into Employees values('"+empName+"','"+empSalary+"','"+empDeptId+"')",sqlConnection))
                {
                    int i = sqlCommand.ExecuteNonQuery();
                    Console.WriteLine("One Record inserted");
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        public void InsertWithStoredProcedure()
        {
            Console.WriteLine("Insert Record With stored Procedure");
            try
            {
                sqlConnection.Open();
                Console.WriteLine("Enter Employee Name");
                var empName = Console.ReadLine();
                Console.WriteLine("Enter Employee Salary");
                var empSalary = Convert.ToSingle(Console.ReadLine());
                Console.WriteLine("Enter Employee departmet Id");
                var empDeptId = Convert.ToDouble(Console.ReadLine());
                using (sqlCommand= new SqlCommand("Sp_InsertEmpRecord", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@eName",empName);
                    sqlCommand.Parameters.AddWithValue("@eSalary",empSalary);
                    sqlCommand.Parameters.AddWithValue("@deptno",empDeptId);

                    int i = sqlCommand.ExecuteNonQuery();
                    Console.WriteLine("One Record Inserted");
                       
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
                ShowTableData();
            }
        }
        static void Main(string[] args)
        {
            InsertData insertData = new InsertData();
            int option;
            Console.WriteLine("Check DML operations In ADO .NET\n");
            insertData.ShowTableData();
            Console.WriteLine("--------------------------------------");
            do
            {

                Console.WriteLine("Enter option \n1, for Insert data with Parameter\n2. for Insert data without Parameter\n3. for Insert data with stored procedure\n4.for Exit");
                option = int.Parse(Console.ReadLine());
                switch (option)
                {
                    case 1: insertData.InsertWithParameter(); break;
                    case 2: insertData.InsertWithoutParameter(); break;
                    case 3: insertData.InsertWithStoredProcedure(); break;

                    default: Console.WriteLine("wrong choice"); break;
                }
            } while (option > 1 && option <= 3);

           
        }
    }
}
