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
    public partial class txtbxsellerInfo : Form
    {
        public txtbxsellerInfo()
        {
            InitializeComponent();
        }

        private void sellerInfo_Load(object sender, EventArgs e)
        {
            loadSellerInfo();
        }
        private void UpdateSellerInfo(int Index)
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

                        dRow[25] = txtbxsellerName.Text;
                        dRow[26] = txtbxsellerTrackService.Text;
                        dRow[27] = txtbxExtraInfo.Text;

                        SQLiteCommandBuilder commBuild;
                        commBuild = new SQLiteCommandBuilder(adapter);
                        adapter.UpdateCommand = commBuild.GetUpdateCommand(true);
                        adapter.Update(dSet);
                    }
                }
            }
        }

        private void loadSellerInfo()
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

                        txtbxsellerName.Text = dRow[25].ToString();
                        txtbxsellerTrackService.Text = dRow[26].ToString();
                        txtbxExtraInfo.Text = dRow[27].ToString();
                    }
                }

                connection.Close();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateSellerInfo(Form1.SelectedIndex);
            MessageBox.Show("Updated");
            this.Close();
        }
    }
}
