using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrderingRegistrator
{
    public partial class PriceCalculator : Form
    {
        private static double TransportersMethodPriceOnKG;
        private static double currencyRate;
        private static double packageWeight;
        private static double packagePrice;
        private static double probablePrice = 0;

        public PriceCalculator()
        {
            InitializeComponent();
        }

        private void PriceCalculator_Load(object sender, EventArgs e)
        {
            txtbxGetPrice.Text = "8.5";
            txtbxPackWeight.Text = "100";
            txtbxPrice.Text = "5";
            cmbBxCurrency.SelectedIndex = 0;
        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            if (!validator())
            {
                try
                {
                    currencyRate = Tools.DataTools.getValuteRate(0);

                    TransportersMethodPriceOnKG = Convert.ToDouble(txtbxGetPrice.Text.Replace(".", ",")) * currencyRate;

                    packageWeight = Convert.ToDouble(txtbxPackWeight.Text.Replace(".", ",")) / 1000;

                    packagePrice = Convert.ToDouble(txtbxPrice.Text.Replace(".", ","));

                    probablePrice = (packageWeight * TransportersMethodPriceOnKG) + (packagePrice * currencyRate);
                    labAns.Text = " სავარაუდო ფასი შეიძლება იყოს: " + probablePrice.ToString("F2") + " ლარი";
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
            }
        }

        private bool validator() // ვალიდატორი
        {
            object sender = new object();
            CancelEventArgs cancelArg = new CancelEventArgs();

            try
            {
                Convert.ToDouble(txtbxGetPrice.Text.Replace(".", ","));
                Convert.ToDouble(txtbxPackWeight.Text.Replace(".", ","));
                Convert.ToDouble(txtbxPrice.Text.Replace(".", ","));

                cancelArg.Cancel = (Convert.ToDouble(txtbxGetPrice.Text.Replace(".", ",")) <= 0) ? true : false;
                txtbxGetPrice_Validating(sender, cancelArg);
                if (cancelArg.Cancel) return true;
                cancelArg.Cancel = (Convert.ToDouble(txtbxPackWeight.Text.Replace(".", ",")) <= 0) ? true : false;
                txtbxPackWeight_Validating(sender, cancelArg);
                if (cancelArg.Cancel) return true;
                cancelArg.Cancel = (Convert.ToDouble(txtbxPrice.Text.Replace(".", ",")) <= 0) ? true : false;
                txtbxPrice_Validating(sender, cancelArg);
                if (cancelArg.Cancel) return true;
                return false;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
                return false;
            }
        }

        // * * * * * * * * * *      KeyPress ევენთები      * * * * * * * * * * //
        private void txtbxGetPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != ',';
        }
        private void txtbxPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != ',';
        }
        private void txtbxPackWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != ',';
        }


        // * * * * * * * * * *      Validating ფუნქციები      * * * * * * * * * * //
        private void txtbxGetPrice_Validating(object sender, CancelEventArgs e)
        {
            if (e.Cancel) { labAns.Text = "დაფიქსირდა შეცდომა..."; txtbxGetPrice.ForeColor = Color.Red; }
            else
            {
                txtbxGetPrice.ForeColor = Color.Black;
            }
        }
        private void txtbxPackWeight_Validating(object sender, CancelEventArgs e)
        {
            if (e.Cancel) { labAns.Text = "დაფიქსირდა შეცდომა..."; txtbxPackWeight.ForeColor = Color.Red; }
            else
            {
                txtbxPackWeight.ForeColor = Color.Black;
            }
        }
        private void txtbxPrice_Validating(object sender, CancelEventArgs e)
        {
            if (e.Cancel) { labAns.Text = "დაფიქსირდა შეცდომა..."; txtbxPrice.ForeColor = Color.Red; }
            else
            {
                txtbxPrice.ForeColor = Color.Black;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            object sender = new object(); CancelEventArgs cancelE = new CancelEventArgs(); cancelE.Cancel = false;

            txtbxGetPrice_Validating(sender, cancelE);
            txtbxPackWeight_Validating(sender, cancelE);
            txtbxPrice_Validating(sender, cancelE);

            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.WindowsShutDown) return;
                    e.Cancel = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtbxGetPrice.Text = "";
        }
        private void btnClean2_Click(object sender, EventArgs e)
        {
            txtbxPackWeight.Text = "";
        }
        private void btnClean3_Click(object sender, EventArgs e)
        {
            txtbxPrice.Text = "";
        }

        private void btnPlus3_Click(object sender, EventArgs e)
        {
            if (txtbxPrice.Text == "") txtbxPrice.Text = "0";
            txtbxPrice.Text = (Convert.ToDouble(txtbxPrice.Text) + 10).ToString();
        }

        private void btnPlus2_Click(object sender, EventArgs e)
        {
            if (txtbxPackWeight.Text == "") txtbxPackWeight.Text = "0";
            txtbxPackWeight.Text = (Convert.ToDouble(txtbxPackWeight.Text) + 10).ToString();
        }
    }
}
