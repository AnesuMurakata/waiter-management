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
using INF2011S_Workshop8_WaS7_PIISOL.Employees;

namespace INF2011S_Workshop8_WaS7_PIISOL.PresentationLayer
{
    public partial class EmployeeListForm : Form
    {
        //Add form states 
        public enum FormStates
        {
            View = 0,
            Add = 1,
            Edit = 2,
            Delete = 3
        }
        public bool listFormClosed = false;
        private Role.RoleType roleValue;
        private EmployeeController employeeController;
        private Employee employee;
        private Collection<Employee> employees;
        private FormStates state;
             
        public EmployeeListForm(EmployeeController aController)
        {
            InitializeComponent();
            employeeController = aController;
            employees = employeeController.AllEmployees;
            //Set up Event Handlers for some form events in code rather than trhough the designer
            this.Load += EmployeeListForm_Load;
            this.Activated += EmployeeListForm_Activated;
            this.FormClosed += EmployeeListForm_FormClosed;
            state = FormStates.View;
        }
        public Role.RoleType RoleValue
        {
            set
            {
                roleValue = value;
            }
        }

        #region Form Events
        private void EmployeeListForm_Load(object sender, EventArgs e)
        {
            //employeeListView.View = View.Details;
            //setUpEmployeeListView();
        }
        private void EmployeeListForm_Activated(object sender, EventArgs e)
        {
            employeeListView.View = View.Details;
            setUpEmployeeListView();
            ShowAll(false, roleValue);
        }
        private void EmployeeListForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            listFormClosed = true;
        }
        #endregion

