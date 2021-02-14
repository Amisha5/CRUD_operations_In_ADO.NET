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
    class UpdateData
    {
        SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ConnectionString.ToString());
        SqlCommand sqlCommand = null;

        public void ShowTableData()
        {
            Console.WriteLine("Employee Table Record are:");
            try
            {
                sqlConnection.Open();
                using (sqlCommand = new SqlCommand("Select * from Employees", sqlConnection))
                {
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
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

        public void UpdateWithParameter()
        {
            Console.WriteLine("Update Record With Parameter");
            try
            {
                sqlConnection.Open();
                Console.WriteLine("enter Employee Id");
                var empId = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter Employee Name");
                var empName = Console.ReadLine();
                Console.WriteLine("Enter Employee Salary");
                var salary = Convert.ToSingle(Console.ReadLine());
                Console.WriteLine("Enter Department No");
                var DeptNo = Convert.ToInt32(Console.ReadLine());
                using (sqlCommand = new SqlCommand("Update Employees set EmpName=@empName,DeptNo=@DeptNo,Salary= @salary where EmpId=@empId",sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("EmpId", empId);
                    sqlCommand.Parameters.AddWithValue("EmpName", empName);
                    sqlCommand.Parameters.AddWithValue("Salary", salary);
                    sqlCommand.Parameters.AddWithValue("DeptNo", DeptNo);

                    int result = sqlCommand.ExecuteNonQuery();
                    Console.WriteLine("One record updated");
                   
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

        public void UpdateWithoutParameter()
        {
            Console.WriteLine("Update Record Without Parameter");
            try
            {
                sqlConnection.Open();
                Console.WriteLine("enter Employee Id");
                int empId = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter Employee Name");
                string empName = Console.ReadLine();
                Console.WriteLine("Enter Employee Salary");
                double empSalary = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Enter Department No");
                int empDeptId = Convert.ToInt32(Console.ReadLine());
                using (sqlCommand= new SqlCommand("update Employees set EmpName='"+empName+"',Salary='"+empSalary+"',DeptNo='"+empDeptId+"' where EmpId='"+empId+"'",sqlConnection))
                {
                   int res= sqlCommand.ExecuteNonQuery();
                    Console.WriteLine("One Record Updated");
                    
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

        public void UpdateWithStoredProcedure()
        {
            Console.WriteLine("Insert Record With Stored Procedure");
            try
            {
                sqlConnection.Open();
                Console.WriteLine("enter Employee Id");
                var empId = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter Employee Name");
                var empName = Console.ReadLine();
                Console.WriteLine("Enter Employee Salary");
                var salary = Convert.ToSingle(Console.ReadLine());
                Console.WriteLine("Enter Department No");
                var DeptNo = Convert.ToInt32(Console.ReadLine());
                using (sqlCommand = new SqlCommand("Sp_UpdateEmpRecord", sqlConnection))
                {
                   
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@eid", empId);
                    sqlCommand.Parameters.AddWithValue("@eName", empName);
                    sqlCommand.Parameters.AddWithValue("@eSalary", salary);
                    sqlCommand.Parameters.AddWithValue("@deptno", DeptNo);


                    int result = sqlCommand.ExecuteNonQuery();
                    Console.WriteLine("One record updated");

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
            UpdateData updatedata = new UpdateData();

            int option;
            Console.WriteLine("Check DML operations In ADO .NET\n");
            updatedata.ShowTableData();
            Console.WriteLine("--------------------------------------");
            do
            {

                Console.WriteLine("Enter option \n1, for update data with Parameter\n2. for update data without Parameter\n3. for update data with stored procedure\n4.for Exit");
                option = int.Parse(Console.ReadLine());
                switch (option)
                {
                    case 1: updatedata.UpdateWithParameter(); break;
                    case 2: updatedata.UpdateWithoutParameter(); break;
                    case 3: updatedata.UpdateWithStoredProcedure(); break;

                    default: Console.WriteLine("wrong choice"); break;
                }
            } while (option > 1 && option <= 3);

            
        }
    }
}
