using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;

namespace OrderingRegistrator
{
    public partial class Form1 : Form
    {
        static int clicked = 0;

        public Form1()
        {
            InitializeComponent();
        }

        public static int SelectedIndex; // ცვლადი სარჩევში არჩეული სტრიქონის ინდექსისთვის
        private void Form1_Load(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("ru-RU"); // Double ცვლადების წარმოდგენისას შეცდომის აღმოფხვრა

                Task.Factory.StartNew(() =>
                {
                    InitializeObjects(); // ფუნქცია ქმნის ToolBox Ellement -ების ინიციალიზაციას, რომ შემდეგში მოხდეს მათი წვდომა სხვა კლასებიდან
                });

                syncLoad(); // ფუნქცია ასრულებს სინქრონიზაციას *

                // ასინქრონული void ფუნქცია - syncLoad() გამოიყენება დაყოვნებისთვის...
                // ეფექტი მიიღწევა ობიექტების და პროგრამის ვიზუალური მხარის ინიციალიზაციით და
                // შემდეგ ხდება ვალუტის კურსის განახლება , მონაცემთა ბაზიდან ინფორმაციის წამოღება და ასე შემდეგ...
                // პროგრამაში ფუნქცია "აჩქარებს" ჩატვირთვას და შემდეგ იწყებს "სინქრონიზაციას"::
                // 1. ვალუტის კურსის განახლება თუ ხდება ინტერნეტთან წვდომა
                // 2. მონაცემების წამოღება ბაზიდან
                // 3. პროგრამის მზადყოფნაში მოყვანა
        }

