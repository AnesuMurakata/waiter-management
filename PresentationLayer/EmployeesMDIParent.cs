using System;
using System.Collections.Generic;
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
    public partial class EmployeesMDIParent : Form
    {
        private int childFormNumber = 0;
        //1a. ***Declare a reference to an EmployeeForm object
        private EmployeeForm employeeForm;
        //1b *** Declare a reference to a EmployeeListForm object
        private EmployeeListForm employeeListForm;
        //***Declare a reference to a shiftsForm object
        private ShiftsForm shiftsForm;
        //2. ***Declare a reference to an EmployeeController object
        private EmployeeController employeeController;

        public EmployeesMDIParent()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            employeeController = new EmployeeController();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }
        #region ToolstripMenus
        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }
        #endregion

        #region ToolstripMenus Employees & Shifts
        private void listAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (employeeListForm == null)
            {
                CreateNewEmployeeListForm();
            }
            if (employeeListForm.listFormClosed)
            {
                CreateNewEmployeeListForm();
            }
            employeeListForm.RoleValue = Role.RoleType.NoRole;
            employeeListForm.setUpEmployeeListView();
            employeeListForm.Show();
        }
        private void listHeadWaitersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (employeeListForm == null)
            {
                CreateNewEmployeeListForm();
            }
            if (employeeListForm.listFormClosed)
            {
                CreateNewEmployeeListForm();
            }
            employeeListForm.RoleValue = Role.RoleType.Headwaiter;
            employeeListForm.setUpEmployeeListView();
            employeeListForm.Show();
        }
        private void listWaitersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (employeeListForm == null)
            {
                CreateNewEmployeeListForm();
            }
            if (employeeListForm.listFormClosed)
            {
                CreateNewEmployeeListForm();
            }
            employeeListForm.RoleValue = Role.RoleType.Waiter;
            employeeListForm.setUpEmployeeListView();
            employeeListForm.Show();
        }
        private void listRunnersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (employeeListForm == null)
            {
                CreateNewEmployeeListForm();
            }
            if (employeeListForm.listFormClosed)
            {
                CreateNewEmployeeListForm();
            }
            employeeListForm.RoleValue = Role.RoleType.Runner;
            employeeListForm.setUpEmployeeListView();
            employeeListForm.Show();
        }
        private void addAnEmplyeeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (employeeForm == null)
            {
                CreateNewEmployeeForm();
            }
            if (employeeForm.employeeFormClosed)
            {
                CreateNewEmployeeForm();
            }
            employeeForm.RoleValue = Role.RoleType.NoRole;
            employeeForm.Show();
        }

        private void addWeeklyShiftsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (shiftsForm == null)
            {
                CreateNewShiftsForm();
            }
            if (shiftsForm.shiftsFormClosed)
            {
                CreateNewShiftsForm();
            }
            shiftsForm.Show();
        }
        #endregion

        #region Employee & Shift Child Forms
        private void CreateNewEmployeeForm()
        {
            employeeForm = new EmployeeForm(employeeController);
            employeeForm.MdiParent = this;        // Setting the MDI Parent
            employeeForm.StartPosition = FormStartPosition.CenterParent;
        }

        private void CreateNewEmployeeListForm()
        {
            employeeListForm = new EmployeeListForm(employeeController);
            employeeListForm.MdiParent = this;        // Setting the MDI Parent
            employeeListForm.StartPosition = FormStartPosition.CenterParent;
        }

        private void CreateNewShiftsForm()
        {
            shiftsForm = new ShiftsForm(employeeController);
            shiftsForm.MdiParent = this;        // Setting the MDI Parent
            shiftsForm.StartPosition = FormStartPosition.CenterParent;
        }
        #endregion

        private void EmployeesMDIParent_Load(object sender, EventArgs e)
        {

        }

     }
}
