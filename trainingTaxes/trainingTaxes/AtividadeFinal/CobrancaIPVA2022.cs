using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtividadeFinal
{
    internal class CobrancaIPVA2022
    {
        public int IDVeiculo { get; set; }              //VEICULOS[0]                                                   FEITO
        public long IDContribuinte { get; set; }        //VEICULOS[1]                                                   FEITO
        public int AnoFabricacao { get; set; }          //VEICULOS[3]                                                   FEITO
        public int CodMarca { get; set; }               //VEICULOS[4]                                                   FEITO
        public int CodTipoCombustivel { get; set; }     //VEICULOS[5]                                                   FEITO
        public string NomeMarca { get; set; }           //MARCAS[1]                                                     FEITO
        public string TipoCombustivel { get; set; }     //TIPOS_COMBUSTIVEL[1]                                          FEITO
        public string NomeContribuinte { get; set; }    //CONTRIBUINTES[1]                                              FEITO
        public string NomeMunicipio { get; set; }       //CONTRIBUINTES[2]                                              FEITO
        public int QuantNotasFiscais { get; set; }      //Cont(NOTAS_FISCAIS) por ID Contribuinte                       FEITO
        public double ValorFipe { get; set; }           //TABELA_FIPE_2022[2] Baseado em COD_MARCA e ANO_FABRICACAO     FEITO
        public double ValorIPVABruto { get; set; }      //ValorFipe * 0.04                                              FEITO
        public double DescontoBomCidadao { get; set; }  //Baseado no Valor Bruto e QuantNotasFiscais                    FEITO
        public double ValorIPVAFinal { get; set; }      //ValorBruto - Desconto                                         FEITO

        public CobrancaIPVA2022 MakeCobranca(int idVeiculo, long idContribuinte, int ano, int codMarca, int codTipoCombustivel)
        {
            CobrancaIPVA2022 cobranca = new CobrancaIPVA2022
            {
                IDVeiculo = idVeiculo,
                IDContribuinte = idContribuinte,
                AnoFabricacao = ano,
                CodMarca = codMarca,
                CodTipoCombustivel = codTipoCombustivel,
                NomeMarca = "",
                TipoCombustivel = "",
                NomeContribuinte = "",
                NomeMunicipio = "",
                QuantNotasFiscais = 0,
                ValorFipe = 0,
                ValorIPVABruto = 0,
                DescontoBomCidadao = 0,
                ValorIPVAFinal = 0
            };

            return cobranca;
        }

        public void ExtrairDadosArquivoVeiculos(List<CobrancaIPVA2022> cobrancaIpva)
        {
            LerEInserirNaListaCobranca(cobrancaIpva);
        }
        private void LerEInserirNaListaCobranca(List<CobrancaIPVA2022> cobrancaIpva)
        {
            string arquivoVeiculos = @"../../inputFiles/VEICULOS.txt";
            string line;

            using (StreamReader reader = new StreamReader(arquivoVeiculos))
            {
                line = reader.ReadLine();
                while ((line = reader.ReadLine()) != null)
                {
                    //Le uma linha e extrai os dados necessários para variáveis
                    var partes = line.Split(';');
                    Int32.TryParse(partes[0], out int idVeiculo);
                    long.TryParse(partes[1], out long idContribuinte);
                    Int32.TryParse(partes[3], out int ano);
                    Int32.TryParse(partes[4], out int codMarca);
                    Int32.TryParse(partes[5], out int codCombustivel);

                    //Cria um item da classe CobrancaIPVA2022 e insere na lista de cobranças
                    cobrancaIpva.Add(MakeCobranca(idVeiculo, idContribuinte, ano, codMarca, codCombustivel));
                }
                Console.WriteLine("Arquivo lido com sucesso!\n");
            }
        }

        public void InserirDadosContribuinte(List<CobrancaIPVA2022> cobrancaIpva, List<Contribuinte> contribuinte)
        {
            foreach (var ipva in cobrancaIpva)
            {
                int i = Convert.ToInt32(ipva.IDContribuinte - ((ipva.IDContribuinte % 100000) * 100000 + 10000011100));

                ipva.QuantNotasFiscais = contribuinte[i].QuantNotasFiscais;
                ipva.NomeContribuinte = contribuinte[i].NomeContribuinte;
                ipva.NomeMunicipio = contribuinte[i].NomeMunicipio;
            }

            Console.WriteLine("Dados dos contribuintes inseridos na lista com sucesso!");
        }

        public void InserirDadosMarca(List<CobrancaIPVA2022> cobrancaIpva, List<Marca> marca)
        {
            foreach (var ipva in cobrancaIpva)
            {
                ipva.NomeMarca = marca[ipva.CodMarca - 1000].NomeMarca;
            }

            Console.WriteLine("Dados das marcas inseridos na lista com sucesso!");
        }

        public void InserirDadosCombustivel(List<CobrancaIPVA2022> cobrancaIpva, List<Combustivel> combustivel)
        {
            foreach (var ipva in cobrancaIpva)
            {
                ipva.TipoCombustivel = combustivel[ipva.CodTipoCombustivel - 1].TipoCombustivel;
            }

            Console.WriteLine("Dados dos combustíveis inseridos na lista com sucesso!");
        }

        public void InserirDadosTabelaFipe(List<CobrancaIPVA2022> cobrancaIPVA2022, List<TabelaFipe2022> tabelaFipe2022)
        {
            foreach (var ipva in cobrancaIPVA2022)
            {
                //(COD_MARCA - 1000) * 23 + ANO_FABR - 2000
                ipva.ValorFipe = tabelaFipe2022[(ipva.CodMarca - 1000) * 23 + (ipva.AnoFabricacao - 2000)].ValorFipe;
            }
            Console.WriteLine("Dados do valor FIPE retornados com sucesso!");
        }

        public void CalcularValorIPVA(List<CobrancaIPVA2022> cobrancaIpva)
        {
            foreach (var ipva in cobrancaIpva)
            {
                // ValorIPVABruto, DescontoBomCidadao, ValorIPVAFinal
                if (ipva.AnoFabricacao > 2010)
                {
                    ipva.ValorIPVABruto = ipva.ValorFipe * 0.04;

                    if (ipva.QuantNotasFiscais <= 5)
                    {
                        ipva.DescontoBomCidadao = 0;
                    }
                    else if (ipva.QuantNotasFiscais <= 10)
                    {
                        ipva.DescontoBomCidadao = ipva.ValorIPVABruto * 0.01;
                    }
                    else if (ipva.QuantNotasFiscais <= 15)
                    {
                        ipva.DescontoBomCidadao = ipva.ValorIPVABruto * 0.03;
                    }
                    else
                    {
                        ipva.DescontoBomCidadao = ipva.ValorIPVABruto * 0.05;
                    }

                    ipva.ValorIPVAFinal = ipva.ValorIPVABruto - ipva.DescontoBomCidadao;
                }
            }
        }

        public void GravarArquivo(List<CobrancaIPVA2022> cobrancaIpva)
        {
            try
            {
                string outputFilePath = @"../../outputFiles/COBRANCA_IPVA_2022.txt";

                using (StreamWriter writer = new StreamWriter(outputFilePath))
                {
                    writer.WriteLine("ID_VEICULO;ANO_FABR;NOME_MARCA;DESCR_COMBUST;NOME_CONTRIB;NOME_MUNICIPIO;QUANT_NF;VLR_FIPE;VLR_IPVA_BRUTO;DESC_BOM_CID;VLR_IPVA_LIQUIDO");

                    foreach (var ipva in cobrancaIpva)
                    {
                        writer.WriteLine(ipva.IDVeiculo + ";" + ipva.AnoFabricacao + ";" + ipva.NomeMarca + ";"
                            + ipva.TipoCombustivel + ";" + ipva.NomeContribuinte + ";" + ipva.NomeMunicipio
                            + ";" + ipva.QuantNotasFiscais + ";" + ipva.ValorFipe.ToString("00.00", new CultureInfo("Pt-BR")) + ";" + ipva.ValorIPVABruto.ToString("00.00", new CultureInfo("Pt-BR"))
                            + ";" + ipva.DescontoBomCidadao.ToString("00.00", new CultureInfo("Pt-BR")) + ";" + ipva.ValorIPVAFinal.ToString("00.00", new CultureInfo("Pt-BR")));
                    }
                    Console.WriteLine("Arquivo escrito com sucesso!!!!!!!!!!!!!");
                    Console.WriteLine("WEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE!!!!!!!!!!!!!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }


    }
}
