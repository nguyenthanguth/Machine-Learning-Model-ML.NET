using RestSharp;
using System;
using System.Windows.Forms;
using System.Xml;

namespace ThaiDanh
{
    public class ConvertNumberToWord
    {
        public static string GetWords(object number)
        {
            string result = "";
            try
            {
                var client = new RestClient("https://clevert.com.br/t/en/numbers_to_words/generate");
                var request = new RestRequest();
                request.Method = Method.Post;

                var body = $@"number={number}&currency=&numLanguage=vi&segundoFormato=";
                request.AddParameter("application/x-www-form-urlencoded", body, ParameterType.RequestBody);
                RestResponse response = client.Execute(request);
                string content = response.Content;
                result = ExtractNumbersToWordsUsingXml(content);
            }
            catch (Exception e)
            {
                MessageBox.Show("Lỗi khi chuyển đổi từ số tiền thành chữ", e.Message);
            }
            return result;
        }

        private static string ExtractNumbersToWordsUsingXml(string html)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml("<root>" + html + "</root>");

                XmlNode spanNode = xmlDoc.SelectSingleNode("//span[@id='numbersToWords']");

                if (spanNode != null)
                {
                    return spanNode.InnerText.Trim();
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu có
                Console.WriteLine("Error: " + ex.Message);
                return string.Empty;
            }
        }

    }
}
