using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite; //!!
using System.Data;
using System.IO;
using System.Drawing;
using System.Reflection;

namespace OrderingRegistrator.Tools
{
    public class DataTools
    {

        // Interface TextBoxes , Labels , ImageBoxes, Buttons... public ობიექტების განსაზღვრა //

        public static TextBox txtbxpckgName;
        public static TextBox txtbxpckgLink;
        public static TextBox txtbxCount;
        public static TextBox txtbxitPrice;
        public static TextBox txtbxShipFee;
        public static TextBox txtbxTransComp;
        public static TextBox txtbxTrackNum;
        public static TextBox txtbxOrdNum;
        public static TextBox txtbxOrdWght;
        public static TextBox txtbxordcreatDate;
        public static TextBox txtbxEstTime;
        public static TextBox txtbxArrAtbxOffice;
        public static TextBox txtbxvalGel;
        public static TextBox deliverAtGeo;
        public static TextBox txtbxBoF;
        public static TextBox txtbxTimetotall;
        public static TextBox txtbxpricetotall;
        public static TextBox txtbxShowCurrencies;
        public static PictureBox pcbxPackImage;
        public static CheckBox chkbxdelivAtOffice;
        public static CheckBox chkbxPayed;
        public static CheckBox chkbxGotOut;
        public static CheckBox chkbxCC;
        public static TextBox txtbxImgPath;
        public static ListBox ordersList;
        public static ComboBox cmbxCurrency;


        // SQL Query Strings  - SQL ბრძანებები //

        private static string QueryOrdersShow = "Select * From OrderedItems";
        private static string QueryCurrencies = "Select * From Currency";
        private static string QueryrowsCount = "SELECT COUNT(*) FROM OrderedItems";

        // * //


        private static string imagePath = ""; // ფოტო სურათის მისამართი ვინჩესტერზე...
        private static byte[] imageBT = null; // byte ტიპის მასივი ფოტოს შესანახად ***
        private static int MaxRows; // მონაცემთა ბაზის , რომელიმე Table -ის სტრიქონდა მაქსიმალური რაოდენობა

        static SQLiteConnection connection;
        private static double convertedToGel;

        //private static string ConnectionString = @"Data Source=D:\Programming (R) Ultimate\Informatics 4EverHere\C#\C# BacheLorsApp_OrderingManager\OrderingRegistrator_SQLITE\OrderingRegistrator\OrderingRegistrator\Data\DataSource.sqlite;Version=3";



        private static string ConnectionString = MakeDataLink(); // ConnectionString - SQLite ბაზასთან წვდომისათვის
        public static string getConnectionString { get { return ConnectionString; } }
        private static string MakeDataLink() // Auto-Generated DataFile Source Path
        {
            // ფუნქცია აგენერირებს არსებული მონაცემთა ბაზის მისამართს, რომელიც ორი დონით წინაა , ვიდრე გამშვები ფაილი: OrderingRegistrator.exe *

            StringBuilder strBuild = new StringBuilder();
            strBuild.Append(@"Data Source=");
            strBuild.Append(System.IO.Path.GetFullPath(".\\")); // უზრუნველყოფს მისამართის სისწორეს ვინჩესტერის ნებისმიერი დისკიდან წაკითხვის შემთხვევაში
            strBuild.Remove(strBuild.Length - 12, 12);
            strBuild.Append(@"Data\DataSource.sqlite;Version=3");
            return strBuild.ToString();
        }

        // Dataset & DataRow - მონაცემთა ბაზების ობიექტები

        private static DataSet currencyValues;
        private static DataSet OrdersInfo;


