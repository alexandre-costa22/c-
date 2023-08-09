using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtividadeFinal
{
    internal class Combustivel
    {
        public int CodTipoCombustivel { get; set; }     //TIPOS_COMBUSTIVEL[0]
        public string TipoCombustivel { get; set; }     //TIPOS_COMBUSTIVEL[1]
        public Combustivel MakeCombustivel(int cod, string tipo)
        {
            Combustivel combustivel = new Combustivel
            {
                CodTipoCombustivel = cod,
                TipoCombustivel = tipo
            };

            return combustivel;
        }

        public void ExtrairDadosArquivoCombustiveis(List<Combustivel> combustivel)
        {
            LerEInserirNaListaCombustivel(combustivel);
        }
        private void LerEInserirNaListaCombustivel(List<Combustivel> combustivel)
        {
            string arquivoCombustiveis = @"../../inputFiles/TIPOS_COMBUSTIVEL.txt";
            string line;

            using (StreamReader reader = new StreamReader(arquivoCombustiveis))
            {
                line = reader.ReadLine();
                while ((line = reader.ReadLine()) != null)
                {
                    var partes = line.Split(';');
                    Int32.TryParse(partes[0], out int cod);
                    string nome = partes[1];

                    combustivel.Add(MakeCombustivel(cod, nome));
                }
                Console.WriteLine("Arquivo lido com sucesso!\n");
            }
        }
    }
}
