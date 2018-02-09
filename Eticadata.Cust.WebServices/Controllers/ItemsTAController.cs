using System;
using System.Reflection;
using System.Web.Http;

namespace Eticadata.Cust.WebServices.Controllers
{
    public class ItemsTAController : ApiController
    {
        private Models.myItem GetNewItem()
        {
            Models.myItem item = new Models.myItem()
            {
                Code = "ART2",
                Category = "1",
                Description = "Artigo 2",
                Abbreviation = "AR 2",
                VATRateSale = 3,
                VATRatePurchase = 3,
                MeasureOfStock = "UN",
                MeasureOfSale = "UN",
                MeasureOfPurchase = "UN",
            };

            return item;
        }


        [HttpPost]
        [Authorize]
        public IHttpActionResult GenerateItem([FromBody] Models.myItem pItem)
        {
            var items = Eti.Aplicacao.Tabelas.Artigos.Clone();
            var errorDescription = "";

            try
            {                
                //pItem = GetNewItem();

                var item = Eti.Aplicacao.Tabelas.Artigos.Find(pItem.Code);

                item.CodigoCategoria = pItem.Category;
                item.Descricao = pItem.Description;
                item.Abreviatura = pItem.Abbreviation;

                item.CodigoTaxaIvaVenda = pItem.VATRateSale;
                item.CodigoTaxaIvaVenda2 = pItem.VATRateSale;
                item.CodigoTaxaIvaCompra = pItem.VATRatePurchase;

                item.AbrevMedStk = pItem.MeasureOfStock;
                item.AbrevMedVnd = pItem.MeasureOfSale;
                item.AbrevMedCmp = pItem.MeasureOfPurchase;

                item.NaoAfectaIntrastat = true;

                if (item.IsNew)
                {
                    item.CodigoInterno = Eti.Aplicacao.Tabelas.Artigos.DaCodigoInterno();
                }

                if (item.Validate())
                {
                    items.Update(ref item);
                }

                if (item.EtiErrorDescription != "")
                {
                    errorDescription = $"Erro ao criar o artigo [{item.Codigo} - {item.Descricao}]: {item.EtiErrorDescription}";
                    throw new Exception(errorDescription);
                }
            }
            catch (Exception ex)
            {
                errorDescription = string.Format("{0}.{1}.{2}", MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, ex.Message);
                return BadRequest(errorDescription);
            }

            return Ok("");
        }
       
    }
}