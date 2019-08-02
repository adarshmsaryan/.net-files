using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;//for ADO.net classes
using System.Data;
using System.Configuration;//for reading config file
namespace EmployeeDAllib
{
    public class Adoconnected
    {
        SqlConnection con;
        SqlCommand cmd;
        public Adoconnected()
        {
            //creat connectionn
            con = new SqlConnection();
            // con.ConnectionString = "Data Source=VDC01LTC4531;Initial Catalog=ValtechDB;User ID=sa;Password=welcome1@";
            //or 
            //read connection fron config file
            string constr = ConfigurationManager.ConnectionStrings["sqlconstr"].ConnectionString;
            con.ConnectionString = constr;
            cmd = new SqlCommand();
            cmd.Connection = con;
        }
        //select all employee
        public List<Employee> SelectAllEmps()
        {
            List<Employee> emplst = new List<Employee>();
            //configure command for select all statement
            cmd.CommandText = "select * from tbl_employee";
            cmd.CommandType = CommandType.Text;
            //open the connection
            con.Open();
            //execute the command
            SqlDataReader sdr = cmd.ExecuteReader();
            while(sdr.Read())
            {
                Employee emp = new Employee();
                emp.Ecode = (int)sdr[0];
                emp.Ename = sdr[1].ToString();
                emp.Salary = (int)sdr[2];
                emp.Deptid = (int)sdr[3];
                emplst.Add(emp);
            }
            sdr.Close();
            con.Close();
            return emplst;
        }
        //insert 
        public void InsertEmployee(Employee emp)
        {
            //configure command for insert statement\
            //insert into tbl_employee values(101,'ravi',1111,201)
            //cmd.CommandText = "insert into tbl_employee values(" +emp.Ecode ")";
            cmd.CommandText = "insert into tbl_employee values(@ec,@en,@sa,@di)";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@ec", emp.Ecode);
            cmd.Parameters.AddWithValue("@en", emp.Ename);
            cmd.Parameters.AddWithValue("@sa", emp.Salary);
            cmd.Parameters.AddWithValue("@di", emp.Deptid);
            //open connection
            con.Open();
            cmd.ExecuteNonQuery();
            //close connection
            con.Close();
        }
        public void DeleteEmployee(int emp)
        {
            try
            {
                cmd.CommandText = "delete from tbl_employee where Ecode=" +emp ;
                cmd.CommandType = CommandType.Text;
                // cmd.Parameters.AddWithValue("@ec", emp.Ecode);
                con.Open();
                int recordAffected = cmd.ExecuteNonQuery();
                if (recordAffected == 0)
                {
                    throw new Exception("ecode does not exit");
                }
            }
            catch(SqlException ex)
            {
                throw ex;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            con.Close();
            
        }
        public void updatesalary(int ecode, int salary)
        {
            try
            {
                cmd.CommandText = "update tbl_employee set Salary=@sal where Ecode=@ec";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ec", ecode);
                cmd.Parameters.AddWithValue("@sal", salary);
                con.Open();
                int recordeffected = cmd.ExecuteNonQuery();
                if (recordeffected == 0)
                {
                    throw new Exception("ecode not exist");
                }
            }
            catch (SqlException ex)
            {
                throw ex;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            con.Close();
        }
        public List<Employee> displaybyid(int ecode)
        {
               List<Employee> emplst = new List<Employee>();
            
                cmd.CommandText = "select * from tbl_employee where Ecode=@ec";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ec", ecode);
            
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
            Employee emp = new Employee();
            if (sdr.Read())
               {
                
                emp.Ecode = (int)sdr[0];
                emp.Ename = (string)sdr[1];
                emp.Salary = (int)sdr[2];
                emp.Deptid = (int)sdr[3];
                emplst.Add(emp);
               }
           
            sdr.Close();  
              
            con.Close();
            return emplst;
                
                 
        }
        //calling stored procedure
        public void insertEmpUsingSP(Employee emp)
        {
            try
            {
                cmd.CommandText = "sp_insert_emp";
                cmd.CommandType = CommandType.StoredProcedure;//mandatory
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ec", emp.Ecode);
                cmd.Parameters.AddWithValue("@en", emp.Ename);
                cmd.Parameters.AddWithValue("@sal", emp.Salary);
                cmd.Parameters.AddWithValue("@did", emp.Deptid);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        
            con.Close();
        }
    
       
}

    //entity
    public class Employee
    {
        public int Ecode { get; set; }
        public string Ename { get; set; }
        public int Salary { get; set; }
        public int Deptid { get; set; }
    }
}
