using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtividadeFinal
{
    internal class Marca
    {
        public int CodMarca { get; set; }       //MARCA[0]
        public string NomeMarca { get; set; }   //MARCA[1]
        public Marca MakeMarca(int cod, string nome)
        {
            Marca marca = new Marca
            {
                CodMarca = cod,
                NomeMarca = nome
            };

            return marca;
        }

        public void ExtrairDadosArquivoMarcas(List<Marca> marca)
        {
            LerEInserirNaListaMarca(marca);
        }
        private void LerEInserirNaListaMarca(List<Marca> marca)
        {
            string arquivoMarcas = @"../../inputFiles/MARCAS.txt";
            string line;

            using (StreamReader reader = new StreamReader(arquivoMarcas))
            {
                line = reader.ReadLine();
                while ((line = reader.ReadLine()) != null)
                {
                    var partes = line.Split(';');
                    Int32.TryParse(partes[0], out int cod);
                    string nome = partes[1];

                    marca.Add(MakeMarca(cod, nome));
                }
                Console.WriteLine("Arquivo lido com sucesso!\n");
            }
        }
    }
}
