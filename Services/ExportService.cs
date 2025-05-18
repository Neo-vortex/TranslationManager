using ClosedXML.Excel;
using TranslationManager.Models;
using YamlDotNet.Serialization;

namespace TranslationManager.Services;

public class ExportService
{
    public byte[] ExportToExcel(IEnumerable<Translation> translations)
    {
        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Translations");

        worksheet.Cell(1, 1).Value = "Key";
        var cultures = translations
            .SelectMany(t => t.Values.Select(v => v.Culture))
            .Distinct()
            .OrderBy(c => c)
            .ToList();

        for (int i = 0; i < cultures.Count; i++)
        {
            worksheet.Cell(1, i + 2).Value = cultures[i];
        }

        // Data
        int row = 2;
        foreach (var translation in translations.OrderBy(t => t.Key))
        {
            worksheet.Cell(row, 1).Value = translation.Key;
            
            for (int i = 0; i < cultures.Count; i++)
            {
                var value = translation.Values.FirstOrDefault(v => v.Culture == cultures[i]);
                worksheet.Cell(row, i + 2).Value = value?.Value ?? string.Empty;
            }
            
            row++;
        }

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }

    public string ExportToJson(IEnumerable<Translation> translations)
    {
        var result = new Dictionary<string, Dictionary<string, string>>();
        
        foreach (var translation in translations)
        {
            var translationsDict = new Dictionary<string, string>();
            foreach (var value in translation.Values)
            {
                translationsDict[value.Culture] = value.Value;
            }
            result[translation.Key] = translationsDict;
        }

        return System.Text.Json.JsonSerializer.Serialize(result, new System.Text.Json.JsonSerializerOptions
        {
            WriteIndented = true
        });
    }

    public string ExportToYaml(IEnumerable<Translation> translations)
    {
        var result = new Dictionary<string, Dictionary<string, string>>();
        
        foreach (var translation in translations)
        {
            var translationsDict = new Dictionary<string, string>();
            foreach (var value in translation.Values)
            {
                translationsDict[value.Culture] = value.Value;
            }
            result[translation.Key] = translationsDict;
        }

        var serializer = new SerializerBuilder().Build();
        return serializer.Serialize(result);
    }
}