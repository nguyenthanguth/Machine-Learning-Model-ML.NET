using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;

using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApp2
{
    public class GoogleSheet
    {
        public GoogleSheet() { }
        public GoogleCredential credential;
        public SheetsService sheetsService;
        public string SpreadSheetID { get; set; }

        public string SheetName { get; set; }
        public string SheetID { get; set; }

        public string RangeDefault = "A1:Z1000";

        public void ConnectJsonCredentials(string path_to_file)
        {
            using (FileStream stream = new FileStream(path_to_file, FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream).CreateScoped(SheetsService.Scope.Spreadsheets);
            }

            BaseClientService.Initializer baseClientService = new BaseClientService.Initializer();
            baseClientService.HttpClientInitializer = credential;

            sheetsService = new SheetsService(baseClientService);
        }

        public List<KeyValuePair<string, int?>> GetInfoAllSheet()
        {
            List<KeyValuePair<string, int?>> ListSheetName = new List<KeyValuePair<string, int?>>();

            SpreadsheetsResource.GetRequest getRequest = sheetsService.Spreadsheets.Get(SpreadSheetID);
            Spreadsheet spreadsheet = getRequest.Execute();

            foreach (Sheet sheet in spreadsheet.Sheets)
            {
                SheetProperties sheetProperties = sheet.Properties;
                KeyValuePair<string, int?> keyPair = new KeyValuePair<string, int?>(sheetProperties.Title, sheetProperties.SheetId);
                ListSheetName.Add(keyPair);
            }

            return ListSheetName;
        }

        public void Clear(string sheet_name)
        {
            string rangeSheet = sheet_name + "!" + RangeDefault;

            ClearValuesRequest clearRequest = new ClearValuesRequest();
            SpreadsheetsResource.ValuesResource.ClearRequest clear = sheetsService.Spreadsheets.Values.Clear(clearRequest, SpreadSheetID, rangeSheet);
            ClearValuesResponse clearResponse = clear.Execute();
        }

        public void InsertContent(string sheet_name, List<IList<object>> values)
        {
            string rangeSheet = sheet_name + "!" + RangeDefault;

            ValueRange valueRange = new ValueRange();
            valueRange.Values = values;

            SpreadsheetsResource.ValuesResource.UpdateRequest updateRequest = sheetsService.Spreadsheets.Values.Update(valueRange, SpreadSheetID, rangeSheet);
            updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW;

            UpdateValuesResponse updateResponse = updateRequest.Execute();
        }

        public void CreateSheet(string sheet_name)
        {
            SheetProperties sheetProperties = new SheetProperties();
            sheetProperties.Title = sheet_name;

            AddSheetRequest addSheetRequest = new AddSheetRequest();
            addSheetRequest.Properties = sheetProperties;

            Request request = new Request();
            request.AddSheet = addSheetRequest;

            List<Request> listRequest = new List<Request>();
            listRequest.Add(request);

            BatchUpdateSpreadsheetRequest batchUpdateRequest = new BatchUpdateSpreadsheetRequest();
            batchUpdateRequest.Requests = listRequest;

            SpreadsheetsResource.BatchUpdateRequest batchUpdate = sheetsService.Spreadsheets.BatchUpdate(batchUpdateRequest, SpreadSheetID);
            BatchUpdateSpreadsheetResponse batchUpdateResponse = batchUpdate.Execute();
        }

        public void PostData(string sheet_name, List<IList<object>> list_list_Obj)
        {
            List<KeyValuePair<string, int?>> listPairs = this.GetInfoAllSheet();
            if (!listPairs.Any(a => a.Key == sheet_name))
            {
                this.CreateSheet(sheet_name);
            }

            this.Clear(sheet_name);
            this.InsertContent(sheet_name, list_list_Obj);
        }
    }

    public class CredentialJson
    {
        public string type { get; set; }
        public string project_id { get; set; }
        public string private_key_id { get; set; }
        public string private_key { get; set; }
        public string client_email { get; set; }
        public string client_id { get; set; }
        public string auth_uri { get; set; }
        public string token_uri { get; set; }
        public string auth_provider_x509_cert_url { get; set; }
        public string client_x509_cert_url { get; set; }
        public string universe_domain { get; set; }
    }
}
