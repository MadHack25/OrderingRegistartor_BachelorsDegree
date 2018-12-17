using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrderingRegistrator.Tools
{
    class DateManager // კლასი პასუხისმგებელია პროგრამაში აკონტროლოს Date ტიპის ცვლადები და ასევე მომხმარებლის კონტროლი მათი სწორი შეყვანის მიზნით
    {
        private static Regex dateRegExp = new Regex(@"^(((0?[1-9]|[12]\d|3[01])[\.\-\/](0?[13578]|1[02])[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}|\d))|((0?[1-9]|[12]\d|30)[\.\-\/](0?[13456789]|1[012])[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}|\d))|((0?[1-9]|1\d|2[0-8])[\.\-\/]0?2[\.\-\/]((1[6-9]|[2-9]\d)?\d{2}|\d))|(29[\.\-\/]0?2[\.\-\/]((1[6-9]|[2-9]\d)?(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)|00|[048])))$"); // Date ტიპის ამომცნობი რთული Regex ცვლადი 


        // DateTime ცვლადები
        private static DateTime OrderCreationDate; // გამოწერის თარიღი
        private static DateTime OrderReachedBoxOffice; // მიაღწია ამანათმა *ტრანსპორტერი კომპანიის მისამართს უცხოეთში...
        private static DateTime OrderToGeo; // საქართველოში ჩამოსვლის თარიღი

        public static TextBox txtbxordcreatDate;
        public static TextBox txtbxArrAtbxOffice;
        public static TextBox deliverAtGeo;

        // Validate DateBoxes - ფუნქცია CancelEventArgs -ების ხარჯზე ამოწმებს , მომხმარებელმა სწორად შეიყვანა თუ არა თარიღი TextBox-ებში //
        public static bool textboxesValidator(object sender)
        {
            CancelEventArgs cancelArg = new CancelEventArgs();
            DateTime cDate;
            DateTime arriv;
            DateTime delivery;

            Match createDate = dateRegExp.Match(txtbxordcreatDate.Text);
            Match arrivalOffice = dateRegExp.Match(txtbxArrAtbxOffice.Text);
            Match deliveryGeo = dateRegExp.Match(deliverAtGeo.Text);

            cancelArg.Cancel = (createDate.Success != true) ? true : false; // თუ შეცდომაა თარიღში, მაშინ  cancelArg.Cancel = True 

            if (!(cancelArg.Cancel)) // if No Error
            {
                cDate = Convert.ToDateTime(txtbxordcreatDate.Text);
                cancelArg.Cancel = (cDate > DateTime.Today) ? true : false;
                txtbxordcreatDate_Validating(sender, cancelArg);
                if (cancelArg.Cancel) return false;
            }

            else
            {
                txtbxordcreatDate_Validating(sender, cancelArg);
                return false;
            }

            cancelArg.Cancel = (arrivalOffice.Success != true) ? true : false;
            if (!(cancelArg.Cancel))
            {
                arriv = Convert.ToDateTime(txtbxArrAtbxOffice.Text);
                cDate = Convert.ToDateTime(txtbxordcreatDate.Text);
                cancelArg.Cancel = (arriv > DateTime.Today || arriv < cDate) ? true : false;
                txtbxArrAtbxOffice_Validating(sender, cancelArg);
                if (cancelArg.Cancel) return false;
            }
            else { txtbxArrAtbxOffice_Validating(sender, cancelArg);return false; }        
           
            cancelArg.Cancel = (deliveryGeo.Success != true) ? true : false;

            if (!(cancelArg.Cancel))
            {
                cDate = Convert.ToDateTime(txtbxordcreatDate.Text);
                delivery = Convert.ToDateTime(deliverAtGeo.Text);
                arriv = Convert.ToDateTime(txtbxArrAtbxOffice.Text);
                cancelArg.Cancel = (delivery > DateTime.Today || delivery < cDate || delivery < arriv) ? true : false;
                deliverAtGeo_Validating(sender, cancelArg);
                if (cancelArg.Cancel) return false;
            }
            else { deliverAtGeo_Validating(sender, cancelArg); return false; }
            return true;
        }

        // Validations - თუ ამ ფუნქციებს გადაეცემა e.Cancel = true , მაშინ არ მოხდება მოცემული TextBox-ის ვალიდაცია... //
        private static void txtbxordcreatDate_Validating(object sender, CancelEventArgs e)
        {
            if (e.Cancel)
            {
                txtbxordcreatDate.ForeColor = Color.Red;
                MessageBox.Show("დაფიქსირდა შეცდომა... გადაამოწმეთ შეყვანილი თარიღის ფორმატი: dd/mm/yyyy", "შეკვეთის თარიღი");
            }
            else txtbxordcreatDate.ForeColor = Color.Black;
        }
        private static void txtbxArrAtbxOffice_Validating(object sender, CancelEventArgs e)
        {
            if (e.Cancel)
            {
                txtbxArrAtbxOffice.ForeColor = Color.Red;
                MessageBox.Show("დაფიქსირდა შეცდომა... გადაამოწმეთ შეყვანილი თარიღის ფორმატი: dd/mm/yyyy", "უცხოეთის საწყობში მისვლის თარიღი");
            }
            else txtbxArrAtbxOffice.ForeColor = Color.Black;
        }
        private static void deliverAtGeo_Validating(object sender, CancelEventArgs e)
        {
            if (e.Cancel)
            {
                deliverAtGeo.ForeColor = Color.Red;
                MessageBox.Show("დაფიქსირდა შეცდომა... გადაამოწმეთ შეყვანილი თარიღის ფორმატი: dd/mm/yyyy", "ამანათის ჩამოსვლის თარიღი");
            }
            else deliverAtGeo.ForeColor = Color.Black;
        }

        // ფუნქცია აბრუნებს string ცვლადს მთლიანად დახარჯული დროის შესახებ: [გამოწერიდან - გატანის ჩათვლით გასული დროის შესახებ]
        public static string getProcessingTime()
        {  
                OrderCreationDate = DateTime.ParseExact(txtbxordcreatDate.Text.Replace("/","."), @"dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture);
                OrderReachedBoxOffice = DateTime.ParseExact(txtbxArrAtbxOffice.Text.Replace("/", "."), @"dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture);
                OrderToGeo = DateTime.ParseExact(deliverAtGeo.Text.Replace("/", "."), @"dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture);
                return (OrderToGeo - OrderCreationDate).TotalDays.ToString() + " Days";
        }
        public static string getCurrentTime() // მიმდინარე თარიღი
        {
            return DateTime.Now.ToString("dd/MM/yyyy");
        }
    }
}