        private async void syncLoad()
        {
            disableAllButtons();
            await Task.Delay(300); // დაყოვნების დრო
            PreLoad(); // პუნქცია "მოსამზადებელი სამუშაოები" ***    
            enableAllButtons(); // გააქტიურდეს ყველა ღილაკი
        }
        private void PreLoad()
        {

            if (File.Exists(Tools.DataTools.getDBpath())) // შემოწმდეს - არსებობს , თუ არა მონაცემთა ბაზა
            {

                if (!Tools.StatusOrganizers.netAvailable) // შემოწმდეს - შესაძლებელია, თუ არა წვდომა ინტერნეტთან
                {
                    refreshCurrencyRatesToolStripMenuItem.Enabled = false; // თუ ინტერნეტთან წვდომა არ ხდება, მაშინ გაუქმდეს მენიუს ბრძანება: ვალუტის კურსის განახლება
                    Tools.DataTools.restoreCurrencies(); // ! ფუნქციის უარყოფის გამო , ბოლოჯერ განახლებული კურსი აისახება ზედა , ვალუტის TextBox-ში
                }
                else // თუ ინტერნეტთან კავშირი ხერხდება, მაშინ განახლდება ვალუტის კურსი **
                {
                    refreshCurrencyRatesToolStripMenuItem.Enabled = true;
                    Packages.CurrencyManager.UpdateToTextBox();
                    Tools.DataTools.updateCurrencies();
                }

                Tools.DataTools.loadOrdersList();
                Tools.StatusOrganizers.ApplyStatusMain();
                if (!chkbxCC.Checked) btnCCopen.Enabled = false;
                else btnCCopen.Enabled = true;
                txtbxTimetotall.Enabled = false;
                txtbxpricetotall.Enabled = false;
            }
            else // თუ ვერ მოიძებნა მონაცემთა ბაზა
            {
                MessageBox.Show("Check Database Existence...");
                ordersList.Items.Clear();
                Tools.StatusOrganizers.NoDB();
                if (Tools.StatusOrganizers.netAvailable) // Network Check
                {
                    refreshCurrencyRatesToolStripMenuItem.Enabled = true;
                    Packages.CurrencyManager.UpdateToTextBox();
                }
                else { refreshCurrencyRatesToolStripMenuItem.Enabled = true; }
                ordersList.Items.Add("No Records... Database Empty - შეამოწმეთ მონაცემთა ბაზის არსებობა, ან დაამატეთ ახალი ჩანაწერები...");

            }

            btnSave.Enabled = false;
            btnCurrent1.Enabled = false;
            btnCurrent2.Enabled = false;
            btnCurrent3.Enabled = false;
        } // "მოსამზადებელი სამუშაოები" ***   
        private void InitializeObjects()
        {
            Tools.DataTools.txtbxpckgName = txtbxpckgName;
            Tools.DataTools.txtbxpckgLink = txtbxpckgLink;
            Tools.DataTools.ordersList = ordersList;
            Tools.DataTools.txtbxvalGel = txtbxvalGel;
            Tools.DataTools.cmbxCurrency = cmbxCurrency;
            Tools.DataTools.txtbxOrdNum = txtbxOrdNum;
            Tools.DataTools.txtbxTrackNum = txtbxTrackNum;
            Tools.DataTools.txtbxTransComp = txtbxTransComp;
            Tools.DataTools.txtbxCount = txtbxCount;
            Tools.DataTools.txtbxOrdWght = txtbxOrdWght;
            Tools.DataTools.txtbxitPrice = txtbxitPrice;
            Tools.DataTools.txtbxShipFee = txtbxShipFee;
            Tools.DataTools.chkbxdelivAtOffice = chkbxdelivAtOffice;
            Tools.DataTools.chkbxPayed = chkbxPayed;
            Tools.DataTools.chkbxGotOut = chkbxGotOut;
            Tools.DataTools.txtbxordcreatDate = txtbxordcreatDate;
            Tools.DataTools.txtbxEstTime = txtbxEstTime;
            Tools.DataTools.txtbxArrAtbxOffice = txtbxArrAtbxOffice;
            Tools.DataTools.deliverAtGeo = deliverAtGeo;
            Tools.DataTools.chkbxCC = chkbxCC;
            Tools.DataTools.txtbxTimetotall = txtbxTimetotall;
            Tools.DataTools.txtbxpricetotall = txtbxpricetotall;
            Tools.DataTools.txtbxShowCurrencies = txtbxShowCurrencies;
            Tools.DataTools.txtbxBoF = txtbxBoF;
            Tools.DataTools.txtbxpricetotall = txtbxpricetotall;
            Tools.DataTools.txtbxTimetotall = txtbxTimetotall;
            Tools.DataTools.pcbxPackImage = pcbxPackImage;
            Tools.DataTools.txtbxImgPath = txtbxImgPath;
            Tools.StatusOrganizers.labDB = labDB;
            Tools.StatusOrganizers.labNet = labNet;
            Tools.StatusOrganizers.labContent = labContent;
            Tools.StatusOrganizers.labStatOrderquant = labStatOrderquant;
            Tools.StatusOrganizers.pcboxTick = pcboxTick;
            Tools.StatusOrganizers.pcboxNet = pcboxNet;
            Tools.StatusOrganizers.pcboxContent = pcboxContent;
            Tools.DateManager.txtbxordcreatDate = txtbxordcreatDate;
            Tools.DateManager.txtbxArrAtbxOffice = txtbxArrAtbxOffice;
            Tools.DateManager.deliverAtGeo = deliverAtGeo;
            Packages.CurrencyManager.txtbxShowCurrencies = txtbxShowCurrencies;
        }  // ხდება ინიციალიზაცია Private ობიექტებზე გარე კლასების, Public ფუნქციებით...
        private void ordersList_SelectedIndexChanged(object sender, EventArgs e) // გადავიდეს შემდეგ ამანათზე
        {  
            Tools.DataTools.populateOrders(ordersList.SelectedIndex); // გამოვიდეს შემდეგი ან წინა ამანათი პროგრამაში ფუნქციაში გადაცემული ინდექსის მიხედვით
            SelectedIndex = ordersList.SelectedIndex;
            backBlacktexts();
        }



