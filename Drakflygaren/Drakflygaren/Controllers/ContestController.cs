using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Drakflygaren.Controllers
{
    public class ContestController : Controller
    {
        // GET: Contest
        public ActionResult Index()
        {
            return View();
        }

        public FileResult CreatePdf()
        {
            DateTime dTime = DateTime.Now;
            MemoryStream workStream = new MemoryStream();
            string PdfFileName = string.Format("Reglerna" + dTime.ToShortDateString() + "-" + ".pdf");

            Document doc = new Document();
            doc.SetMargins(0f, 0f, 0f, 0f);

            //file will be created in this path  
            string strAttachment = Server.MapPath("~/Downloads/" + PdfFileName);
            PdfWriter.GetInstance(doc, workStream).CloseStream = false;

            try
            {
                doc.Open();

                //Setting header color to yellow
                BaseColor color = new BaseColor(255 - 228 - 181);

                //Stating rules
                string point1 = @"1. Drakar trädde i konkurrensen måste inte vara hemlagad, men inte nödvändigtvis av den person som flyger dem. Drakar sammansatta av kommersiella kit kommer inte att beaktas hem.";
                string point2 = @"2. Tillverkade drakar endast från Drakmästare Nitin är berättigade till tävlingen.";
                string point3 = @"3. Tävlande får endast ange en Drake per händelse. De kan dock komma in varje händelse med en annan drake för varje händelse.";
                string point4 = @"4. Drakar måste flyga för att kvalificera sig för att bedöma.";
                string point5 = @"5. Utmärkelserna kommer att ges till första, andra och tredje plats vinnare i varje fall.";
                string point6 = @"6. Domare Calle Sandströms beslut är slutgiltiga.";
                string point7 = @"7. Tävlande måste registrera & kontakta med Daniel Bjurström innan tävlingen och visa deras antal vid alla tidpunkter.Sara & Rickard kan hjälpa barn med sina drakar.";

                point1 = point1.Replace(Environment.NewLine, String.Empty).Replace("  ", String.Empty);

                //heading with underline
                Chunk header = new Chunk("Drakflygningen Tävlingsregler", FontFactory.GetFont("Helvetica-black", 12, 1));
                header.SetUnderline(0.5f, -1.5f);
                header.SetBackground(color);

                doc.Add(new Phrase(header));

                doc.Add(new Paragraph(point1));
                doc.Add(new Paragraph(point2));
                doc.Add(new Paragraph(point3));
                doc.Add(new Paragraph(point4));
                doc.Add(new Paragraph(point5));
                doc.Add(new Paragraph(point6));
                doc.Add(new Paragraph(point7));

            }
            catch (DocumentException dex)
            {
                throw (dex);
            }
            catch (IOException ioex)
            {
                throw (ioex);
            }
            finally
            {
                doc.Close();
            }
            byte[] byteInfo = workStream.ToArray();
            workStream.Write(byteInfo, 0, byteInfo.Length);
            workStream.Position = 0;

            return File(workStream, "application/pdf", PdfFileName);

        }
    }
}