        #region ListView
        public void setUpEmployeeListView()
        {
            ListViewItem employeeDetails;
            HeadWaiter headW;
            Waiter waiter;
            Runner runner;
            //Clear current List View Control
            employeeListView.Clear();
            //Set Up Columns of List View
            employeeListView.Columns.Insert(0, "ID", 120, HorizontalAlignment.Left);
            employeeListView.Columns.Insert(1, "EMPID", 120, HorizontalAlignment.Left);
            employeeListView.Columns.Insert(2, "Name", 150, HorizontalAlignment.Left);
            employeeListView.Columns.Insert(3, "Phone", 100, HorizontalAlignment.Left);
            Collection<Employee> employees = null;  //employees collection will be filled by role
            switch (roleValue)                                 //Check which role
            {
                case Role.RoleType.NoRole:
                    // Get all the employees from the EmployeeController object (use the property) and
                    // assign to employees collection
                    employees = employeeController.AllEmployees;
                    listViewLabel.Text = "Listing of all employees";
                    employeeListView.Columns.Insert(4, "Payment", 100,
                                                                                  HorizontalAlignment.Center);
                    break;
                case Role.RoleType.Headwaiter:
                    //Add a FindByRole method to the EmployeeController 
                    employees = employeeController.FindByRole(employeeController.AllEmployees, Role.RoleType.Headwaiter);
                    listViewLabel.Text = "Listing of all Headwaiters";
                    //Set Up Columns of List View
                    employeeListView.Columns.Insert(4, "Salary", 100, HorizontalAlignment.Center);
                    break;
                //do for the others
                case Role.RoleType.Waiter:
                    //Add a FindByRole method to the EmployeeController 
                    employees = employeeController.FindByRole(employeeController.AllEmployees, Role.RoleType.Waiter);
                    listViewLabel.Text = "Listing of all Waiters";
                    //Set Up Columns of List View
                    employeeListView.Columns.Insert(4, "Rate", 100, HorizontalAlignment.Center);
                    employeeListView.Columns.Insert(5, "Number of Shifts", 100, HorizontalAlignment.Center);
                    break;
                case Role.RoleType.Runner:
                    //Add a FindByRole method to the EmployeeController 
                    employees = employeeController.FindByRole(employeeController.AllEmployees, Role.RoleType.Runner);
                    listViewLabel.Text = "Listing of all Runners";
                    //Set Up Columns of List View
                    employeeListView.Columns.Insert(4, "Rate", 100, HorizontalAlignment.Center);
                    employeeListView.Columns.Insert(5, "Number of Shifts", 100, HorizontalAlignment.Center);
                    break;
            }

            //Add employee details to each ListView item 
            foreach (Employee employee in employees)
            {
                employeeDetails = new ListViewItem();
                employeeDetails.Text = employee.ID.ToString();
                // Do the same for Gender, HomeLanguage, PopGroup and SA_Citizenship_Status
                employeeDetails.SubItems.Add(employee.EmpID);
                employeeDetails.SubItems.Add(employee.Name);
                employeeDetails.SubItems.Add(employee.Phone);
                switch (employee.role.RoleValue)
                {
                    case Role.RoleType.Headwaiter:
                        headW = (HeadWaiter)employee.role;
                        employeeDetails.SubItems.Add(headW.Salary.ToString());
                        break;
                    case Role.RoleType.Waiter:
                        waiter = (Waiter)employee.role;
                        employeeDetails.SubItems.Add(waiter.Rate.ToString());
                        employeeDetails.SubItems.Add(waiter.NumberOfShifts.ToString());
                        break;
                    case Role.RoleType.Runner:
                        runner = (Runner)employee.role;
                        employeeDetails.SubItems.Add(runner.Rate.ToString());
                        employeeDetails.SubItems.Add(runner.NumberOfShifts.ToString());
                        break;
                }
                employeeListView.Items.Add(employeeDetails);
            }
            employeeListView.Refresh();
            employeeListView.GridLines = true;
        }
        private void employeeListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowAll(true, roleValue);
            state = FormStates.View;
            ShowAll(false);
            if (employeeListView.SelectedItems.Count > 0)   // if you selected an item 
            {
                employee = employeeController.Find(employeeListView.SelectedItems[0].Text);  //selected student becoms current student
                                                                         // Show the details of the selected student in the controls
                PopulateTextBoxes(employee);
            }           
        }
        #endregion              

        #region Utility Methods
        private void ShowAll(bool value, Role.RoleType roleType)
        {
            idLabel.Visible = value;
            empIDLabel.Visible = value;
            nameLabel.Visible = value;
            phoneLabel.Visible = value;
            paymentLabel.Visible = value;
            idTextBox.Visible = value;
            empIDTextBox.Visible = value;
            nameTextBox.Visible = value;
            phoneTextBox.Visible = value;
            paymentTextBox.Visible = value;
            if (state == FormStates.Delete)
            {
                cancelButton.Visible = !value;
                submitButton.Visible = !value;
            }
            else
            { 
            cancelButton.Visible = value;
            submitButton.Visible = value;
             }         
            deleteButton.Visible = value;
            editButton.Visible = value;
            if ((roleType == Role.RoleType.Waiter) || (roleType == Role.RoleType.Runner) && value)
            {
                shiftsLabel.Visible = value;
                shiftsTextBox.Visible = value;
            }
            else
            {
                shiftsLabel.Visible = false;
                shiftsTextBox.Visible = false;
            }
        }
        private void ClearAll()
        {
            idTextBox.Text = "";
            empIDTextBox.Text = "";
            nameTextBox.Text = "";
            phoneTextBox.Text = "";
            paymentTextBox.Text = "";
            shiftsTextBox.Text = "";
        }
        private void ShowAll(bool value)
        {
            if ((state == FormStates.Edit) && value)
            {
                idTextBox.Enabled = !value;
                //do the same for all buttons & textboxes
                empIDTextBox.Enabled = !value;
            }
            else
            {
                idTextBox.Enabled = value;
                empIDTextBox.Enabled = value;
            }
            nameTextBox.Enabled = value;
            phoneTextBox.Enabled = value;
            paymentTextBox.Enabled = value;
            shiftsTextBox.Enabled = value;
            if (state == FormStates.Delete)
            {
                cancelButton.Visible = !value;
                submitButton.Visible = !value;
            }
            else
            {
                cancelButton.Visible = value;
                submitButton.Visible = value;
            }
        }
        #endregion

        #region Populate methods
        private void PopulateObject(Role.RoleType roleType)
        {
            HeadWaiter headW;
            Waiter waiter;
            Runner runner;
            employee = new Employee(roleType);
            employee.ID = idTextBox.Text;
            employee.EmpID = empIDTextBox.Text;
            employee.Name = nameTextBox.Text;
            employee.Phone = phoneTextBox.Text;

            switch (employee.role.RoleValue)
            {
                case Role.RoleType.Headwaiter:
                    headW = (HeadWaiter)(employee.role);
                    headW.Salary = decimal.Parse(paymentTextBox.Text);
                    break;
                case Role.RoleType.Waiter:
                    //***waiter to be done later for HW
                    waiter = (Waiter)(employee.role);
                    waiter.Rate = decimal.Parse(paymentTextBox.Text);
                    waiter.NumberOfShifts = int.Parse(shiftsTextBox.Text);
                    //  waiter.Tips = decimal.Parse(tipsTextBox.Text);
                    break;
                case Role.RoleType.Runner:
                    //***waiter to be done later for HW
                    runner = (Runner)(employee.role);
                    runner.Rate = decimal.Parse(paymentTextBox.Text);
                    runner.NumberOfShifts = int.Parse(shiftsTextBox.Text);
                    break;
            }
        }

        private void PopulateTextBoxes(Employee employee)
        {
            HeadWaiter headW;
            Waiter waiter;
            Runner runner;
            idTextBox.Text = employee.ID;
            empIDTextBox.Text = employee.EmpID;
            nameTextBox.Text = employee.Name;
            phoneTextBox.Text = employee.Phone;

            switch (employee.role.RoleValue)
            {
                case Role.RoleType.Headwaiter:
                    headW = (HeadWaiter)(employee.role);
                    paymentTextBox.Text = Convert.ToString(headW.Salary);
                    break;
                case Role.RoleType.Waiter:
                    //***waiter to be done later for HW
                    waiter = (Waiter)(employee.role);
                    paymentTextBox.Text = Convert.ToString(waiter.Rate);
                    shiftsTextBox.Text = Convert.ToString(waiter.NumberOfShifts);
                    break;
                case Role.RoleType.Runner:
                    //***waiter to be done later for HW
                    runner = (Runner)(employee.role);
                    paymentTextBox.Text = Convert.ToString(runner.Rate);
                    shiftsTextBox.Text = Convert.ToString(runner.NumberOfShifts);
                    break;
            }
        }
        #endregion

        #region Button Click Events
        private void editButton_Click(object sender, EventArgs e)
        {
            //set the form state to Edit
            state = FormStates.Edit;
            deleteButton.Visible = false;
            //call the EnableEntities method
            ShowAll(true);            
        }
        private void submitButton_Click(object sender, EventArgs e)
        {
            //The employee object is already populated in the  employeeListView_SelectedIndexChanged event
            // hence no need to populate now
            // if the add operation will be included on this form, it will become necessary to populate the employee
            if (state == FormStates.Edit)
            {
                PopulateObject(employee.role.RoleValue);
                employeeController.DataMaintenance(employee, DatabaseLayer.DB.DBOperation.Edit);
            }
            else
            {
                employeeController.DataMaintenance(employee, DatabaseLayer.DB.DBOperation.Delete);
            }
            employeeController.FinalizeChanges(employee);
            ClearAll();
            state = FormStates.View;          
            ShowAll(false, roleValue);
            setUpEmployeeListView();   //refresh List View
            employee = null;
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            //set the form state to Delete
            state = FormStates.Delete;
            editButton.Visible = false;
            //call the ShowAll method
            ShowAll(false);
            MessageBox.Show("This record is about to be deleted");
        }
        private void cancelButton_Click(object sender, EventArgs e)
        {
            ClearAll();
            state = FormStates.View;
            ShowAll(false, roleValue);
            setUpEmployeeListView();   //refresh List View
        }
        #endregion

    }
}
