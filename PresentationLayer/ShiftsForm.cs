using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Workshop8_WaS7.Employees;
using Workshop8_WaS7.Shifts;

namespace Workshop8_WaS7.PresentationLayer
{
    public partial class
        ShiftsForm : Form
    {
        private BlockArray shiftBookings;
        private BlockArray shiftDates;
        private System.DateTime startDate;
        private System.DateTime endDate;
        private EmployeeController employeeController;
        private ShiftController shiftController;
        public bool shiftsFormClosed = false;
        private bool comboInitialised = false;
        public ShiftsForm(EmployeeController empController)
        {
            InitializeComponent();
            //the form will subscribe to its Load & the FormClosed events and to the DateSelected event of the shiftsMonthcalendar control
            //the name of the methods that will execute in each case is given on the right hand side of the "+=" symbol
            this.Load += ShiftsForm_Load;
            this.FormClosed += ShiftsForm_Closed;
            shiftsMonthCalendar.DateSelected += ShiftsMonthCalendar_DateSelected;
            employeeController = empController;
            shiftController = new ShiftController(employeeController);
            shiftController.NoMoreShifts += ShiftController_NoMoreShifts;
            shiftController.OnShift += ShiftController_OnShift;
        }
        
        #region Form Events
        private void ShiftsForm_Load(object sender, EventArgs e)
        {
            ShowControls(false);
            dayShiftsLabel.Text = Shift.ShiftType.Day.ToString();
            eveningShiftsLabel.Text = Shift.ShiftType.Evening.ToString();
            dayKeyLabel.BackColor = Color.LightGoldenrodYellow;
            eveningKeyLabel.BackColor = Color.Turquoise;
            calendarMessageLabel.BorderStyle = BorderStyle.None;
            calendarMessageLabel.ForeColor = Color.Red;
            calendarMessageLabel.Text = "Select a week to schedule shifts";
        }
        private void ShiftsForm_Closed(object sender, FormClosedEventArgs e)
        {
            shiftsFormClosed = true;
        }
        #endregion

        #region Utility  Methods
        private void ShowControls(bool value)
        {
            waitersComboBox.Visible = value;
            waitersLabel.Visible = value;
            dayShiftsLabel.Visible = value;
            eveningShiftsLabel.Visible = value;
            dayKeyLabel.Visible = value;
            eveningKeyLabel.Visible = value;
        }

        public void FillCombo()
        {
            Collection<Employee> waiters = null;
            //Find a collection of all the employees that are waiters
            // *** use the overloaded  find by role method that only needs one parameter
            waiters = employeeController.FindByRole(Role.RoleType.Waiter);
            //Link the objects in the collection of waitrons to every item of the combo box
            foreach (Employee employee in waiters)
            {
                waitersComboBox.Items.Add(employee);
            }
            //this is an alternative way of putting waiter object in the combo box
            //waitersComboBox.DataSource = waiters;
            //Set the current display of the combobox to show nothing
            waitersComboBox.SelectedIndex = -1;
            waitersComboBox.Text = "";
            comboInitialised = true;
        }

        #endregion

        #region Calendar Events
        private void ShiftsMonthCalendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            System.DateTime aDate = default(System.DateTime);  // Find the systems date
            //Calendar control functions to set the Start and End date
            startDate = shiftsMonthCalendar.SelectionRange.Start;
            endDate = shiftsMonthCalendar.SelectionRange.Start;
            //***Add control array, ShiftDates, to show the dates of the shifts – instantiate with parameters
            shiftDates = new BlockArray(this, 50, 100, 400, 1);  //these blocks will appear in a column
            aDate = startDate;
            int intcnt = 0;
            for (intcnt = 0; intcnt <= 6; intcnt++)
            {
                shiftDates.AddNewBlock();
                shiftDates.Item(intcnt).BackColor = Color.White;
                shiftDates.Item(intcnt).Text = aDate.ToShortDateString();    //Display the date on the Button
                aDate = aDate.AddDays(1);                             //**This function allows you to go to the NEXT day
            }
            //***Add a block for each slot on all the shifts (6 slots) – ShiftBookings control array
            shiftBookings = new BlockArray(this, 50, 100, 500, 6);

            for (intcnt = 0; intcnt <= 41; intcnt++)         // Why 41 blocks?   W7-Q1
            {
                // Add a new block for the shiftBooking  
                shiftBookings.AddNewBlock();
                // Afternoon slots should be turquoise (HINT:  6 blocks, use the remainder operator)
                if (intcnt % 6 > 2)
                {
                    shiftBookings.Item(intcnt).BackColor = Color.Turquoise;
                }
                //*** ONLY 4 slots on a SUNDAY (indices 2 or 5)– ie  2 slots per shift,  2 INACTIVE Slots
                // ***the colours of these blocks (identify the indices) will change to Color.DarkSlateGray
                if (intcnt == 2 || intcnt == 5)
                {
                    shiftBookings.Item(intcnt).BackColor = Color.DarkSlateGray;
                }
                else
                {
                    //A click event to be added dynamically to make button respond on user click 
                    //  subscribe to the eventhandler event for ALL slots except INACTIVE ones
                    shiftBookings.Item(intcnt).Click += SlotSelected;
                }
            }
            // Hide the calendar & the message below the calendar 
            shiftsMonthCalendar.Visible = false;
            calendarMessageLabel.Visible = false;
            // ***Call the method to fill the Combo box – the list of waiters becomes the datasource 
            FillCombo();
            //Call NewSchedule ShiftController method to create a ShiftBookings array in memory
            shiftController.NewShedule(startDate, endDate);
            //Show controls for ComboBox and labels
            ShowControls(true);
        }
        #endregion