        // * * * * * * * * * *      ფუნქციები     * * * * * * * * * * //
        private void importDB()
        {
            try
            {
                string datalink; // Database Source Dir in OpenFileDialog
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = "SQLite Database Files(*.sqlite)|*sqlite";
                dlg.Title = "Choose A Database To Load";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    DialogResult dialogResult = MessageBox.Show("გსურთ არჩეული მონაცემთა ბაზის იმპორტი?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (dialogResult == DialogResult.Yes)
                    {
                        datalink = dlg.FileName.ToString();
                        File.Copy(datalink, Tools.DataTools.getDBpath(), true);
                        Tools.DataTools.loadOrdersList();
                        MessageBox.Show("მონაცემთა ბაზის იმპორტი დასრულებულია...");
                    }
                    else if (dialogResult == DialogResult.No)
                    {

                    }
                }
            }
            catch (IOException error)
            {
                MessageBox.Show(error.Message);
                MessageBox.Show("შეცდომა დაფიქსირდა მონაცემთა ბაზის იმპორტის დროს...");
            }
        } // მონაცემთა ბაზის იმპორტი
        private void exportDB() // Backup
        {
            try
            {
                SaveFileDialog savedb = new SaveFileDialog();
                savedb.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                savedb.Filter = "SQLite Database Files(*.sqlite)|*sqlite";
                savedb.FileName = "DataSource _ " + Tools.DateManager.getCurrentTime() + ".sqlite";
                if (savedb.ShowDialog() == DialogResult.OK)
                {
                    string newDirectory = savedb.FileName;
                    System.IO.File.Copy(Tools.DataTools.getDBpath(), newDirectory);
                    MessageBox.Show("Database Backup Successfull At: " + Tools.DateManager.getCurrentTime());
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
        private void backBlacktexts()
        {
            txtbxArrAtbxOffice.ForeColor = Color.Black;
            txtbxordcreatDate.ForeColor = Color.Black;
            deliverAtGeo.ForeColor = Color.Black;
        }
        private void ClearAll()
        {
            txtbxpckgName.Clear();
            txtbxpckgLink.Clear();
            txtbxOrdNum.Clear();
            txtbxTrackNum.Clear();
            txtbxTransComp.Clear();
            txtbxCount.Clear();
            txtbxOrdWght.Clear();
            txtbxitPrice.Clear();
            txtbxvalGel.Clear();
            txtbxShipFee.Clear();
            txtbxBoF.Clear();
            txtbxordcreatDate.Clear();
            txtbxEstTime.Clear();
            txtbxArrAtbxOffice.Clear();
            chkbxdelivAtOffice.Checked = false;
            chkbxPayed.Checked = false;
            chkbxGotOut.Checked = false;
            chkbxCC.Checked = false;
            txtbxpricetotall.Clear();
            txtbxTimetotall.Clear();
            deliverAtGeo.Clear();
        }
        private void disableAllButtons()
        {
            btnAddNew.Enabled = false;
            btnBackupDB.Enabled = false;
            btnCCopen.Enabled = false;
            btnCurrent1.Enabled = false;
            btnCurrent2.Enabled = false;
            btnCurrent3.Enabled = false;
            btnDelete.Enabled = false;
            btnImportDB.Enabled = false;
            btnLoadImage.Enabled = false;
            btnPriceCalc.Enabled = false;
            btnRemovePic.Enabled = false;
            btnSave.Enabled = false;
            btnSellerInfo.Enabled = false;
            btnUpdateRow.Enabled = false;
            menuStripMain.Enabled = false;
        }
        private void enableAllButtons()
        {
            btnAddNew.Enabled = true;
            btnBackupDB.Enabled = true;
            btnCurrent1.Enabled = true;
            btnCurrent2.Enabled = true;
            btnCurrent3.Enabled = true;
            btnDelete.Enabled = true;
            btnImportDB.Enabled = true;
            btnLoadImage.Enabled = true;
            btnPriceCalc.Enabled = true;
            btnRemovePic.Enabled = true;
            btnSellerInfo.Enabled = true;
            btnUpdateRow.Enabled = true;
            menuStripMain.Enabled = true;
        }
        private void addNewParcel() // ახალი ამანათის დამატების ფუნქცია
        {
            ClearAll();
            ordersList.Enabled = false;
            btnUpdateRow.Enabled = false;
            pcbxPackImage.Image = Properties.Resources.NoImageAvailable;
            btnLoadImage.Enabled = false;
            btnRemovePic.Enabled = false;
            btnDelete.Text = "გაუქმება";
            clicked++;
            btnSave.Enabled = true;
            deliverAtGeo.Text = txtbxArrAtbxOffice.Text;
            txtbxArrAtbxOffice.Enabled = true;
            btnCurrent1.Enabled = true;
            btnCurrent2.Enabled = true;
            btnCurrent3.Enabled = true;

            if (txtbxArrAtbxOffice.Text != "") deliverAtGeo.Text = txtbxArrAtbxOffice.Text;
        }
        private void deleteCurrentParcel()
        {
            if (clicked > 0) // პირობა, რომელიც ამოწმებს , გაუქმების მიზნით მოხდა დაჭერა ღილაკზე, თუ წაშლის...
            {
                btnDelete.Text = "წაშლა";
                ClearAll();
                btnUpdateRow.Enabled = true;
                ordersList.SelectedIndex = 0;
                clicked = 0;
                btnSave.Enabled = false;
                ordersList.Enabled = true;
                backBlacktexts();
            }

            else
            {
                btnDelete.Text = "წაშლა";

                DialogResult dialogResult = MessageBox.Show("Want To Delete Selected Pachage?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dialogResult == DialogResult.Yes)
                {
                    ClearAll();
                    Tools.DataTools.deleteRecord(ordersList.SelectedIndex);
                    Tools.DataTools.loadOrdersList();
                }
                else if (dialogResult == DialogResult.No)
                {

                }

                ordersList.Enabled = true;
                Tools.StatusOrganizers.OrdersCountShow();
                clicked = 0;
                btnSave.Enabled = false;
                btnCurrent1.Enabled = false;
                btnCurrent2.Enabled = false;
                btnCurrent3.Enabled = false;
                backBlacktexts();
            }
        }
        private bool pricesValidator()
        {
            try{
                if (Convert.ToDouble(txtbxitPrice.Text.Replace(".", ",")) <= 0) { txtbxitPrice.ForeColor = Color.Red; return false; }
                else txtbxitPrice.ForeColor = Color.Black;
               }
                
            catch (Exception err) { MessageBox.Show("დაფიქსირდა შეცდომა... ამანათის ფასი არასწორია"); txtbxitPrice.Text = ""; return false; }

            try{ 
                if (Convert.ToDouble(txtbxShipFee.Text.Replace(".", ",")) < 0) { txtbxShipFee.ForeColor = Color.Red; return false; }
                else txtbxShipFee.ForeColor = Color.Black;
               } 
            catch (Exception err) { MessageBox.Show("დაფიქსირდა შეცდომა... ამანათის გადაზიდვის ფასი არასწორია"); txtbxShipFee.Text = ""; return false; }
            
            try{ 
            if (Convert.ToDouble(txtbxBoF.Text.Replace(".", ",")) <= 0) { txtbxBoF.ForeColor = Color.Red; return false; }
            else txtbxBoF.ForeColor = Color.Black;
            }
            catch (Exception err) { MessageBox.Show("დაფიქსირდა შეცდომა... გამოტანის ფასი არასწორია"); txtbxBoF.Text = ""; return false; }
            return true;
        } // ფასების ვალიდატორი

        // * * * * * * * * * *      ღილაკები      * * * * * * * * * * //

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            addNewParcel();
        } 
        private void btnUpdateRow_Click(object sender, EventArgs e) // მიმდინარე ამანათის განახლება
        {
            bool NoErrorDate = false, NoErrorPrices = pricesValidator();

            NoErrorDate = Tools.DateManager.textboxesValidator(sender);

            if (NoErrorDate && NoErrorPrices) // თუ არ დაფიქსირდა შეცდომა ფასებში და არც თარიღებში
            {
                Tools.DataTools.UpdateOrderInfo(ordersList.SelectedIndex);
                ordersList.Items.Clear();
                Tools.DataTools.loadOrdersList();
            }
        }
        private void btnSave_Click(object sender, EventArgs e) // ახალი დამატებული ამანათის შენახვა
        {
            bool NoError = false, NoErrorPrices = pricesValidator();

            NoError = Tools.DateManager.textboxesValidator(sender);

            if (NoError && NoErrorPrices) // თუ არ დაფიქსირდა შეცდომა ფასებში და არც თარიღებში
            {

                btnUpdateRow.Enabled = true;
                cmbxCurrency.SelectedIndex = (cmbxCurrency.SelectedIndex == -1) ? 0 : cmbxCurrency.SelectedIndex; // CrashFix
                Tools.DataTools.AddNewRecord();
                ordersList.Items.Clear();
                Tools.DataTools.loadOrdersList();
                btnRemovePic.Enabled = true;
                btnLoadImage.Enabled = true;
                Tools.StatusOrganizers.OrdersCountShow();
                btnDelete.Text = "წაშლა";

                clicked = 0;
                btnSave.Enabled = false;
                ordersList.Enabled = true;
            }
        }
        private void btnDelete_Click(object sender, EventArgs e) // მიმდინარე ამანათის წაშლა
        {
            deleteCurrentParcel();
        }
        private void btnLoadImage_Click(object sender, EventArgs e) // ფოტოს მიმაგრება
        {
            Tools.DataTools.GetImagePath();
            Tools.DataTools.UpdateImage(ordersList.SelectedIndex);
            txtbxImgPath.Text = "";
        }
        private void btnRemovePic_Click(object sender, EventArgs e) // ფოტოს წაშლა
        {
            DialogResult dialogResult = MessageBox.Show("Want To Clear Attached Picture?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialogResult == DialogResult.Yes)
            {
                pcbxPackImage.Image = Properties.Resources.NoImageAvailable;
                Tools.DataTools.RemoveImage(ordersList.SelectedIndex); // სურათის წაშლა მიმდინარე ინდექსზე არსებული ამანათისთვის
                MessageBox.Show("Image Removed");
                txtbxImgPath.Text = "";
            }
            else if (dialogResult == DialogResult.No)
            {

            }
        }
        private void btnBackupDB_Click(object sender, EventArgs e)
        {
            exportDB();
        } // მონაცემთა ბაზის დარეზერვება
        private void btnPriceCalc_Click(object sender, EventArgs e)
        {
            PriceCalculator prCalc = new PriceCalculator();
            prCalc.ShowDialog();
        } // * წონის კალკულატორის * გამოძახება
        private void btnImportDB_Click(object sender, EventArgs e)
        {
            importDB();
        }  // არსებული მონაცემთა ბაზის იმპორტი
        private void btnCCopen_Click(object sender, EventArgs e)
        {
            customClearanse CC = new customClearanse();
            CC.ShowDialog();
        }  // განბაჟების ფანჯრის გამოძახება
        private void btnSellerInfo_Click(object sender, EventArgs e)
        {
            txtbxsellerInfo Info = new txtbxsellerInfo();
            Info.ShowDialog();
        } // დამატებითი ინფორმაცია გამყიდველზე *
        private void btnCurrent1_Click(object sender, EventArgs e)
        {
            txtbxordcreatDate.Text = DateTime.Today.ToShortDateString();
        }
        private void btnCurrent2_Click(object sender, EventArgs e)
        {
            txtbxArrAtbxOffice.Text = DateTime.Today.ToShortDateString();
        }
        private void btnCurrent3_Click(object sender, EventArgs e)
        {
            deliverAtGeo.Text = DateTime.Today.ToShortDateString();
        }



        // * * * * * * * * * *      KeyPress ევენთები + KeyDown    * * * * * * * * * * //
        private void txtbxordcreatDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '/';
        }
        private void txtbxArrAtbxOffice_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '/';
        }
        private void deliverAtGeo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '/';
        }
        private void txtbxShipFee_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '.';
        }
        private void txtbxOrdWght_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '.';
        }
        private void txtbxitPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != ',';
        }
        private void txtbxCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void ordersList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (char)Keys.Delete)
            {
                deleteCurrentParcel();
            }
        }


        // * * * * * * * * * *      TextChanged ევენთები      * * * * * * * * * * //
        private void chkbxCC_CheckedChanged(object sender, EventArgs e)
        {
            if (chkbxCC.Checked) btnCCopen.Enabled = true;
            else btnCCopen.Enabled = false;
        }
        private void txtbxitPrice_TextChanged(object sender, EventArgs e)
        {

        }
        private void cmbxCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void txtbxTimetotall_TextChanged(object sender, EventArgs e)
        {
            if (!chkbxGotOut.Checked)
            {
                txtbxTimetotall.Text = "ამანათი არაა გატანილი";
            }
        }
        private void txtbxpricetotall_TextChanged(object sender, EventArgs e)
        {
            if (!chkbxPayed.Checked)
            {
                txtbxpricetotall.Text = "გატანის თანხა არაა გადახდილი";
            }
        }
      

        // * * * * * * * * * *      MenuTrip ფუნქციები      * * * * * * * * * * //
        private void addNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addNewParcel();
        }
        private void deleteCurrentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            deleteCurrentParcel();
        }
        private void databaseImportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            importDB();
        }
        private void databaseExportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            exportDB();
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void refreshCurrencyRatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Tools.StatusOrganizers.netAvailable) Packages.CurrencyManager.UpdateToTextBox();
            else
            {
                Tools.DataTools.restoreCurrencies();
                refreshCurrencyRatesToolStripMenuItem.Enabled = false;
            }
        }
        private void refreshItemListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Tools.DataTools.loadOrdersList();
        }
        private void refreshStatusWindowsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PreLoad();
            enableAllButtons();
        }

        // * * * * * * * * * *      პროგრამის გათიშვის: OnFormClosing ფუნქციის გადაფარვა       * * * * * * * * * * //
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.WindowsShutDown) return;

            // Confirm user wants to close
            switch (MessageBox.Show(this, "Are You Sure Want To Close The Application? - გნებავთ დასრულდეს პროგრამა?", "Closing - პროგრამის დასრულება", MessageBoxButtons.YesNo))
            {
                case DialogResult.No:
                    e.Cancel = true;
                    break;
                default:
                    break;
            }
        }

    }
}
