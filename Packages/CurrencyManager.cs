using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Windows.Forms;
using System.Web;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Threading;
using System.Globalization;

namespace OrderingRegistrator.Packages
{
    class CurrencyManager // კლასი უზრუნველყოფს ვალუტის კურსის კონტროლს პროგრამაში
    {
                  
        private static double USDprop;
        private static double EURprop;
        private static double GRBprop;
        private static double RUBprop;

        // Getters `N Setters //
        public static double USD
        {
            get { return USDprop; }
            set { USDprop = value; }
        }
        public static double EUR
        {
            get { return EURprop; }
            set { EURprop = value; }
        }
        public static double GRB
        {
            get { return GRBprop; }
            set { GRBprop = value; }
        }
        public static double RUB
        {
            get { return RUBprop; }
            set { RUBprop = value; }
        }

        public static TextBox txtbxShowCurrencies;
        private static string usdLink, eurLink, grbLink, rubLink;

        private static StringBuilder yqlUSD;
        private static StringBuilder yqlEUR;
        private static StringBuilder yqlGRB;
        private static StringBuilder yqlRUB;

        private static void CleanTxtBox() // ვალუტის კურსისთვის განკუთვნილი TxtBox-ის გასუფთავება
        {
            txtbxShowCurrencies.Clear();
        }
        public static void UpdateToTextBox()
        {

           Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("ru-RU"); // *Force Double Convertation mEthods To Work Properly... * "en-EN" Culture Breaks When Converting Double:::

           // * - ინვარიანტული კულტურა "ru-RU" არ უშვებს შეცდომას Double ტიპის ცვლადში კონვერტაციის დროს

            CleanTxtBox();
            txtbxShowCurrencies.Text = "Currency Rates:";
            JsonDownloader();
        }
        private static void JsonDownloader() // ფუნქციას yql query-ის საშუალებით შეუძლია "წამოიღოს" უცხოური ვალუტის კურსი ლართან მიმართებაში .jSon ფაილის სახით...
        {
            yqlUSD = new StringBuilder();
            yqlEUR = new StringBuilder();
            yqlGRB = new StringBuilder();
            yqlRUB = new StringBuilder();


            // Yahoo API-ს Live-მისამართები...
            yqlUSD.Append("https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20yahoo.finance.xchange%20where%20pair%20in%20(%22USDMXN%22%2C%20%22USDGEL%22)&format=json&diagnostics=true&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys&callback=");
            yqlEUR.Append("https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20yahoo.finance.xchange%20where%20pair%20in%20(%22EURMXN%22%2C%20%22EURGEL%22)&format=json&diagnostics=true&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys&callback=");
            yqlGRB.Append("https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20yahoo.finance.xchange%20where%20pair%20in%20(%22GBPMXN%22%2C%20%22GBPGEL%22)&format=json&diagnostics=true&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys&callback=");
            yqlRUB.Append("https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20yahoo.finance.xchange%20where%20pair%20in%20(%22RUBMXN%22%2C%20%22RUBGEL%22)&format=json&diagnostics=true&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys&callback=");

            using (WebClient wc = new WebClient())
            {
                // ხდება გადაცემული Live-მისამართიდან ინფორმაციის გადმოტვირთვა და შენახვა

                wc.Proxy = null;
                usdLink = wc.DownloadString(yqlUSD.ToString()); 
                eurLink = wc.DownloadString(yqlEUR.ToString());
                grbLink = wc.DownloadString(yqlGRB.ToString());
                rubLink = wc.DownloadString(yqlRUB.ToString());
            }

            // Saving Current Currency For Valutes - .Json ტიპის ფაილიდან ინფორმაციის წაკითხვა ხდება Json ბიბლიოთეკის ფუნქციების გამოყენებით...
            USDprop = JsonParser(usdLink, "USD", USDprop); // USDprop double ცვლადს მიენიჭება წაკითხული ინფორმაციიდან კონვერტირებული კურსი:::
            EURprop = JsonParser(eurLink, "EUR", EURprop);
            GRBprop = JsonParser(grbLink, "GRB", GRBprop);
            RUBprop = JsonParser(rubLink, "RUB", RUBprop);
            AttachDate();
        }
        private static double JsonParser(string valueLink, string ValuteName, double Value)
        {
            JObject dataObject = JObject.Parse(valueLink);
            JArray jsonArray = (JArray)dataObject["query"]["results"]["rate"];

            foreach (var locationResult in jsonArray.Skip(1)) // ხდება პირველი იტერაციის გამოტოვება
            {
                txtbxShowCurrencies.Text += " " + ValuteName + ": " + locationResult["Ask"].ToString() + " |";
                string ExtractedString = locationResult["Ask"].ToString().Replace(".", ",");
                Value = Convert.ToDouble(ExtractedString);
            }
            return Value;
        }
        private static void AttachDate() // სტატუსად გამოიტანოს ბოლო განახლების თარიღი
        {
            txtbxShowCurrencies.Text += " Last Check: " + DateTime.Now.ToString("h:mm:ss tt") + "| Date:" + DateTime.Now.ToString("dd/MM/yyyy");

        }
    }
}
