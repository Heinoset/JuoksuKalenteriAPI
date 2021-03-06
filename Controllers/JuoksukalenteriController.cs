using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using WebAPIApplication.Models;

namespace WebAPIApplication.Controllers
{
    /// <summary>
    /// Controller for Juoksukalenteri API functions
    /// </summary>
    [Route("api/[controller]")]    
    public class JuoksukalenteriController : Controller
    {
        private readonly JuoksukalenteriApiSettings _optionsAccessor;

        public JuoksukalenteriController(IOptions<JuoksukalenteriApiSettings> optionsAccessor)
        {
            _optionsAccessor = optionsAccessor.Value;
        }

        [HttpGet]
        public IEnumerable<Tapahtuma> Get(string laji, string location, string matka, DateTime alku, DateTime loppu, string query)
        {
            string alkuStr = string.Empty;
            string loppuStr = string.Empty;

            if(string.IsNullOrEmpty(laji)) { laji = "kaikki"; }
            if(string.IsNullOrEmpty(location)) { location = string.Empty; } 
            if(string.IsNullOrEmpty(matka)) { matka = "kaikki"; } 
            if(alku != null && DateTime.MinValue != alku) { alkuStr = alku.ToString("dd.MM.yyyy"); } 
            if(loppu != null && DateTime.MinValue != loppu) { loppuStr = loppu.ToString("dd.MM.yyyy"); } 
            if(string.IsNullOrEmpty(query)) { query = string.Empty; } 

            HttpClient client = new HttpClient();
            Uri serviceUrl = 
            new Uri(string.Format("{0}?&type=search&format=json&culture=&laji={1}&location={2}&matka={3}&start={4}&end={5}&q={6}",
            _optionsAccessor.ApiUrl,
            laji,
            location,
            matka,
            alkuStr,
            loppuStr,
            query
            ));

            Console.WriteLine(serviceUrl.ToString());

            HttpResponseMessage results = client.GetAsync(serviceUrl).Result;
            string json = results.Content.ReadAsStringAsync().Result;

            List<Tapahtuma> tapahtumaList = JsonConvert.DeserializeObject<List<Tapahtuma>>(json);
            return tapahtumaList;
        }

        [HttpGet("{eventid}")]
        public string Get(int eventid)
        {
            HttpClient client = new HttpClient();
            Uri serviceUrl = new Uri(
                string.Format("http://www.juoksija-lehti.fi/Tapahtumat/Juoksukalenteri.aspx?&type=search&culture=&id={0}",
                eventid
            ));

            HttpResponseMessage results = client.GetAsync(serviceUrl).Result;
            string html = results.Content.ReadAsStringAsync().Result;
            
            return html;
        }
    }
}
