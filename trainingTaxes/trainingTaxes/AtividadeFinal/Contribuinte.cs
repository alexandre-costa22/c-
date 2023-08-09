using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtividadeFinal
{
    internal class Contribuinte
    {
        public long IDContribuinte { get; set; }        //CONTRIBUINTE[0]
        public string NomeContribuinte { get; set; }    //CONTRIBUINTE[1]
        public string NomeMunicipio { get; set; }       //CONTRIBUINTE[2]
        public int QuantNotasFiscais { get; set; }      //Cont(NOTAS_FISCAIS) por ID Contribuinte
        public Contribuinte MakeContribuinte(long id, string nomeContribuinte, string nomeMunicipio)
        {
            Contribuinte contribuinte = new Contribuinte
            {
                IDContribuinte = id,
                NomeContribuinte = nomeContribuinte,
                NomeMunicipio = nomeMunicipio,
                QuantNotasFiscais = 0
            };

            return contribuinte;
        }

        public void ExtrairDadosArquivoContribuintes(List<Contribuinte> contribuinte)
        {
            LerEInserirNaListaContribuinte(contribuinte);
        }
        private void LerEInserirNaListaContribuinte(List<Contribuinte> contribuinte)
        {
            string arquivoContribuintes = @"../../inputFiles/CONTRIBUINTES.txt";
            string line;

            using (StreamReader reader = new StreamReader(arquivoContribuintes))
            {
                line = reader.ReadLine();
                while ((line = reader.ReadLine()) != null)
                {
                    var partes = line.Split(';');
                    long.TryParse(partes[0], out long id);
                    string nome = partes[1];
                    string municipio = partes[2];

                    contribuinte.Add(MakeContribuinte(id, nome, municipio));
                }
                Console.WriteLine("Arquivo lido com sucesso!\n");
            }
        }

        public void ExtrairNotasFiscaisPorContribuinte(List<Contribuinte> contribuinte)
        {
            string arquivoNotasFiscais = @"../../inputFiles/NOTAS_FISCAIS.txt";

            using (StreamReader reader = new StreamReader(arquivoNotasFiscais))
            {
                string line = reader.ReadLine();
                while ((line = reader.ReadLine()) != null)
                {
                    var partes = line.Split(';');
                    int.TryParse(partes[1].Substring(partes[1].Length - 5), out int idContribuinte);

                    contribuinte[idContribuinte - 11100].QuantNotasFiscais++;
                }
                Console.WriteLine("Notas Fiscais adicionadas com sucesso!\n");
            }
        }
    }
}
