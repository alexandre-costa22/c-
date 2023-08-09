using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtividadeFinal
{
    internal class TabelaFipe2022
    {
        public int CodMarca { get; set; }       //TABELA_fIPE_2022[0]
        public int AnoFabr { get; set; }        //TABELA_fIPE_2022[1]
        public double ValorFipe { get; set; }    //TABELA_fIPE_2022[2]

        public TabelaFipe2022 MakeTabelaFipe(int codMarca, int ano, double valorFipe)
        {
            TabelaFipe2022 tabela = new TabelaFipe2022
            {
                CodMarca = codMarca,
                AnoFabr = ano,
                ValorFipe = valorFipe
            };

            return tabela;
        }

        public void ExtrairDadosArquivoTabelaFipe(List<TabelaFipe2022> tabelaFipe)
        {
            LerEInserirNaListaTabelaFipe(tabelaFipe);
        }
        private void LerEInserirNaListaTabelaFipe(List<TabelaFipe2022> tabelaFipe)
        {
            string arquivoTabelaFipe = @"../../inputFiles/TABELA_FIPE_2022.txt";
            string line;

            using (StreamReader reader = new StreamReader(arquivoTabelaFipe))
            {
                line = reader.ReadLine();
                while ((line = reader.ReadLine()) != null)
                {
                    var partes = line.Split(';');
                    Int32.TryParse(partes[0], out int cod);
                    Int32.TryParse(partes[1], out int ano);

                    var partesAux = partes[2].Split('.');

                    double.TryParse(partesAux[0], out double valorFipeInt);
                    double.TryParse(partesAux[1], out double valorFipeFlut);

                    double valorFipe = valorFipeInt + valorFipeFlut / 100;

                    tabelaFipe.Add(MakeTabelaFipe(cod, ano, valorFipe));
                }
                Console.WriteLine("Arquivo lido com sucesso!\n");
            }
        }
    }
}
