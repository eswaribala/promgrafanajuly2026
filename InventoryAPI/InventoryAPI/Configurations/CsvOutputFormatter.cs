using InventoryAPI.Models;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Text;

namespace InventoryAPI.Configurations
{
    public class CsvOutputFormatter : TextOutputFormatter
    {
        public CsvOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/csv"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        protected override bool CanWriteType(Type? type)
            => typeof(Category).IsAssignableFrom(type)
                || typeof(IEnumerable<Category>).IsAssignableFrom(type);

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;
            var buffer = new StringBuilder();

            if (context.Object is IEnumerable<Category>)
            {
                foreach (var Category in (IEnumerable<Category>)context.Object)
                {
                    FormatCsv(buffer, Category);
                }
            }
            else
            {
                FormatCsv(buffer, (Category)context.Object);
            }

            await response.WriteAsync(buffer.ToString(), selectedEncoding);
        }

        private static void FormatCsv(StringBuilder buffer, Category category)
        {
            
                buffer.AppendLine($"{category.CategoryId},\"{category.CategoryName}");
            
        }
    }
}
