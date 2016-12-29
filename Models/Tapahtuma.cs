/*{
	"TapahtumaID": 2913,
	"TapahtumaNimi": "UUDENVUODENJUOKSU",
	"Ajankohta": "31.12.2016",
	"TapahtumaPaiva": "\/Date(1483135200000)\/",
	"TapahtumaPostitoimipaikka": "Hyllykallio",
	"IlmoittautumisLinkki": "",
	"TapahtumaLinkki": "\u003ca class=\u0027link\u0027 target=\u0027_blank\u0027 href=\u0027http://www.nurmonjymy.sporttisaitti.com\u0027\u003eTapahtuman verkkosivut\u003c/a\u003e",
	"Maraton": false,
	"Puolimaraton": false,
	"Km10": false,
	"Km5": false,
	"Muu": true
}*/

namespace WebAPIApplication.Models
{
    public class Tapahtuma
    {
        public int TapahtumaID { get; set; }
        public string TapahtumaNimi { get; set; }
        public string Ajankohta { get; set; }
        public string TapahtumaPaiva { get; set; }
        public string TapahtumaPostitoimipaikka { get; set; }
        public string IlmoittautumisLinkki { get; set; }
        public string TapahtumaLinkki { get; set; }
        public bool Maraton { get; set; }
        public bool Puolimaraton { get; set; }
        public bool Km10 { get; set; }
        public bool Km5 { get; set; }
        public bool Muu { get; set; }
    }
}
