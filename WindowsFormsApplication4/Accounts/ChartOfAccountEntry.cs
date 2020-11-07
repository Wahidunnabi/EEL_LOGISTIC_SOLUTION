using System;
using System.Data;
using System.Drawing;
using LOGISTIC.BLL;
using System.Windows.Forms;


namespace LOGISTIC.UI
{
    public partial class ChartOfAccountEntry : Form
    {

        private static ChartOfAccount objCOA = new ChartOfAccount();

        private AccounceBLL objBll = new AccounceBLL();

        TreeNode _selectedNode = null;
        DataTable _acountsTb = null;      
        bool _newNode, _thisLevel, _update;
        int _parent = -1;
        int Id;
        int parentCode;
        public ChartOfAccountEntry()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.Manual;
            Location = new Point(0, 0);
            _newNode = _thisLevel = _update = false;
            _acountsTb = new DataTable();
          
        }

      

        private void ChartOfAccountEntry_Load(object sender, EventArgs e)
        {
            _acountsTb = objBll.GetAllChartOfAccount();
            PopulateTreeView(0, null);

        }
        

        private void PopulateTreeView(int parentId, TreeNode parentNode)
        {

            TreeNode childNode;

            foreach (DataRow dr in _acountsTb.Select("[parent]=" + parentId))
            {
                TreeNode t = new TreeNode();
                t.Text = dr["code"].ToString() + " - " + dr["ac_name"].ToString();
                t.Name = dr["code"].ToString();
                t.Tag = _acountsTb.Rows.IndexOf(dr);
                if (parentNode == null)
                {
                    treeView1.Nodes.Add(t);
                    childNode = t;
                }
                else
                {
                    parentNode.Nodes.Add(t);
                    childNode = t;
                }
                PopulateTreeView(Convert.ToInt32(dr["code"].ToString()), childNode);
            }
        }

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _selectedNode = treeView1.SelectedNode;
            ShowNodeData(_selectedNode);
        }

     

        private void ShowNodeData(TreeNode nod)
        {

            if (nod != null)
            {
                DataRow r = _acountsTb.Rows[int.Parse(nod.Tag.ToString())];
                Id = Convert.ToInt32(r["ID"]);
                parentCode = Convert.ToInt32(r["parent"]);
                txtCode.Text = Convert.ToString(r["parent"]);
                txtCode.Text = r["code"].ToString();
                txtName.Text = r["ac_name"].ToString();
                dtpDate.Value = Convert.ToDateTime(r["EntryDate"]);
                txtbalance.Text = r["open_bal"].ToString();

                if (r["type"].ToString().Equals("Parent Account"))
                {
                    radioParent.Checked = true;
                    txtbalance.Enabled = false;
                }
                else
                    radioTransaction.Checked = true;
                txtName.Focus();
                _update = true;
                btnSave.Text = "Update";
            }
            else
            { MessageBox.Show("Account has not Select", "Please Select Account", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void atThisLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _selectedNode = treeView1.SelectedNode;
            int max = 0;
            if (treeView1.Nodes.Count > 0)
            {
                _parent = int.Parse(_acountsTb.Rows[int.Parse(_selectedNode.Tag.ToString())]["parent"].ToString());
                DataRow[] nodes = _acountsTb.Select("[parent]=" + _parent);

                foreach (DataRow r in nodes)
                {
                    int n = int.Parse(r["code"].ToString());
                    if (n > max)
                        max = n;

                }
            }
            max += 1;
            txtCode.Text = max.ToString();

            _newNode = true;
            _thisLevel = true;
            txtName.Focus();

        }

        private void deletAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _selectedNode = treeView1.SelectedNode;
            if (_selectedNode.FirstNode != null)
            {
                MessageBox.Show("Parent account can't be deleted !!", "Acount deleting Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                DataRow r = _acountsTb.Rows[int.Parse(_selectedNode.Tag.ToString())];
               // r.Delete();
                string  id = r["ID"].ToString();
                var status = objBll.Delete(Convert.ToInt32(id));
                MessageBox.Show(status.ToString(), "Data Deletion Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _acountsTb = objBll.GetAllChartOfAccount();
                treeView1.Nodes.Clear();
                PopulateTreeView(0, null);
                ResetForm();
                //if (_selectedNode.Parent != null)
                //    _selectedNode.Parent.Nodes.Remove(_selectedNode);
                //else
                //    treeView1.Nodes.Remove(_selectedNode);
            }

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            _selectedNode = treeView1.SelectedNode;
            if (_selectedNode.Parent == null)
                atThisLevelToolStripMenuItem.Enabled = false;
            else
                atThisLevelToolStripMenuItem.Enabled = true;

        }

       

        private void underSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _selectedNode = treeView1.SelectedNode;
            
            DataRow r = _acountsTb.Rows[int.Parse(treeView1.SelectedNode.Tag.ToString())];
            if (r["type"].ToString().Equals("Parent Account"))
            {
                _newNode = true;
                _thisLevel = false;
                string code = string.Empty;
                _parent = int.Parse(_acountsTb.Rows[int.Parse(_selectedNode.Tag.ToString())]["code"].ToString());
                
                if (_selectedNode.Nodes.Count > 0)
                {

                    DataRow[] nodes = _acountsTb.Select("[parent]=" + _parent);
                    int max = 0;
                    foreach (DataRow ra in nodes)
                    {
                        int n = int.Parse(ra["code"].ToString());
                        if (n > max)
                            max = n;

                    }
                    max += 1;
                    txtCode.Text = max.ToString();
                    code = max.ToString();
                }
                else
                {
                    if (_selectedNode.Level < 3)
                        code = "01";
                    else
                        code = "001";

                    txtCode.Text = r["code"] + code;
                }
                txtName.Focus();

            }
            else
            {
                _newNode = false;
                MessageBox.Show("New Account can't be opened under a Transaction Account", "Acount opening Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

      

        //private void cmdSave_Click(object sender, EventArgs e)
        //{
        //    //string sql;
        //    if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrWhiteSpace(txtName.Text))
        //    {
        //        MessageBox.Show("Name Can not be empty", "Name", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //        txtName.Focus();
        //        return;
        //    }
        //    if (_update)
        //    {
        //        //sql = "UPDATE [accounts] SET [ac_name] = '" + txtName.Text + "',[type] = '" + (radioParent.Checked ? "Parent Account" : "Transaction Account") + "',[fixed] = '" + (radioFixed.Checked ? "Fixed" : (radioVariable.Checked ? "Variable" : "NA")) + "',[direct] = '" + (radioDirect.Checked ? "Direct" : (radioIndirect.Checked ? "Indirect" : "NA")) + "',[open_bal]=" + (radioParent.Checked ? "0" : textBox1.Text) + ",[dt]='" + dtpDate.Value.ToString("yyyy-MM-dd") + "',[active]=" + (isActive.Checked ? 1 : 0) + " WHERE [code] =" + txtCode.Text;
        //        //try
        //        //{
        //        //    _connection.Open();
        //        //    _command.CommandText = sql;
        //        //    _command.ExecuteNonQuery();
        //        //    MessageBox.Show("Data Saved Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

        //        //}
        //        //catch (SqlException ex)
        //        //{

        //        //    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        //}
        //        //finally
        //        //{
        //        //    _connection.Close();
        //        //}
        //        //_selectedNode.Text = txtCode.Text + " - " + txtName.Text;
        //        //DataRow r = _acountsTb.Rows[int.Parse(_selectedNode.Tag.ToString())];
        //        //r["ac_name"] = txtName.Text;
        //        //r["type"] = (radioParent.Checked ? "Parent Account" : "Transaction Account");
        //        //r["fixed"] = (radioFixed.Checked ? "Fixed" : (radioVariable.Checked ? "Variable" : "NA"));
        //        //r["direct"] = (radioDirect.Checked ? "Direct" : (radioIndirect.Checked ? "Indirect" : "NA"));

        //    }
        //    else if (_newNode)
        //    {
        //        //sql = "INSERT INTO [accounts]  VALUES (" + txtCode.Text + " ,'" + txtName.Text + "' ," + _parent + ",'" + (radioParent.Checked ? "Parent Account" : "Transaction Account") + "'," + (_thisLevel ? _selectedNode.Level : _selectedNode.Level + 1) + ",'" + (radioFixed.Checked ? "Fixed" : (radioNA1.Checked ? "Variable" : "NA")) + "','" + (radioDirect.Checked ? "Direct" : (radioNA2.Checked ? "Indirect" : "NA")) + "'," + txtbalance.Text + ",'" + dtpDate.Value.ToString("yyyy-MM-dd") + "','" + (isActive.Checked ? 1 : 0) + "')";
        //        //try
        //        //{
        //        //    _connection.Open();
        //        //    _command.CommandText = sql;
        //        //    _command.ExecuteNonQuery();
        //        //    MessageBox.Show("Data Saved Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

        //        //}
        //        //catch (SqlException ex)
        //        //{

        //        //    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        //}
        //        //finally
        //        //{
        //        //    _connection.Close();
        //        //}

        //        FillingData();
        //        var status = objBll.Insert(objCOA);
        //        MessageBox.Show(status.ToString(), "Data Insertion Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        _acountsTb = objBll.GetAllChartOfAccount();
        //        treeView1.Nodes.Clear();
        //        PopulateTreeView(0, null);
        //        ResetForm();
        //        //_acountsTb.Rows.Add(txtCode.Text, txtName.Text, _parent, (radioParent.Checked ? "Parent Account" : "Transaction Account"), _selectedNode.Level, (radioFixed.Checked ? "Fixed" : "Variable"), (radioDirect.Checked ? "Direct" : "Indirect"), txtbalance.Text, dtpDate.Value.ToString("yyyy-MM-dd"), (isActive.Checked ? 1 : 0));
        //        //TreeNode tn = new TreeNode();
        //        //tn.Text = txtCode.Text + " - " + txtName.Text;
        //        //tn.Name = txtCode.Text;
        //        //tn.Tag = _acountsTb.Rows.Count - 1;

        //        //if (_thisLevel)
        //        //{
        //        //    if (_selectedNode.Parent != null)
        //        //        _selectedNode.Parent.Nodes.Add(tn);
        //        //    else
        //        //        treeView1.Nodes.Add(tn);
        //        //}
        //        //else
        //        //    _selectedNode.Nodes.Add(tn);


        //    }
        //    else
        //    {
        //        MessageBox.Show("Nothing is selected", "Nothing", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //    }
        //}

        private void btnSave_Click(object sender, EventArgs e)
        {
            //string sql;
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Name Can not be empty", "Name", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtName.Focus();
                return;
            }
            if (_update)
            {

                FillingData();
                objCOA.ID = Id;
                objCOA.parent = parentCode;
                var status = objBll.Update(objCOA);
                MessageBox.Show(status.ToString(), "Data Update Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _acountsTb = objBll.GetAllChartOfAccount();
                treeView1.Nodes.Clear();
                PopulateTreeView(0, null);
                ResetForm();
                //sql = "UPDATE [accounts] SET [ac_name] = '" + txtName.Text + "',[type] = '" + (radioParent.Checked ? "Parent Account" : "Transaction Account") + "',[fixed] = '" + (radioFixed.Checked ? "Fixed" : (radioVariable.Checked ? "Variable" : "NA")) + "',[direct] = '" + (radioDirect.Checked ? "Direct" : (radioIndirect.Checked ? "Indirect" : "NA")) + "',[open_bal]=" + (radioParent.Checked ? "0" : textBox1.Text) + ",[dt]='" + dtpDate.Value.ToString("yyyy-MM-dd") + "',[active]=" + (isActive.Checked ? 1 : 0) + " WHERE [code] =" + txtCode.Text;
                //try
                //{
                //    _connection.Open();
                //    _command.CommandText = sql;
                //    _command.ExecuteNonQuery();
                //    MessageBox.Show("Data Saved Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //}
                //catch (SqlException ex)
                //{

                //    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
                //finally
                //{
                //    _connection.Close();
                //}
                //_selectedNode.Text = txtCode.Text + " - " + txtName.Text;
                //DataRow r = _acountsTb.Rows[int.Parse(_selectedNode.Tag.ToString())];
                //r["ac_name"] = txtName.Text;
                //r["type"] = (radioParent.Checked ? "Parent Account" : "Transaction Account");
                //r["fixed"] = (radioFixed.Checked ? "Fixed" : (radioVariable.Checked ? "Variable" : "NA"));
                //r["direct"] = (radioDirect.Checked ? "Direct" : (radioIndirect.Checked ? "Indirect" : "NA"));

            }
            else if (_newNode)
            {
                //sql = "INSERT INTO [accounts]  VALUES (" + txtCode.Text + " ,'" + txtName.Text + "' ," + _parent + ",'" + (radioParent.Checked ? "Parent Account" : "Transaction Account") + "'," + (_thisLevel ? _selectedNode.Level : _selectedNode.Level + 1) + ",'" + (radioFixed.Checked ? "Fixed" : (radioNA1.Checked ? "Variable" : "NA")) + "','" + (radioDirect.Checked ? "Direct" : (radioNA2.Checked ? "Indirect" : "NA")) + "'," + txtbalance.Text + ",'" + dtpDate.Value.ToString("yyyy-MM-dd") + "','" + (isActive.Checked ? 1 : 0) + "')";
                //try
                //{
                //    _connection.Open();
                //    _command.CommandText = sql;
                //    _command.ExecuteNonQuery();
                //    MessageBox.Show("Data Saved Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //}
                //catch (SqlException ex)
                //{

                //    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
                //finally
                //{
                //    _connection.Close();
                //}

                FillingData();
                var status = objBll.Insert(objCOA);
                MessageBox.Show(status.ToString(), "Data Insertion Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _acountsTb = objBll.GetAllChartOfAccount();
                treeView1.Nodes.Clear();
                PopulateTreeView(0, null);
                ResetForm();
                //_acountsTb.Rows.Add(txtCode.Text, txtName.Text, _parent, (radioParent.Checked ? "Parent Account" : "Transaction Account"), _selectedNode.Level, (radioFixed.Checked ? "Fixed" : "Variable"), (radioDirect.Checked ? "Direct" : "Indirect"), txtbalance.Text, dtpDate.Value.ToString("yyyy-MM-dd"), (isActive.Checked ? 1 : 0));
                //TreeNode tn = new TreeNode();
                //tn.Text = txtCode.Text + " - " + txtName.Text;
                //tn.Name = txtCode.Text;
                //tn.Tag = _acountsTb.Rows.Count - 1;

                //if (_thisLevel)
                //{
                //    if (_selectedNode.Parent != null)
                //        _selectedNode.Parent.Nodes.Add(tn);
                //    else
                //        treeView1.Nodes.Add(tn);
                //}
                //else
                //    _selectedNode.Nodes.Add(tn);


            }
            else
            {
                MessageBox.Show("Nothing is selected", "Nothing", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ResetForm();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FillingData()
        {
            try
            {
               
                objCOA.code = txtCode.Text.Trim();
                objCOA.ac_name = txtName.Text.Trim();
                objCOA.parent = _parent;
                objCOA.type = radioParent.Checked ? "Parent Account" : "Transaction Account";
                objCOA.levelno = _thisLevel ? _selectedNode.Level : _selectedNode.Level + 1;
                objCOA.open_bal = Convert.ToDecimal(txtbalance.Text.Trim());
                objCOA.EntryDate = DateTime.Now;
                objCOA.active = isActive.Checked ? 1 : 0;               
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        private void ResetForm()
        {
            _selectedNode = null;                            
            _parent = -1;
            txtCode.Text = "";
            txtName.Text = "";
            radioParent.Checked = true;
            isActive.Checked = false;
            dtpDate.Value = DateTime.Now;
            btnSave.Text = "Save";
        }

                                        
    }
}
