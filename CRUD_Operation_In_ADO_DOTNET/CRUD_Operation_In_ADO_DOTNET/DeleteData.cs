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
    class DeleteData
    {
        SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ConnectionString.ToString());
        SqlCommand sqlCommand = null;
        public void ShowTableData()
        {
            Console.WriteLine("Employee Table Record are:");
            try
            {
                sqlConnection.Open();
                using (sqlCommand= new SqlCommand("Select * from Employees",sqlConnection))
                {
                    using(SqlDataReader sqlDataReader=  sqlCommand.ExecuteReader())
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        public void DeleteWithParameter()
        {
            Console.WriteLine("Delete Record With Parameter");
            try
            {
                
                sqlConnection.Open();
                Console.WriteLine("Enter Employee Id");
                int empid = Convert.ToInt32(Console.ReadLine());
                using(sqlCommand = new SqlCommand("Delete from Employees where EmpId=@empid",sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("EmpId", empid);
                    int i =sqlCommand.ExecuteNonQuery();
                    Console.WriteLine("One record Deleted ");
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

        public void DeleteWithoutParameter()
        {
            Console.WriteLine("Delete Record Without Parameter");
            try
            {
                
                sqlConnection.Open();
                
                Console.WriteLine("Enter Employee ID");
                int empId = Convert.ToInt32(Console.ReadLine());
                using (sqlCommand = new SqlCommand("Delete from Employees where EmpId='" + empId + "'", sqlConnection))
                {
                    int i = sqlCommand.ExecuteNonQuery();
                    Console.WriteLine("One REcord Deleted");
                    
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

        public void DeleteWithStoredProcedure()
        {
            Console.WriteLine("Delete Record With Stored Procedure");
            try
            {
                sqlConnection.Open();
                Console.WriteLine("Enter Employee Id");
                int empid = Convert.ToInt32(Console.ReadLine());
                using (sqlCommand = new SqlCommand("Sp_DeleteEmpRecord", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@eid", empid);
                    int i = sqlCommand.ExecuteNonQuery();
                    Console.WriteLine("One record Deleted ");
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
            DeleteData dd = new DeleteData();
            int option;
            Console.WriteLine("Check DML operations In ADO .NET\n");
            dd.ShowTableData();
            Console.WriteLine("--------------------------------------");
            do
            {

                Console.WriteLine("Enter option \n1, for Delete data with Parameter\n2. for Delete data without Parameter\n3. for Delete data with stored procedure\n4.for Exit");
                option = int.Parse(Console.ReadLine());
                switch (option)
                {
                    case 1: dd.DeleteWithParameter(); break;
                    case 2: dd.DeleteWithoutParameter(); break;
                    case 3: dd.DeleteWithStoredProcedure(); break;
                   
                    default: Console.WriteLine("wrong choice"); break;
                }
            } while (option > 1 && option <= 3);
        }
    }
}
