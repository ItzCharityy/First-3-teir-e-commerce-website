using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using st1.ServiceReference1;

namespace st1
{
    public partial class InvoiceManagement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindInvoices();
            }
        }

        // Bind invoices to the repeater control
        private void BindInvoices()
        {
            using (Service1Client client = new Service1Client())
            {
                var invoices = client.GetUserInvoices(/*(int)Session["UserID"]*/14); // Fetch invoices for the current user
                InvoiceRepeater.DataSource = invoices;
                InvoiceRepeater.DataBind();
            }
        }

        // Handle Delete Invoice button click
        protected void DeleteInvoice_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int invoiceId = Convert.ToInt32(btn.CommandArgument);

            using (Service1Client client = new Service1Client())
            {
                bool success = client.DeleteInvoice(invoiceId);

                if (success)
                {
                    BindInvoices(); // Refresh the list
                }
                else
                {
                    // Show an error message
                }
            }
        }

        // Handle Download PDF button click
        protected void DownloadInvoice_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int invoiceId = Convert.ToInt32(btn.CommandArgument);

            using (Service1Client client = new Service1Client())
            {
                var invoice = client.GetInvoiceById(invoiceId);

                if (invoice != null)
                {
                    // Generate PDF
                    GenerateInvoicePDF(invoice);
                }
            }
        }

        // Generate the PDF invoice using iTextSharp
        private void GenerateInvoicePDF(dynamic invoice)
        {
            Document pdfDoc = new Document(PageSize.A4);
            MemoryStream ms = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, ms);
            pdfDoc.Open();

            // Add invoice details to the PDF
            pdfDoc.Add(new Paragraph($"Invoice ID: {invoice.InvoiceID}"));
            pdfDoc.Add(new Paragraph($"Date: {invoice.InvoiceDate:dd/MM/yyyy}"));
            pdfDoc.Add(new Paragraph($"Total Amount: R {invoice.TotalAmount:F2}"));
            pdfDoc.Add(new Paragraph($"Status: {invoice.Status}"));
            pdfDoc.Add(new Paragraph("\n"));

            // Example of adding more data to the PDF
            pdfDoc.Add(new Paragraph("Thank you for your business!"));

            pdfDoc.Close();

            // Download the PDF file
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=Invoice_" + invoice.InvoiceID + ".pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.BinaryWrite(ms.ToArray());
            Response.End();
        }
    }
}