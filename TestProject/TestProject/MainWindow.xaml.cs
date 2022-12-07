using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<DetaislModel> detaislModels = new List<DetaislModel>();
        CSVHelper CSV = new CSVHelper();
        PdfHelper helper = new PdfHelper();
        string folderPath = @"D:\Pravin\Project\practice";
        public MainWindow()
        {
            InitializeComponent();
            DGDetails.ItemsSource = CSV.ReadCSVForDetails(folderPath);

        }

        private void BtnDownload_Click(object sender, RoutedEventArgs e)
        {
            DetaislModel data =(DetaislModel)((Button)e.Source).DataContext;
            string fileName = folderPath + "\\"+data.TestId+".csv";
            string pdfFileName = folderPath + "\\" + data.TestId + ".pdf";
            var pdfDetails=CSV.GetPdfDetails(fileName);
            //here pass the file name to pdf generation method 
            helper.GeneratePdf(pdfDetails, pdfFileName);
        }
    }
}
