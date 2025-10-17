using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using InterviewGeneratorBlazorHybrid.Data;
using Microsoft.EntityFrameworkCore;

namespace InterviewGeneratorBlazorHybrid.Helpers
{
    public class MSWordHelper
    {
        private readonly AppDbContextFactory _contextFactory;
        public MSWordHelper(AppDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }
       
        public void GenerateInterviewDoc(int interviewId)
        {
            using var context = _contextFactory.CreateDbContext();
            var interview = context.Interviews
                .Include(i => i.Questions)
                .ThenInclude(q => q.Category)
                .FirstOrDefault(i => i.Id == interviewId);

            string templatePath = @"c:\temp\Interview Guide Template.docx";
            string outputPath = @"c:\temp\Interview Guide Output.docx";

            File.Copy(templatePath, outputPath, true);

            using (var doc = WordprocessingDocument.Open(outputPath, true))
            {

                var body = doc.MainDocumentPart!.Document.Body!;
                foreach (var text in body.Descendants<Text>())
                {
                    if (text.Text.Contains("{**InterviewGuideName**}"))
                        text.Text = text.Text.Replace("{**InterviewGuideName**}", interview.InterviewName);
                }

                // Add to the table that is there
                Table table = body.Elements<Table>().FirstOrDefault();
                foreach (var question in interview.Questions)
                {
                    if (table != null)
                    {
                        TableRow newRow = new TableRow(
                            new TableCell(new Paragraph(new Run(new Text($"{question.Text} ({question.Category.Name})")))),
                            new TableCell(new Paragraph(new Run(new Text($""))))

                        );
                        table.AppendChild(newRow);
                    }
                }

                doc.MainDocumentPart.Document.Save();
            }
        }
    }
}
