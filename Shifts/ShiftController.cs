using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workshop8_WaS7.Employees;

namespace Workshop8_WaS7.Shifts
{

    public class EmployeeTally
    {
        //declare two local variables.  One for the ID (string) and one for the count (integer)
        private string id;
        private int count;

        #region Properties
        public string ID
        { get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        public int shiftCount
        {
            get
            {
                return count;
            }
            set
            {
               count = value;
            }
        }
        #endregion
        public EmployeeTally(string idValue, int cnt)
        {
            //complete the assignment statements for this parameterized constructor
            // assign the parameters to the attributes of the class
            id = idValue;
            count = cnt;
        }
        // for now we are using parallel array, 
        // alternatively an array of structures could also have been a “smarter”  alternative 
    }
        public class ShiftController
    {
        //Phase I: Define a new delegate for the NoMoreShifts event-a delegate provides the signature of the event handler
        public delegate void NoMoreShiftsEventHandler(Employee sender);   //delegate for event
        //Phase I:   Declare a new event of this type - NoMoreShiftsEventHandler
        public event NoMoreShiftsEventHandler NoMoreShifts;
        //Phase II: Using a delegate to define the signature of another eventhandler 
        public delegate void OnShiftEventHandler(Employee sender, int shiftNumber);
        public event OnShiftEventHandler OnShift;

        private List<Shift> newShift;
        private EmployeeController employeeController;
        private List<EmployeeTally> employeesOnSchedule;
        public ShiftController(EmployeeController empController)
        {
            employeeController = empController;
            newShift = new List<Shift>();
        }

        #region ShiftController Methods
        public void NewShedule(System.DateTime startDate, System.DateTime endDate)
        {
            int count = 0;
            //instantiate a new list for the employees on a specific schedule
            employeesOnSchedule = new List<EmployeeTally>();
            System.DateTime aDate = startDate;

            // indices go from 0 to 13 -- but shift numbers go from 1 to 14
            for (count = 0; count <= 13; count++)
            {
                newShift.Add(new Shift());
                newShift[count].Date = aDate;
                newShift[count].ShiftDayEve = (Shift.ShiftType)(count % 2);
                newShift[count].Number = count + 1;
                //2 shifts per day 
                if (count % 2 == 1)
                {
                    aDate = aDate.AddDays(1);
                }
            }
        }
        public bool AddEmployeeToShift(int index, Employee emp)
        {
            bool addSuccessful = false;
            addSuccessful = newShift[index].Add2Shift(emp);
            //Phase 1, Step 1.5 - If an employee is added to the schedule increment his/her count 
            if (addSuccessful)
            {
                IncrementEmployeeTally(emp);
            }
            else
            {
                OnShift(emp, index + 1);
            }
            return addSuccessful;
        }

        //Phase 1, Step 1.4.2 - increment the shift count of an employee with more than one shift
        // or add the record if the employee(waiter role) has not been added before 
        private void IncrementEmployeeTally(Employee emp)
        {
            int index = FindIndex(emp);
            if (index >= 0)
            {
                employeesOnSchedule[index].shiftCount += 1;
                //Phase 1, Step 2.3 - If the employee(waitron) is already on 5 shifts he/she
                //should not be placed on more shifts
                if (employeesOnSchedule[index].shiftCount == 5)
                {
                    OnNoMoreShifts(emp);
                }
            }
            else
            {
                employeesOnSchedule.Add(new EmployeeTally(emp.ID, 1));
            }
        }


        //Phase II - when removing an employee from a shift their shift count should decrease
        private void DecrementShifts(Employee emp)
        {
            int index = 0;
            index = FindIndex(emp);
            employeesOnSchedule[index].shiftCount -= 1;
        }

        //Phase II -  remove an employee from a shift
        public void RemoveEmployeeFromShift(int index, Employee emp)
        {
            newShift[index].RemoveFromShift(emp);
            DecrementShifts(emp);
        }

        public int GetShiftCount(Employee employee)
        {
            int shiftCount = 0;
            int index = FindIndex(employee); // find the index of where the object with this employee's ID is in the empsOnSchedule list
            shiftCount = employeesOnSchedule[index].shiftCount;  // get this employee's shift count from the empsOnSchedule list at this index
            return shiftCount;
        }

        #endregion

        #region Virtual Event Methods

        protected virtual void OnNoMoreShifts(Employee emp)
        {
            // test if the event has been instantiated -- and a subscriber wants to listen to  this event?
            if (NoMoreShifts != null)
            {
                //Invokes the delegates.
                // if the test succeeds call the event handler method with the ”emp” object as parameter
                NoMoreShifts(emp);
            }            
        }

        protected virtual void EmpOnShift(Employee emp, int index4Shift)
        {
            //Phase II if employee is already on the shift 
            //***Check if subscriber (listener) will handle this event
            //Invoke the delegate.  The handler will know which employee and which shift it is.
            OnShift?.Invoke(emp, index4Shift);
        }
        #endregion

        #region Lookup methods
        //Phase 1, Step 1.4.1 - Find if the specific employee has already been placed on a shift 
        public int FindIndex(Employee employee)
        {
            int index = 0;
            bool found = false;
            int count = employeesOnSchedule.Count;

            while (!found && index < count)
            {
                found = (employeesOnSchedule[index].ID == employee.ID);
                if (!found)
                {
                    index++;
                }
            }
            if (found)
            {
                return index;
            }
            else
            {
                return -1;
            }
        }
        #endregion

    }
}
