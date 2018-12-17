using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrderingRegistrator
{
    public partial class customClearanse : Form
    {
        public customClearanse()
        {
            InitializeComponent();
        }

        private void customClearanse_Load(object sender, EventArgs e)
        {
            loadCCinfo();
        }

        private void loadCCinfo()
        {
            using (SQLiteConnection connection = new SQLiteConnection(Tools.DataTools.getConnectionString))
            {
                using (SQLiteCommand sqliteCommand = new SQLiteCommand("Select * From OrderedItems", connection))
                {
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqliteCommand))
                    {
                        connection.Open();
                        DataRow dRow;
                        DataSet dSet = new DataSet();
                        adapter.Fill(dSet);
                        dRow = dSet.Tables[0].Rows[Form1.SelectedIndex];

                        txtbxCCFee.Text = dRow[16].ToString();
                        txtbxCCDate.Text = dRow[17].ToString();
                    }
                }

                connection.Close();
            }
        }

        private void UpdateCCInfo()
        {
            using (SQLiteConnection connection = new SQLiteConnection(Tools.DataTools.getConnectionString))
            {
                using (SQLiteCommand sqliteCommand = new SQLiteCommand("Select * From OrderedItems", connection))
                {
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqliteCommand))
                    {
                        connection.Open();
                        DataRow dRow;
                        DataSet dSet = new DataSet();
                        adapter.Fill(dSet);
                        dRow = dSet.Tables[0].Rows[Form1.SelectedIndex];

                        dRow[16] = txtbxCCFee.Text;
                        dRow[17] = txtbxCCDate.Text;

                        SQLiteCommandBuilder commBuild;
                        commBuild = new SQLiteCommandBuilder(adapter);
                        adapter.UpdateCommand = commBuild.GetUpdateCommand(true);
                        adapter.Update(dSet);
                    }
                }
            }
        }

        private void btnUpdateCC_Click(object sender, EventArgs e)
        {
            UpdateCCInfo();
            MessageBox.Show("Updated");
            this.Close();
        }

    }
}
