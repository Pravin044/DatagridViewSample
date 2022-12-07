using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
   public class PdfHelper
    {
        private Table table;

        internal void GeneratePdf(pdfDetailsModel pdfDetails,string filename)
        {
            Document maiDoc = new Document();
            Paragraph paragraph = new Paragraph();
            var GenInfo = pdfDetails.Genricinformation;
            paragraph.AddText("Device Name: " + GenInfo.DeviceName);
            paragraph.AddLineBreak();
            paragraph.AddText("Test Id: " + GenInfo.TestId);
            paragraph.AddLineBreak();
            paragraph.AddText("Customer Name: " + GenInfo.CustomerName);
            paragraph.AddLineBreak();
            paragraph.AddText("Company Name: " + GenInfo.CompanyName);
            paragraph.AddLineBreak();
            paragraph.AddText("Date Time: " + GenInfo.DateTime);
            paragraph.AddLineBreak();
           
            Section section = maiDoc.AddSection();
            section.Add(paragraph);
            // Create the item table
            this.table = section.AddTable();
            this.table.Style = "Table";
            //this.table.Borders.Color = TableBorder;
            this.table.Borders.Width = 0.25;
            this.table.Borders.Left.Width = 0.5;
            this.table.Borders.Right.Width = 0.5;
            this.table.Rows.LeftIndent = 0;
            for (int i = 0; i < pdfDetails.TableDetails.Columns.Count; i++)
            {
                // Before you can add a row, you must define the columns
                Column column = this.table.AddColumn("2cm");
                column.Format.Alignment = ParagraphAlignment.Center;
            }

            //Columns
            Row ColumnsNames = table.AddRow();
            for (int i = 0; i < pdfDetails.TableDetails.Columns.Count; i++)
            {
                ColumnsNames.Cells[i].AddParagraph(pdfDetails.TableDetails.Columns[i].ColumnName);
            }

            //rows
            foreach (DataRow item in pdfDetails.TableDetails.Rows)
            {
                Row rowDetails = table.AddRow();
                for (int i = 0; i < item.ItemArray.Count(); i++)
                {
                    rowDetails.Cells[i].AddParagraph(item.ItemArray[i].ToString());
                }
            }


            const PdfFontEmbedding embedding = PdfFontEmbedding.Always;

            // ========================================================================================

            // Create a renderer for the MigraDoc document.
            PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer();

            // Associate the MigraDoc document with a renderer
            pdfRenderer.Document = maiDoc;

            // Layout and render document to PDF
            pdfRenderer.RenderDocument();

            // Save the document...
            pdfRenderer.PdfDocument.Save(filename);
            // ...and start a viewer.
            Process.Start(filename);
        }


    }
}
