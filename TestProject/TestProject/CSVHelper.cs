using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class CSVHelper
    {

        internal IEnumerable<DetaislModel> ReadCSVForDetails(string FolderDetails)
        {
            string[] files = GetFiles(FolderDetails);
            List<DetaislModel> details = new List<DetaislModel>();
            foreach (var fileName in files)
            {
                string[] lines = File.ReadAllLines(System.IO.Path.ChangeExtension(fileName, ".csv"));

                details.Add(GetgenericInfo(lines));
            }
            return details;
        }

        private static DetaislModel GetgenericInfo(string[] lines)
        {
            return new DetaislModel()
            {
                DeviceName = lines[0].Split(',')[0],
                TestId = lines[1].Split(',')[1],
                CustomerName = lines[2].Split(',')[1],
                CompanyName = lines[3].Split(',')[1],
                DateTime=lines[4].Split(',')[1]
            };
        }

        private string[] GetFiles(string folderDetails)
        {
            return Directory.GetFiles(folderDetails, "*.csv",
                                         SearchOption.TopDirectoryOnly);
        }

        internal pdfDetailsModel GetPdfDetails(string fileName)
        {
            pdfDetailsModel pdfDetailsModel = new pdfDetailsModel();
            string[] lines = File.ReadAllLines(System.IO.Path.ChangeExtension(fileName, ".csv"));

            //get the generic information
            pdfDetailsModel.Genricinformation = GetgenericInfo(lines);
            //get the table information
            pdfDetailsModel.TableDetails = GetTableDetails(lines);
            return pdfDetailsModel;

        }

        private DataTable GetTableDetails(string[] lines)
        {
            DataTable dataTable = new DataTable();
            //get columns names
            var colunmNames=lines[6].Split(',');
            foreach (var name in colunmNames)
            {
                dataTable.Columns.Add(name);
            }

            //get rows data and add to datatable rows.
            for (int i = 7; i < lines.Count(); i++)
            {
                var rowData = lines[i].Split(',');
                dataTable.Rows.Add(rowData);

            }
            return dataTable;
            
        }
    }
}
