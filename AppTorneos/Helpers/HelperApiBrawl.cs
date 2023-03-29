using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace AppTorneos.Helpers
{
    public class HelperApiBrawl
    {
        string enlaceJugadores = "https://api.brawlstars.com/v1/players/%23";

        public async Task<bool> Prueba(string idjugador)
        {
            HttpClient cliente = new HttpClient();

            cliente.DefaultRequestHeaders.Add("accept", "application/json");
            cliente.DefaultRequestHeaders.Add("authorization", "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzUxMiIsImtpZCI6IjI4YTMxOGY3LTAwMDAtYTFlYi03ZmExLTJjNzQzM2M2Y2NhNSJ9.eyJpc3MiOiJzdXBlcmNlbGwiLCJhdWQiOiJzdXBlcmNlbGw6Z2FtZWFwaSIsImp0aSI6IjJlNTJjMTliLWRjZTgtNDIzMi05NGE0LTI5YzU1OTVhZWY0ZCIsImlhdCI6MTY4MDEwMDc4Mywic3ViIjoiZGV2ZWxvcGVyLzQ4ZWJlYzNiLWQ3YjEtMmMzNi02MmQ4LTI0YzJkN2Y1ODgyYSIsInNjb3BlcyI6WyJicmF3bHN0YXJzIl0sImxpbWl0cyI6W3sidGllciI6ImRldmVsb3Blci9zaWx2ZXIiLCJ0eXBlIjoidGhyb3R0bGluZyJ9LHsiY2lkcnMiOlsiOTUuNjAuMTMwLjY3Il0sInR5cGUiOiJjbGllbnQifV19.zZAd8ACXJG2cDAGEFS67Vpy_br0ZCGYcBCXIYti1bObI9hKi6uUl-B7cfBgEV5O3_EoLnxqy4xUI5l_rQkSmUw");

            string enlaceSolicitud = $"{enlaceJugadores}{idjugador}";

            try
            {
                var playerresponse = await cliente.GetStringAsync("https://api.brawlstars.com/v1/players/%23" + idjugador);
                JObject json = JObject.Parse(playerresponse);

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }


            //JObject json = JObject.Parse(playerresponse);

            //if (json.ContainsKey("reason") ==  false) 
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}

            return true;
        }
    }
}
