using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OMGNEW_API.Dto.GetData;
using RestSharp;
using System;
using System.Collections.Generic;
using Windows_Service.Entities;

namespace Windows_Service.Operations
{
    public static class Operation
    {

        public static int?[] dokumNos = new int?[] { null, null, null, null };

        public static void SendDokum()
        {
            try
            {
                using (OmgContext db = new OmgContext())
                {
                    var client = new RestClient("https://localhost:7189/api/getpages");
                    var request = new RestRequest(Method.GET);
                    var response = client.Execute(request);

                    List<GetPagesResponse> pages = JsonConvert.DeserializeObject<List<GetPagesResponse>>(response.Content);

                    foreach (var page in pages)
                    {
                        int pageId = page.page_id;

                        var clientData = new RestClient("https://localhost:7189/api/getdatas");
                        var requestData = new RestRequest(Method.POST);
                        var jsonBody = new { page = pageId };
                        requestData.AddJsonBody(jsonBody);
                        var responseData = clientData.Execute(requestData);
                        var responseDataList = JsonConvert.DeserializeObject<List<ResponseData>>(responseData.Content);

                        // İlgili döküm numarasını al
                        //Hafızaya Bilgileri Al
                        string dokumNoKey = (pageId == 1 || pageId == 2) ? "DOKUM NO" : "LF DOKUM NO";
                        if (dokumNos[pageId - 1] == null)
                        {
                            dokumNos[pageId - 1] = Convert.ToInt32(responseDataList.Find(d => d.data_title == dokumNoKey)?.data_value);
                        }

                        //Güncel Datalar İle Kontrol Et
                        var clientDataG = new RestClient("https://localhost:7189/api/getdatas");
                        var requestDataG = new RestRequest(Method.POST);
                        var jsonBodyG = new { page = pageId };
                        requestDataG.AddJsonBody(jsonBodyG);
                        var responseDataG = clientData.Execute(requestData);
                        var responseDataListG = JsonConvert.DeserializeObject<List<ResponseData>>(responseData.Content);
                        var GetDokum = Convert.ToInt32(responseDataListG.Find(d => d.data_title == dokumNoKey)?.data_value);

                        if (GetDokum != dokumNos[pageId - 1])
                        {
                            var dokumNoData = responseDataListG.Find(d => d.data_title == dokumNoKey);
                            if (dokumNoData != null)
                            {
                                dokumNoData.data_value = dokumNos[pageId - 1];
                                string jsonString = JsonConvert.SerializeObject(responseDataListG);
                                string encodedJson = JsonConvert.SerializeObject(jsonString);
                                using (OmgContext success = new OmgContext())
                                {
                                    var GetError = success.OErrors.FirstOrDefault(x => x.PageId == pageId);
                                    var newAddData = new OData()
                                    {
                                        DokumNo = dokumNos[pageId - 1],
                                        PageId = pageId,
                                        DataJson = encodedJson,
                                        GenelAriza = (GetError == null) ? 0 : GetError.GenelAriza,
                                        MekanikAriza = (GetError == null) ? 0 : GetError.MekanikAriza,
                                        ElektrikAriza = (GetError == null) ? 0 : GetError.ElektrikAriza,
                                        IsletmeAriza = (GetError == null) ? 0 : GetError.IsletmeAriza,
                                    };
                                    success.ODatas.Add(newAddData);
                                    if (GetError != null)
                                    {
                                        success.Remove(GetError);
                                    }
                                    success.SaveChanges();
                                }
                                dokumNos[pageId - 1] = GetDokum;
                            }
                        }



                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    public class ResponseData
    {
        public string data_title { get; set; }
        public object data_value { get; set; }
        public string data_description { get; set; }
        public bool data_error { get; set; }
    }

    public class GetPagesResponse
    {
        public int page_id { get; set; }
    }
}