        // * * * * * * * * * *     Data ფუნქციები      * * * * * * * * * * //
        public static string getDBpath() // ფუნქცია აბრუნებს არსებული მონაცემთა ბაზის მისამართს ვინჩესტერზე
        {
            StringBuilder strBuild = new StringBuilder();
            strBuild.Append(System.IO.Path.GetFullPath(".\\"));
            strBuild.Remove(strBuild.Length - 11, 11);
            strBuild.Append(@"\Data\DataSource.sqlite");
            return strBuild.ToString();
        }
        public static int getMaxRows
        {
            get
            {
                try
                {

                    using (connection = new SQLiteConnection(ConnectionString)) // initialise a Connection
                    {
                        using (SQLiteCommand sqlCommand = new SQLiteCommand(QueryrowsCount, connection))
                        {
                            connection.Open();
                            MaxRows = Convert.ToInt32(sqlCommand.ExecuteScalar());
                            return MaxRows;
                        }
                    }

                }

                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                }
                return MaxRows;
            }
        } // ჩანაწერთა მაქსიმალური რაოდენობა Table - OrderedItem-ში... ანუ გამოწერილი ამანათების რაოდენობა
        public static void loadOrdersList() // Load Orders in ListBox - გამოწერილი ამანათების (ნივთების) ამოტანა ListBox-ში შეზღუდული ინფორმაციით **
        {

            int counter = 0; // ინდექს ცვლადი + მთვლელი
            try
            {
                using (connection = new SQLiteConnection(ConnectionString))
                {
                    using (SQLiteCommand sqliteCommand = new SQLiteCommand(QueryOrdersShow, connection))
                    {
                        connection.Open();
                        SQLiteDataReader dReader = sqliteCommand.ExecuteReader();
                        ordersList.Items.Clear();
                        while (dReader.Read())
                        {
                            ordersList.Items.Add(" ამანათის ID:  " + (++counter) + "   |   დასახელება: " + dReader["pckgName"].ToString()); //+ "   |   " + dReader["pckgPrice"] + "   |   " + dReader["transCompany"] + "   |   " + "Order Date: " + dReader["orderCreateDate"] + "   |   " + "Totall Cost: " + dReader["processingMoney"] + " GEL");
                        }
                        dReader.Close();
                    }

                    connection.Close();
                }

            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }
        public static void populateOrders(int Index) // Populate Orders in TextBoxes - გამოწერილი 
        {
            
            OrdersInfo = new DataSet();
            try
            {
                using (connection = new SQLiteConnection(ConnectionString))
                {
                    using (SQLiteCommand sqliteCommand = new SQLiteCommand(QueryOrdersShow, connection))
                    {
                        connection.Open();
                        using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqliteCommand))
                        {
                            DataRow dRow;
                            adapter.Fill(OrdersInfo);
                            dRow = OrdersInfo.Tables[0].Rows[Index];

                            txtbxpckgName.Text = dRow.ItemArray.GetValue(1).ToString();
                            txtbxpckgLink.Text = dRow.ItemArray.GetValue(2).ToString();

                            if (dRow.ItemArray.GetValue(3).ToString() == "") // თუ მონაცემთა ბაზაში ჩაწერილი არ არის სურათი Byte[], მაშინ გამოვიდეს ცარიელი სურათი NoImageAvailable  - უჯრაში
                            {
                                pcbxPackImage.Image = Properties.Resources.NoImageAvailable;
                            }
                            else loadImage(OrdersInfo, Index); //Image Load Function

                            txtbxCount.Text = dRow.ItemArray.GetValue(4).ToString();
                            txtbxitPrice.Text = dRow.ItemArray.GetValue(6).ToString();
                            txtbxShipFee.Text = dRow.ItemArray.GetValue(7).ToString();
                            txtbxTransComp.Text = dRow.ItemArray.GetValue(8).ToString();
                            txtbxTrackNum.Text = dRow.ItemArray.GetValue(9).ToString();
                            txtbxOrdNum.Text = dRow.ItemArray.GetValue(10).ToString();
                            txtbxOrdWght.Text = dRow.ItemArray.GetValue(11).ToString();
                            txtbxordcreatDate.Text = dRow.ItemArray.GetValue(12).ToString();
                            txtbxordcreatDate.Text = txtbxordcreatDate.Text.Replace("/", ".");
                            txtbxEstTime.Text = dRow.ItemArray.GetValue(13).ToString();
                            txtbxArrAtbxOffice.Text = dRow.ItemArray.GetValue(14).ToString();
                            txtbxArrAtbxOffice.Text = txtbxArrAtbxOffice.Text.Replace("/",".");
                            deliverAtGeo.Text = dRow.ItemArray.GetValue(18).ToString();
                            deliverAtGeo.Text = deliverAtGeo.Text.Replace("/",".");
                            txtbxBoF.Text = dRow.ItemArray.GetValue(19).ToString();
                            chkbxdelivAtOffice.Checked = Convert.ToInt32(dRow[20].ToString()) == 1; // ჩამოსულია თუ არა საქართველოში
                            chkbxPayed.Checked = Convert.ToInt32(dRow[21].ToString()) == 1; // გადახდილია თუ არა
                            chkbxGotOut.Checked = Convert.ToInt32(dRow[22].ToString()) == 1; // გატანილია თუ არა
                            chkbxCC.Checked = Convert.ToInt16(dRow[15].ToString()) == 1; // საბაჟო უწევს თუ არა
                            cmbxCurrency.SelectedIndex = Convert.ToInt16(dRow[5].ToString()) - 1; // უცხოურ რომელ ვალუტაში მოხდა მიმდინარე ამანათის შეძენა
                            txtbxpricetotall.Text = dRow.ItemArray.GetValue(24).ToString(); // საბოლოო ფასი
                            txtbxTimetotall.Text = DateManager.getProcessingTime(); // დახარჯული დრო
                            convertToGel(Index); // უცხოური ვალუტის მიმდინარე კურსით ლარში კონვერტაცია *** ფუნქციას გადაეცემა ინდექსი
                        }
                    }
                }
              
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }      
        public static void UpdateOrderInfo(int Index) // ამანათის განახლების ფუნქცია
        {
            OrdersInfo = new DataSet();
            try
            {
                using (connection = new SQLiteConnection(ConnectionString))
                {
                    using (SQLiteCommand sqliteCommand = new SQLiteCommand(QueryOrdersShow, connection))
                    {
                        connection.Open();

                        using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqliteCommand))
                        {
                            DataRow dRow;
                            adapter.Fill(OrdersInfo);
                            dRow = OrdersInfo.Tables[0].Rows[Index];

                                dRow[1] = txtbxpckgName.Text;
                                dRow[2] = txtbxpckgLink.Text;

                                if (dRow[3].ToString() == "") pcbxPackImage.Image = Properties.Resources.NoImageAvailable;

                                dRow[4] = txtbxCount.Text;
                                dRow[5] = cmbxCurrency.SelectedIndex + 1;
                                dRow[6] = txtbxitPrice.Text;
                                dRow[7] = txtbxShipFee.Text;
                                dRow[8] = txtbxTransComp.Text;
                                dRow[9] = txtbxTrackNum.Text;
                                dRow[10] = txtbxOrdNum.Text;
                                dRow[11] = txtbxOrdWght.Text;
                                dRow[12] = txtbxordcreatDate.Text;

                                dRow[13] = txtbxEstTime.Text;
                                dRow[14] = txtbxArrAtbxOffice.Text;
                                dRow[18] = deliverAtGeo.Text;
                                dRow[19] = txtbxBoF.Text;
                                convertToGel(Index);
                                dRow[24] = UpdateTotallAmount(); // dRow[24]
                                dRow[23] = Tools.DateManager.getProcessingTime(); //dRow[23]
                                dRow[20] = (chkbxdelivAtOffice.Checked == true) ? 1 : 0; // Fast Way to Check
                                dRow[21] = (chkbxPayed.Checked == true) ? 1 : 0;
                                dRow[22] = (chkbxGotOut.Checked == true) ? 1 : 0;

                                if (chkbxCC.Checked) { // თუ ამანათს მოუწია საბაჟოზე დაყოვნება... უნდა შეივსოს დამატებითი ინფორმაცია განბაჟებაზე                         
                                    MessageBox.Show("Edit Custom Clearance info");
                                    customClearanse CC = new customClearanse();
                                    CC.ShowDialog();
                                }
                                else
                                {
                                    dRow[15] = 0;
                                    dRow[16] = null;
                                    dRow[17] = null;


                                    // *** განახლების სკრიპტი *** //

                                    SQLiteCommandBuilder commBuild;
                                    commBuild = new SQLiteCommandBuilder(adapter);
                                    adapter.UpdateCommand = commBuild.GetUpdateCommand(true);
                                    adapter.Update(OrdersInfo);
                                    MessageBox.Show("Update Complete");
                                }
                            
                        }
                       
                    }
 
                }

            }
            catch (SQLiteException error)
            {
                MessageBox.Show(error.Message);
            }
        } // Done
        public static string UpdateTotallAmount()
        {
            string TotallAmount = (convertedToGel + Convert.ToDouble(txtbxShipFee.Text.Replace(".", ",")) + Convert.ToDouble(txtbxBoF.Text.Replace(".", ","))).ToString("F2");
            return TotallAmount;
        } // სრული საფასურის განახლების ფუნქცია
        public static void AddNewRecord()
        {
            OrdersInfo = new DataSet();
            try
            {
                using (connection = new SQLiteConnection(ConnectionString))
                {
                    using (SQLiteCommand sqliteCommand = new SQLiteCommand(QueryOrdersShow, connection))
                    {
                        connection.Open();

                        using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqliteCommand))
                        {
                            DataRow dRow;
                            adapter.Fill(OrdersInfo);
                            dRow = OrdersInfo.Tables[0].NewRow();

                            dRow[1] = txtbxpckgName.Text;
                            dRow[2] = txtbxpckgLink.Text;

                            if (dRow[3].ToString() == "") pcbxPackImage.Image = Properties.Resources.NoImageAvailable;

                            dRow[4] = txtbxCount.Text;
                            dRow[5] = cmbxCurrency.SelectedIndex + 1;
                            dRow[6] = txtbxitPrice.Text;
                            dRow[7] = txtbxShipFee.Text;
                            dRow[8] = txtbxTransComp.Text;
                            dRow[9] = txtbxTrackNum.Text;
                            dRow[10] = txtbxOrdNum.Text;
                            dRow[11] = txtbxOrdWght.Text;
                            dRow[12] = txtbxordcreatDate.Text;

                            dRow[13] = txtbxEstTime.Text;
                            dRow[14] = txtbxArrAtbxOffice.Text;
                            dRow[15] = (chkbxCC.Checked == true) ? 1 : 0;
                            dRow[18] = deliverAtGeo.Text;
                            dRow[19] = txtbxBoF.Text;
                            convertToGel(getMaxRows-1);
                            dRow[24] = UpdateTotallAmount(); // dRow[24]
                            dRow[23] = Tools.DateManager.getProcessingTime(); //dRow[23] 

                            dRow[20] = (chkbxdelivAtOffice.Checked == true) ? 1 : 0; // Fast Way to Check
                            dRow[21] = (chkbxPayed.Checked == true) ? 1 : 0;
                            dRow[22] = (chkbxGotOut.Checked == true) ? 1 : 0;



                            OrdersInfo.Tables[0].Rows.Add(dRow);

                            SQLiteCommandBuilder commBuild;
                            commBuild = new SQLiteCommandBuilder(adapter);
                            adapter.UpdateCommand = commBuild.GetUpdateCommand(true);
                            adapter.Update(OrdersInfo);
                            MessageBox.Show("New Package Added Successfuly");
                        }
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        } // ახალი ამანათის დამატების ფუნქცია
        public static void deleteRecord(int Index)
        {
            OrdersInfo = new DataSet();
            try
            {
                using (connection = new SQLiteConnection(ConnectionString))
                {
                    using (SQLiteCommand sqliteCommand = new SQLiteCommand(QueryOrdersShow, connection))
                    {
                        connection.Open();

                        using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqliteCommand))
                        {
                            adapter.Fill(OrdersInfo);
                            OrdersInfo.Tables[0].Rows[Index].Delete();

                            SQLiteCommandBuilder commBuild;
                            commBuild = new SQLiteCommandBuilder(adapter);
                            adapter.UpdateCommand = commBuild.GetUpdateCommand(true);
                            adapter.Update(OrdersInfo);
                            MessageBox.Show("Package Removed...");
                        }
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }

        } // წაშლა


        // * * * * * * * * *       ToolBox ფუნქციები      * * * * * * *  * //
        public static void convertToGel(int Index)
        {
            currencyValues = new DataSet();
            currencyValues.Clear();
            using (connection = new SQLiteConnection(ConnectionString)) // initialise a Connection
            {
                using (SQLiteCommand sqLiteCommand = new SQLiteCommand(QueryCurrencies, connection))
                {
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqLiteCommand))
                    {
                        adapter.Fill(currencyValues); // Fill DataSet with DataBase
                        DataRow dRow = currencyValues.Tables[0].Rows[cmbxCurrency.SelectedIndex]; // Pass Index From Currency Combobox

                        convertedToGel = (Convert.ToDouble(txtbxitPrice.Text.Replace(".", ",")) * Convert.ToDouble(dRow[1].ToString()) * Convert.ToDouble(txtbxCount.Text.Replace(".", ","))); // Double ტიპის ცვლადში კონვერტაცია

                        txtbxvalGel.Text = convertedToGel.ToString("F2").Replace(",", ".") + " Gel"; // შედეგის გამოტანა F2 სიზუსტით [ასეულებამდე დამრგვალება]
                    }
                }
            }
        }
        public static void RemoveImage(int Index)
        {
            clearImage(Index);
        }
        private static void clearImage(int Index) {
            OrdersInfo = new DataSet();
            try
            {
                using (connection = new SQLiteConnection(ConnectionString))
                {
                    using (SQLiteCommand sqliteCommand = new SQLiteCommand(QueryOrdersShow, connection))
                    {
                        connection.Open();

                        using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqliteCommand))
                        {
                            DataRow dRow;
                            adapter.Fill(OrdersInfo);
                            dRow = OrdersInfo.Tables[0].Rows[Index];
                            dRow[3] = null;
                            pcbxPackImage.Image = Properties.Resources.NoImageAvailable;

                            SQLiteCommandBuilder commBuild;
                            commBuild = new SQLiteCommandBuilder(adapter);
                            adapter.UpdateCommand = commBuild.GetUpdateCommand(true);
                            adapter.Update(OrdersInfo);
                        }
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }

        }
        public static void UpdateImage(int Index)
        {
            OrdersInfo = new DataSet();
            try
            {
                using (connection = new SQLiteConnection(ConnectionString))
                {
                    using (SQLiteCommand sqliteCommand = new SQLiteCommand(QueryOrdersShow, connection))
                    {
                        connection.Open();

                        using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqliteCommand))
                        {
                            DataRow dRow;
                            adapter.Fill(OrdersInfo);
                            dRow = OrdersInfo.Tables[0].Rows[Index];
                            dRow[3] = convertImage(imagePath);
                            MessageBox.Show("Image Attached");

                            SQLiteCommandBuilder commBuild;
                            commBuild = new SQLiteCommandBuilder(adapter);
                            adapter.UpdateCommand = commBuild.GetUpdateCommand(true);
                            adapter.Update(OrdersInfo);
                        }
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }
        private static void loadImage(DataSet dSet,int Index)
        {
            Byte[] data = new Byte[0];
            data = (Byte[])(dSet.Tables[0].Rows[Index]["pckgPic"]);
            MemoryStream mem = new MemoryStream(data);
            pcbxPackImage.Image = Image.FromStream(mem);
        }
        public static string GetImagePath()
        {
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = "JPG Files(*.jpg)|*jpg|PNG Files(*.png)|*.png|BMP Files(*.bmp)|*bmp|All Files(*.*)|*.*";
                dlg.Title = "Choose An Image To Load";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    imagePath = dlg.FileName.ToString();
                    txtbxImgPath.Text = imagePath;
                    pcbxPackImage.ImageLocation = imagePath;
                    return imagePath;
                }
            }
            catch (IOException error)
            {
                MessageBox.Show(error.Message);
            }
            return "";
        } // Get Image Path
        private static byte[] convertImage(string path) // Converts Image To VARBINARY
        {
            try // ხდება სურათის გადაყვანა ბიტურ მასივში
            {
                FileStream fstream = new FileStream(path, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fstream);
                imageBT = br.ReadBytes((int)fstream.Length);
                return imageBT;
            }
            catch (IOException error)
            {
                MessageBox.Show(error.Message);
            }
            return null;
        }


        // Currencies Toolbox * * * * * ფუნქციები , რომლებიც უზრუნველყოფენ ვალუტის ცვლილებას //
        public static void updateCurrencies() // მონაცემთა ბაზაში ინახავს: დოლარის, ევროს, გირვანქა სტერლინგისა და რუბლის - მიმდინარე კურსს ლართან მიმართებაში... ***
        {
            DataSet CurrencyInfo = new DataSet();
            try
            {
                using (connection = new SQLiteConnection(ConnectionString))
                {
                    using (SQLiteCommand sqliteCommand = new SQLiteCommand(QueryCurrencies, connection))
                    {
                        connection.Open();

                        using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqliteCommand))
                        {
                            DataRow dRowUSD, dRowEUR, dRowGRB, dRowRUB;
                            adapter.Fill(CurrencyInfo);

                            // CurrencyManager - კლასი , რომლის ძირითადი ფუნქციაა ვალუტის კურსის "წამოღება"...

                            dRowUSD = CurrencyInfo.Tables[0].Rows[0];
                            dRowUSD[1] = Packages.CurrencyManager.USD;
                            dRowEUR = CurrencyInfo.Tables[0].Rows[1];
                            dRowEUR[1] = Packages.CurrencyManager.EUR;
                            dRowRUB = CurrencyInfo.Tables[0].Rows[2];
                            dRowRUB[1] = Packages.CurrencyManager.RUB;
                            dRowGRB = CurrencyInfo.Tables[0].Rows[3];
                            dRowGRB[1] = Packages.CurrencyManager.GRB;

                            SQLiteCommandBuilder commBuild;
                            commBuild = new SQLiteCommandBuilder(adapter);
                            adapter.UpdateCommand = commBuild.GetUpdateCommand(true);
                            adapter.Update(CurrencyInfo);
                        }
                    }
                }
            }
            catch (SQLiteException error)
            {
                MessageBox.Show(error.Message);
            }
        }
        public static void restoreCurrencies()
        {
            txtbxShowCurrencies.Text = "| Restored From Database: ";
            try
            {
                using (connection = new SQLiteConnection(ConnectionString))
                {
                    using (SQLiteCommand sqliteCommand = new SQLiteCommand(QueryCurrencies, connection))
                    {
                        connection.Open();
                        SQLiteDataReader dReader = sqliteCommand.ExecuteReader();

                        while (dReader.Read())
                        {
                            txtbxShowCurrencies.Text += " " + dReader["currencyName"] + " " + dReader["currencyRate"];
                            //ordersList.Items.Add(" Item ID:  " + (++counter) + "   |   " + dReader["pckgName"] + "   |   " + dReader["pckgPrice"] + "   |   " + dReader["transCompany"] + "   |   " + "Order Date: " + dReader["orderCreateDate"] + "   |   " + "Totall Cost: " + dReader["processingMoney"] + " GEL");
                        }
                        dReader.Close();
                    }

                    connection.Close();
                }
                txtbxShowCurrencies.Text += " |";
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        } // ბოლოს განახლებული ვალუტის კურსის გააქტიურება * [თუ ინტერნეტთან კავშირი ვერ მოხერხდა, რომ გააქტიურდეს ბოლოჯერ განახლებული ვალუტის კურსი]
        public static double getValuteRate(int Index)
        {
             DataSet CurrencyInfo = new DataSet();
             try
             {
                 using (connection = new SQLiteConnection(ConnectionString))
                 {
                     using (SQLiteCommand sqliteCommand = new SQLiteCommand(QueryCurrencies, connection))
                     {
                         connection.Open();

                         using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqliteCommand))
                         {
                             DataRow dRow;
                             adapter.Fill(CurrencyInfo);

                             dRow= CurrencyInfo.Tables[0].Rows[Index];
                             return Convert.ToDouble(dRow[1].ToString().Replace(".", ","));
                         }
                     }
                 }
             }
            catch (Exception err)
             {
                 MessageBox.Show(err.Message);
             }
             return 0;
        } // უცხოური ვალუტის კურსი ლართან მიმართებაში გადაცემული ინდექსის მიხედვით...
    }
}