        #region  Custom Events

        private void SlotSelected(System.Object sender, System.EventArgs e)
        {
            int whichShift;
            Employee employee = default(Employee);
            Button button = default(Button);

            //***The sender (control that was click-ed) is a button    
            button = (Button)sender;
            //Select an Employee (Waiter) from the combobox 
            employee = (Employee)waitersComboBox.SelectedItem;
            //Cannot book a slot if NO employee(waiter) is selected
            if (employee == null) // To DO:  test if the employee object is null)
            {
                //To DO:  display a message to the user to select a waiter 
                MessageBox.Show("First select a Waiter for the shift");
            }
            else
            {
                button.AccessibleName = employee.ID;
                //***Calculate on which shift to schedule the employee (depends on the button click and whether Day or Evening shift
                whichShift = (Convert.ToInt32(button.Tag) / 6) * 2 + (Convert.ToInt32(button.Tag) % 6) / 3;
                if (shiftController.AddEmployeeToShift(whichShift, employee))
                {   //…..colour block;  write name 
                    button.Text = employee.Name;
                    button.BackColor = Color.Red;
                    //***ONCE booked, the button cannot be selected again
                    //***Disable the button --- not to be clicked again	
                    button.Click += DeSelected;
                    button.Click -= SlotSelected;     //Form not listening to the click event method of the button
                }
            }
        }

               private void DeSelected(System.Object sender, System.EventArgs e)
        {
            Employee waiter = default(Employee);
            Button button = default(Button);
            int whichShift = 0;
            button = (Button)sender;

            System.Windows.Forms.DialogResult response = default(System.Windows.Forms.DialogResult);
            response = MessageBox.Show("Are you sure you want to de-select? ", "De-Select Shift", MessageBoxButtons.YesNo);

            //1.***Allow the manager to decide whether he really wants to change
            if (response == System.Windows.Forms.DialogResult.Yes)
            {
                //2.***Swap the Dynamic event handling routines
                button.Click -= DeSelected;
                button.Click += SlotSelected;
                //3.***Use the AccessibleNamefield of the button to store the employee ID
                waiter = employeeController.Find(button.AccessibleName);
                //4.***Add the waitron back to the combobox if it was already taken out
                if (shiftController.GetShiftCount(waiter) == 5)
                {
                    waitersComboBox.Items.Add(waiter);
                    eventsMessageLabel.Visible = false;
                }
                //5.***Find the right shift and remove the waitron from that shift
                whichShift = (Convert.ToInt32(button.Tag) / 6) * 2 + (Convert.ToInt32(button.Tag) % 6) / 3;
                shiftController.RemoveEmployeeFromShift(whichShift, waiter);
                //6***Reset the blocks to what it looked like BEFORE it was selected
                button.Text = "";
                button.AccessibleName = "";
                if (whichShift % 2 == 0)
                {
                    button.BackColor = Color.LightGoldenrodYellow;
                }
                else
                {
                    button.BackColor = Color.Turquoise;
                }
            }
        }

        private void ShiftController_NoMoreShifts(Employee sender)
        {
            eventsMessageLabel.Visible = true;     // Add a LABEL on the ShiftForm next to the ComboBox
            eventsMessageLabel.ForeColor = Color.Red;
            eventsMessageLabel.Text = "Waiter scheduled for 5 shifts - select another waiter";
            waitersComboBox.Items.Remove(sender);      //***Remove Employee from combo box if on 5 shifts
            //*** now you cannot see this waiter any longer
        }

        private void ShiftController_OnShift(Employee sender, int shiftNumber)
        {
            //Phase II
            //for now just a basic message!
            eventsMessageLabel.Visible = true;
            eventsMessageLabel.Text = "Waiter " + sender.Name + ", already on shift " + shiftNumber + " -  Please select another shift";
        }
        #endregion

        #region ComboBox Events
        private void waitersComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            eventsMessageLabel.Text = "";
        }
        #endregion

    }
}
