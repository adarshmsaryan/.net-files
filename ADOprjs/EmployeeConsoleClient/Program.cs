using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using EmployeeDAllib;
using EmployeeDALLib2;
namespace EmployeeConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            AdoDisconnected dal = new AdoDisconnected();
            do
            {
                int n;
                Console.WriteLine("enter your choice\n 1.Employee display\n 2.Add Employee\n 3.delete record throw employee id\n 4.update salary\n 5.display by id\n");
                n = int.Parse(Console.ReadLine());

                if (n == 1)
                {
                    DisplayAllEmps(dal);
                }
                else if (n == 2)
                {
                    AddEmployee(dal);
                }
                else if (n == 3)
                {
                    deleted(dal);
                }
                else if(n==4)
                {
                    salaryupdate(dal);

                }
                else
                {
                    displayid(dal);
                }
            } while (true);
           

        }

        private static void displayid(AdoDisconnected dal)
        {
            Console.WriteLine("enter emplyoee id");
            int ecode = int.Parse(Console.ReadLine());
            // int sal= int.Parse(Console.ReadLine());
            // dal.displaybyid(ecode))
            List<Employee> emplst = dal.displaybyid(ecode);
            foreach (Employee emp in emplst)
            {
                Console.WriteLine(emp.Ecode + "\t" + emp.Ename + "\t" + emp.Salary + "\t" + emp.Deptid);
            }

        }

        private static void salaryupdate(AdoDisconnected dal)
        {
            Console.WriteLine("enter ecode for update");
            int ecode = int.Parse(Console.ReadLine());
            Console.WriteLine("enter the new salary");
            int salary = int.Parse(Console.ReadLine());
            dal.updatesalary(ecode, salary);
            Console.WriteLine("salary updated.......");
        }

        private static void deleted(AdoDisconnected dal)
        {
            try
            {
                //Employee emp2 = new Employee();
                int n= int.Parse(Console.ReadLine());
                dal.DeleteEmployee(n);
                Console.WriteLine("record deleted........");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void AddEmployee(AdoDisconnected dal)
        {
            Employee emp1 = new Employee();
            emp1.Ecode = int.Parse(Console.ReadLine());
            emp1.Ename = Console.ReadLine();
            emp1.Salary = int.Parse(Console.ReadLine());
            emp1.Deptid = int.Parse(Console.ReadLine());
             dal.InsertEmployee(emp1);
            //dal.insertEmpUsingSP(emp1);
            Console.WriteLine("record inserted");
        }

        private static void DisplayAllEmps(AdoDisconnected dal)
        {
            List<Employee> emplst = dal.SelectAllEmps();
            foreach (Employee emp in emplst)
            {
                Console.WriteLine(emp.Ecode + "\t" + emp.Ename + "\t" + emp.Salary + "\t" + emp.Deptid);
            }
        }
      //  public static void  delete(Adoconnected dal)
       
        
    }
}
