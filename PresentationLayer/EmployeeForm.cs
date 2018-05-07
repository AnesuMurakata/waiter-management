using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//Including the correct namespaces
using INF2011S_Workshop8_WaS7_PIISOL.Employees;


namespace INF2011S_Workshop8_WaS7_PIISOL.PresentationLayer
{
    public partial class EmployeeForm : Form
    {
        public bool employeeFormClosed = false;
        private Employee employee;
        private EmployeeController employeeController;
        private Role.RoleType roleValue;
        public EmployeeForm(EmployeeController aController)
        {
            InitializeComponent();
            employeeController = aController;
            ShowAll(false, roleValue);
            // for this one I used the IDE to generate the event skeleton            
        }

        #region Form Events
        private void EmployeeForm_Load(object sender, EventArgs e)
        {
           ShowAll(false, roleValue);
        }
        private void EmployeeForm_Activated(object sender, EventArgs e)
        {
            ShowAll(false, roleValue);
        }

        private void Form_Closed(object sender, FormClosedEventArgs e)
        {
            employeeFormClosed = true;
        }
        #endregion
        public Role.RoleType RoleValue
        {
            set
            {
                roleValue = value;
            }
        }

        #region Button Click Events
        private void cancelButton_Click(object sender, EventArgs e)
        {
            ClearAll();
            ShowAll(false, roleValue);
        }
        private void exitButton_Click(object sender, EventArgs e)
        {
            employeeFormClosed = true;
            this.Close();
        }
        private void submitButton_Click(object sender, EventArgs e)
        {
            PopulateObject(roleValue);
            MessageBox.Show("To be submitted to the Database!");
            employeeController.DataMaintenance(employee, DatabaseLayer.DB.DBOperation.Add);
            employeeController.FinalizeChanges(employee);
            ClearAll();
            ShowAll(false, roleValue);
        }
        #endregion

        #region Radio Button CheckChanged Events
        private void headWaitronRadioButton_CheckedChanged(object sender, EventArgs e)
        {

            // Get the sender and cast as Type RadioButton
            RadioButton HeadWaitronRb = sender as RadioButton;

            // Check to see if the UI call was initiated by the radio button and not another event 
            // In this instance the InitializeComponent() triggers the the checked change event
            if (HeadWaitronRb != null)
            {
                /* If the call has been triggered by the radio button (i.e sender) , 
                then check if the radio button is checked and execute the appropriate code */
            if (HeadWaitronRb.Checked)
            {
                this.Text = "Add Head Waiter";
                roleValue = Role.RoleType.Headwaiter;
                paymentLabel.Text = "Salary";
                ShowAll(true, roleValue);
                idTextBox.Focus();
            }
            }
        }
        private void runnerRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            this.Text = "Add Runner";
            roleValue = Role.RoleType.Runner;
            paymentLabel.Text = "Rate";

            ShowAll(true, roleValue);
            idTextBox.Focus();
           }
        private void waitronRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            this.Text = "Add Waiter";
            roleValue = Role.RoleType.Waiter;
            paymentLabel.Text = "Rate";
            ShowAll(true, roleValue);
            idTextBox.Focus();
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
            submitButton.Visible = value;
            cancelButton.Visible = value;
            if (!(value))
            {
                headWaitronRadioButton.Checked = false;
                waitronRadioButton.Checked = false;
                runnerRadioButton.Checked = false;
            }
            if ((roleType == Role.RoleType.Waiter) || (roleType == Role.RoleType.Runner) && value)
            {
                tipsLabel.Visible = value;
                tipsTextBox.Visible = value;
                hoursLabel.Visible = value;
                hoursTextBox.Visible = value;
            }
            else
            {
                tipsLabel.Visible = false;
                tipsTextBox.Visible = false;
                hoursLabel.Visible = false;
                hoursTextBox.Visible = false;
            }
        }
        private void ClearAll()
        {
            idTextBox.Text = "";
            empIDTextBox.Text = "";
            nameTextBox.Text = "";
            phoneTextBox.Text = "";
            paymentTextBox.Text = "";
            hoursTextBox.Text = "";
            tipsTextBox.Text = "";
        }
        #endregion

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
                    waiter.NumberOfShifts = int.Parse(hoursTextBox.Text);
                    //  waiter.Tips = decimal.Parse(tipsTextBox.Text);
                    break;
                case Role.RoleType.Runner:
                    //***waiter to be done later for HW
                    runner = (Runner)(employee.role);
                    runner.Rate = decimal.Parse(paymentTextBox.Text);
                    runner.NumberOfShifts = int.Parse(hoursTextBox.Text);
                    break;
            }
        }
              
    }
}
