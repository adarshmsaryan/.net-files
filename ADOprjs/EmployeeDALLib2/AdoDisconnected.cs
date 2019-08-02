using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace EmployeeDALLib2
{
    public class AdoDisconnected
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataSet ds;

        public AdoDisconnected()
        {
            con = new SqlConnection();
            con.ConnectionString = "Data Source=VDC01LTC4531;Initial Catalog=ValtechDB;User ID=sa;Password=welcome1@";
            cmd = new SqlCommand();
            cmd.Connection = con;
           
            cmd.CommandText = "select * from tbl_employee";

            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds, "tbl_employee");
            //add constraints
            ds.Tables[0].Constraints.Add("pk1", ds.Tables[0].Columns[0], true);
                
         }
            public List<Employee> SelectAllEmps()
           {
            List<Employee> emplst = new List<Employee>();
            foreach(DataRow row in ds.Tables[0].Rows)
            {
                Employee emp = new Employee();
                emp.Ecode = (int)row[0];
                emp.Ename= row[1].ToString();
                emp.Salary= (int)row[2];
                emp.Deptid = (int)row[3];
                emplst.Add(emp);

            }
            return emplst;

            }
        public void InsertEmployee(Employee emp)
        {
            DataRow row = ds.Tables[0].NewRow();
            //supply the values
            row[0] = emp.Ecode;
            row[1] = emp.Ename;
            row[2] = emp.Salary;
            row[3] = emp.Deptid;
            //add this new row with data set tables Rows
            ds.Tables[0].Rows.Add(row);
            //save changes to database
            SqlCommandBuilder scb = new SqlCommandBuilder(da);
            da.Update(ds, "tbl_employee");
           

        }
        public void DeleteEmployee(int emp)
        {
            //find the rows to be deleted
            DataRow[] rows= ds.Tables[0].Select("Ecode=" + emp);
            //delete the row
            rows[0].Delete();
            if(rows.Length==0)
            {
                throw new Exception("id not found");
            }
            SqlCommandBuilder scb = new SqlCommandBuilder(da);
            da.Update(ds, "tbl_employee");

        }
        public void updatesalary(int ecode, int salary)
        {
            DataRow[] rows = ds.Tables[0].Select("Ecode=" + ecode);
            SqlCommandBuilder scb = new SqlCommandBuilder(da);
            rows[0][2] = salary;
            da.Update(ds, "tbl_employee");
        }
        public List<Employee> displaybyid(int ecode)
        {
            List<Employee> en = new List<Employee>();

            DataRow[] rows = ds.Tables[0].Select("Ecode=" + ecode);
            Employee em = new Employee();
           // emp.Ecode = (int)rows[0][0];
            em.Ecode = (int)rows[0][0];
            em.Ename = rows[0][1].ToString();
            em.Salary = (int)rows[0][2];
            em.Deptid = (int)rows[0][3];
            en.Add(em);
         
            
             return en;



        }
    }
    public class Employee
    {
        public int Ecode { get; set; }
        public string Ename { get; set; }
        public int Salary { get; set; }
        public int Deptid { get; set; }
    }
} 
