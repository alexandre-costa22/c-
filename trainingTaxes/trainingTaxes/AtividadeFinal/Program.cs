using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AtividadeFinal;

namespace AtividadeFinal
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string enter;
            
            //Objetos do tipo de cada classe para usar as funções mesmo tendo as listas vazias
            CobrancaIPVA2022 cobr = new CobrancaIPVA2022();
            Contribuinte cont = new Contribuinte();
            Combustivel comb = new Combustivel();
            Marca marc = new Marca();
            TabelaFipe2022 tabela = new TabelaFipe2022();

            //Listas de objetos de cada classe para armazenar os dados básicos de cada arquivo
            List<CobrancaIPVA2022> cobrancaIpva = new List<CobrancaIPVA2022>();
            List<Contribuinte> contribuinte = new List<Contribuinte>();
            List<Combustivel> combustivel = new List<Combustivel>();
            List<Marca> marca = new List<Marca>();
            List<TabelaFipe2022> tabelaFipe2022 = new List<TabelaFipe2022>();

            //Extrai os dados básicos dos arquivos e insere em suas respectivas listas por classe
            cobr.ExtrairDadosArquivoVeiculos(cobrancaIpva);
            cont.ExtrairDadosArquivoContribuintes(contribuinte);
            comb.ExtrairDadosArquivoCombustiveis(combustivel);
            marc.ExtrairDadosArquivoMarcas(marca);
            tabela.ExtrairDadosArquivoTabelaFipe(tabelaFipe2022);

            //Extrai os dados das Notas fiscais, inserindo a quantidade por contribuinte na tabela contribuinte
            cont.ExtrairNotasFiscaisPorContribuinte(contribuinte);

            //Insere os dados da lista/classe contribuinte na lista das cobranças
            cobr.InserirDadosContribuinte(cobrancaIpva, contribuinte);
            //Insere os dados da lista/classe marca na lista das cobranças
            cobr.InserirDadosMarca(cobrancaIpva, marca);
            //Insere os dados da lista/classe combustivel na lista das cobranças
            cobr.InserirDadosCombustivel(cobrancaIpva, combustivel);
            //Insere os dados da lista/classe tabela FIPE na lista das cobranças
            cobr.InserirDadosTabelaFipe(cobrancaIpva, tabelaFipe2022);

            //Calcula valor IPVA e desconto e insere na lista/classe das cobranças
            cobr.CalcularValorIPVA(cobrancaIpva);
            //Cria arquivo 
            cobr.GravarArquivo(cobrancaIpva);

            /*
            foreach (var ipva in cobrancaIpva)
            {
                Console.WriteLine($"ID:   {ipva.IDContribuinte}\n" +
                                  $"Nome: {ipva.NomeContribuinte}\n" +
                                  $"Muni: {ipva.NomeMunicipio}\n" +
                                  $"Nota: {ipva.QuantNotasFiscais}\n" +
                                  $"Marc: {ipva.NomeMarca}\n\n");
                
            }
            */

            Console.WriteLine("\n\n\n\nTecle ENTER para encerrar...");
            enter = Console.ReadLine();
        }
    }
}
