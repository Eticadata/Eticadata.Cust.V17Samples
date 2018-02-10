using Eticadata.ERP;
using Eticadata.ERP.EtiEnums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using static Eticadata.ERP.Etiquetas;

namespace Eticadata.Cust.WebServices.Models.Utilities
{
    public class PrintLabelInput
    {
        public TpEtiqueta bytLableType { get; set; }

        public EtiqTpEmissao docType { get; set; }

        public TpEmissaoPorDocOuLin bytTpEmissaoPorDocOuLinha { get; set; }

        public TpNumCopias copiesType { get; set; }

        public bool toPrint { get; set; }

        public int columnPosition { get; set; }

        public TpUnidade measureType { get; set; }

        public bool blnFiltroComTpNivel { get; set; }

        public int linePosition { get; set; }

        public int Copies { get; set; }

        public bool chkUsaQtdMedidas { get; set; }

        public DateTime lngPromInic { get; set; }

        public DateTime lngPromFinal { get; set; }

        public DateTime DataInicPreco { get; set; }

        DateTime DataFimPreco { get; set; }

        public string strFiltroWhere { get; set; }

        public string strFiltroOrderBy { get; set; }

        public string strFiltroArmazens { get; set; }

        public string strFiltroArtigos { get; set; }

        public string strFiltroPromocoes { get; set; }

        public string strTabDocCab { get; set; }

        public string strTabDocLin { get; set; }

        public string labelFileName { get; set; }

        private Etiquetas myLabels;
        public Etiquetas Labels { get { return myLabels; } }

        private Etiqueta myLabel;
        public Etiqueta Label { get { return myLabel; } }

        private Graphics myGrafic;
        public Graphics Grafic { get { return myGrafic; } }

        internal string[] ToArray()
        {
            try
            {
                myLabels = Eti.Aplicacao.Tabelas.get_Etiquetas(Convert.ToByte(bytLableType));
                myLabels.GetNew();

                myLabel = myLabels.FindEtiqueta(bytLableType, 0, labelFileName, ref myGrafic);

                string strErrorDescription = string.Empty;
                string strCamposSelect = myLabels.GetCamposSelectToQtd(docType, bytTpEmissaoPorDocOuLinha, copiesType, chkUsaQtdMedidas, measureType, blnFiltroComTpNivel, strTabDocLin);
                string strFiltroLeftJoin = myLabels.GetFiltroLeftJoin(strCamposSelect + " " + strFiltroWhere + " " + strFiltroOrderBy, strTabDocCab, strTabDocLin, ref strErrorDescription);

                if (!string.IsNullOrEmpty(strErrorDescription))
                {
                    throw new Exception(strErrorDescription);
                }

                string[] res = new string[]{
                                                Convert.ToInt32(bytLableType).ToString(),
                                                Convert.ToInt32(toPrint).ToString(),
                                                Convert.ToInt32(columnPosition).ToString(),
                                                Convert.ToInt32(linePosition).ToString(),
                                                Convert.ToInt32(Copies).ToString(),
                                                Convert.ToInt32(docType).ToString(),
                                                Convert.ToInt32(bytTpEmissaoPorDocOuLinha).ToString(),
                                                Convert.ToInt32(copiesType).ToString(),
                                                strCamposSelect,
                                                strFiltroLeftJoin,
                                                strFiltroWhere,
                                                strFiltroOrderBy,
                                                lngPromInic.Ticks.ToString(),
                                                lngPromFinal.Ticks.ToString(),
                                                "0",
                                                strFiltroArmazens,
                                                strFiltroArtigos,
                                                DataInicPreco.ToShortDateString(),
                                                DataFimPreco.ToShortDateString(),
                                                strFiltroPromocoes
                                            };

                return res;

            } catch (Exception ex) { throw new Exception("PrintLabelInput.ToArray", ex); }
        }
    }
}