using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.IO;
using System.Net.NetworkInformation;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data.SQLite;

namespace OrderingRegistrator.Tools
{
    class StatusOrganizers : Form
    {

        // static Variables **
        private static string StatusKey = ""; // Status Message string
        public static string StatKey
        {

            get { return StatusKey; }
            set
            {
                StatusKey = value;
                //ApplyStatusBottom();
            }
        }

        // Interfacing Textbox, Picturebox and Labels **

        public static Label labStatOrderquant = new Label();
        public static Label labDB = new Label();
        public static Label labNet = new Label();
        public static Label labContent = new Label();
        public static Label labStatBottom = new Label();
        public static PictureBox pcbxPackImage = new PictureBox();
        public static PictureBox pcboxTick = new PictureBox();
        public static PictureBox pcboxNet = new PictureBox();
        public static PictureBox pcboxContent = new PictureBox();


        // Boolean Functions ***

        private static bool IsDBConnected() // არის თუ არა კავშირი მონაცემთა ბაზასთან
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(DataTools.getConnectionString))
                {
                    using (SQLiteCommand sqliteCommand = new SQLiteCommand("Select * From Currency", connection))
                    {
                        connection.Open();
                    }
                    connection.Close();
                }
                return true;
            }
            catch (SQLiteException error)
            {
                return false;
            }
        } // Check Database Connection
        private static bool CheckNetwork() // არის თუ არა კავშირი ინტერნეტთან
        {
            try
            {
                System.Net.IPHostEntry i = System.Net.Dns.GetHostEntry("www.google.ru");
                return true;
            }
            catch
            {
                return false;
            }

        } // Check Network Availability
        private static bool CheckContentLoad() // Check if Content Loaded
        {
            return IsDBConnected();
        }
        public static void NoDB() // თუ ვერ მოხერხდა დაკავშირება მონაცემთა ბაზასთან
        {
            pcboxTick.Image = Properties.Resources.GreenTicksmallRed;
            labDB.Text = "DataBase Connection Error";
            if (CheckNetwork())
            {
                pcboxNet.Image = Properties.Resources.GreenTicksmall;
                labNet.Text = "Network Connection Success";
            }
            else
            {
                pcboxNet.Image = Properties.Resources.GreenTicksmallRed;
                labNet.Text = "Network Connection Missing";
                DataTools.restoreCurrencies(); // Restore Currencies From Db
            }
            pcboxContent.Image = Properties.Resources.GreenTicksmallRed;
            labContent.Text = "Content Load Error";
        }
        public static bool dbConnected 
        {
            get { return IsDBConnected(); }
        }
        public static bool netAvailable
        {
            get { return CheckNetwork();}
        }


        // მარცხენა ქვედა მხარეს სტატუს - ფანჯრის ორგანიზატორი
        public static void ApplyStatusMain()
        {
            OrdersCountShow();
            if (IsDBConnected())
            {
                pcboxTick.Image = Properties.Resources.GreenTicksmall;
                labDB.Text = "DataBase Connection Success";
            }else  
            {
                pcboxTick.Image = Properties.Resources.GreenTicksmallRed;
                labDB.Text = "DataBase Connection Error";
            } if (CheckNetwork())
            {
                pcboxNet.Image = Properties.Resources.GreenTicksmall;
                labNet.Text = "Network Connection Success";
            }else           
            {
                pcboxNet.Image = Properties.Resources.GreenTicksmallRed;
                labNet.Text = "Network Connection Missing"; 
                DataTools.restoreCurrencies(); // Restore Currencies From Db - ვალუტის კურსის "წამოღება" მონაცემთა ბაზიდან

            } if (CheckContentLoad())
            {
                pcboxContent.Image = Properties.Resources.GreenTicksmall;
                labContent.Text = "Content Load Success";
            }else           
            {
                pcboxContent.Image = Properties.Resources.GreenTicksmallRed;
                labContent.Text = "Content Load Error";
            }
        }
        public static void OrdersCountShow() // გამოიტანოს მარჯვენა ქვედა კუთხეში მონაცემთა ბაზაში არსებული გამოწერილი ამანათების შესახებ
        {
              labStatOrderquant.Text = " ბაზაში არსებული ამანათების რაოდენობა შეადგენს: " + DataTools.getMaxRows + " გამოწერას ";
        }
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // StatusOrganizers
            // 
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Name = "StatusOrganizers";
            this.Load += new System.EventHandler(this.StatusOrganizers_Load);
            this.ResumeLayout(false);

        } // არ გამოიყენება ***
        private void StatusOrganizers_Load(object sender, EventArgs e)
        {

        }
    }
}
