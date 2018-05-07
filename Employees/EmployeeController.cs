using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INF2011S_Workshop8_WaS7_PIISOL.DatabaseLayer;

namespace INF2011S_Workshop8_WaS7_PIISOL.Employees
{
    public class EmployeeController
    {

        EmployeeDB employeeDB;
        Collection<Employee> employees;   //***W3

        #region Properties
        public Collection<Employee> AllEmployees
        {
            get
            {
                return employees;
            }
        }
        #endregion

        public EmployeeController()
        {
            //***instantiate the EmployeeDB object to communicate with the database
            employeeDB = new EmployeeDB();
            employees = employeeDB.AllEmployees;
        }

        #region Database Communication
        public void DataMaintenance(Employee anEmp, DB.DBOperation operation)
        {
            int index = 0;
            //perform a given database operation to the dataset in meory; 
            employeeDB.DataSetChange(anEmp, operation);
            //perform operations on the collection
            switch (operation)
            {
                case DB.DBOperation.Add:                                    
                    //*** Add the employee to the Collection
                    employees.Add(anEmp);
                    break;
                case DB.DBOperation.Edit:
                    index = FindIndex(anEmp);
                    employees[index] = anEmp;  // replace employee at this index with the updated employee
                    break;
                case DB.DBOperation.Delete:
                    index = FindIndex(anEmp);  // find the index of the specific employee in collection
                    employees.RemoveAt(index);  // remove that employee form the collection
                    break;
            }
        }

        //***Commit the changes to the database
        public bool FinalizeChanges(Employee employee)
        {
            //***call the EmployeeDB method that will commit the changes to the database
            return employeeDB.UpdateDataSource(employee);
        }
        #endregion

        #region Search Methods
        //This method  (function) searched through all the employess to finds onlly those with the required role
        public Collection<Employee> FindByRole(Collection<Employee> emps, Role.RoleType roleVal)
        {
            Collection<Employee> matches = new Collection<Employee>();

            foreach (Employee emp in emps)
            {
                if (emp.role.RoleValue == roleVal)
                {
                    matches.Add(emp);
                }
            }
            return matches;
        }

        public Collection<Employee> FindByRole(Role.RoleType roleVal)
        {
            Collection<Employee> matches = new Collection<Employee>();

            foreach (Employee emp in employees)
            {
                if (emp.role.RoleValue == roleVal)
                {
                    matches.Add(emp);
                }
            }
            return matches;
        }
        //This method receives a employee ID as a parameter; finds the employee object in the collection of employees and then returns this object
        public Employee Find(string ID)
        {
            int index = 0;
            bool found = (employees[index].ID == ID);  //check if it is the first student
            int count = employees.Count;
            while (!(found) && (index < employees.Count - 1))  //if not "this" student and you are not at the end of the list 
            {
                index = index + 1;
                found = (employees[index].ID == ID);   // this will be TRUE if found
            }
            return employees[index];  // this is the one!  
        }

        public int FindIndex(Employee anEmp)
        {
            int counter = 0;
            bool found = false;
            found = (anEmp.ID== employees[counter].ID);   //using a Boolean Expression to initialise found
            while (!(found) & counter < employees.Count - 1)
            {
                counter += 1;
                found = (anEmp.ID == employees[counter].ID);
            }
            if (found)
            {
                return counter;
            }
            else
            {
                return -1;
            }
        }
        #endregion
    }
}
