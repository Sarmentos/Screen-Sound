using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

// Inicio do Screen Sound

string mensagemDeBoasVindas = "Boas vindas ao Screen Sound!";

Dictionary<string, List<int>> bandasRegistradas = new Dictionary<string, List<int>>()
{
    { "Linkin Park", new List<int> { 10, 8, 6 } },
    { "The Beatles", new List<int>() }
};

// Menu principal
void ExibirMenu()
{
    bool continuar = true;

    while (continuar)
    {
        Console.Clear();
        ExibirLogo();

        Console.WriteLine("\nDigite 1 para registrar banda");
        Console.WriteLine("Digite 2 para visualizar bandas");
        Console.WriteLine("Digite 3 para avaliar banda");
        Console.WriteLine("Digite 4 para exibir média da banda");
        Console.WriteLine("Digite 0 para sair");

        Console.Write("\nDigite sua opção: ");

        if (!int.TryParse(Console.ReadLine(), out int opcaoEscolhida))
        {
            Console.WriteLine("\nDigite uma opção válida!");
            Thread.Sleep(1500);
            continue;
        }

        switch (opcaoEscolhida)
        {
            case 1:
                RegistrarBanda();
                break;

            case 2:
                VisualizarBandas();
                break;

            case 3:
                AvaliarUmaBanda();
                break;

            case 4:
                ExibirMediaDaBanda();
                break;

            case 0:
                Console.WriteLine("\nTchau! Obrigado por utilizar o Screen Sound.");
                continuar = false;
                break;

            default:
                Console.WriteLine("\nOpção inválida!");
                Thread.Sleep(1500);
                break;
        }
    }
}

void ExibirLogo()
{
    Console.WriteLine(@"
░██████╗░█████╗░██████╗░███████╗███████╗███╗░░██╗  ░██████╗░█████╗░██╗░░░██╗███╗░░██╗██████╗░██╗
██╔════╝██╔══██╗██╔══██╗██╔════╝██╔════╝████╗░██║  ██╔════╝██╔══██╗██║░░░██║████╗░██║██╔══██╗██║
╚█████╗░██║░░╚═╝██████╔╝█████╗░░█████╗░░██╔██╗██║  ╚█████╗░██║░░██║██║░░░██║██╔██╗██║██║░░██║██║
░╚═══██╗██║░░██╗██╔══██╗██╔══╝░░██╔══╝░░██║╚████║  ░╚═══██╗██║░░██║██║░░░██║██║╚████║██║░░██║╚═╝
██████╔╝╚█████╔╝██║░░██║███████╗███████╗██║░╚███║  ██████╔╝╚█████╔╝╚██████╔╝██║░╚███║██████╔╝██╗
╚═════╝░░╚════╝░╚═╝░░╚═╝╚══════╝╚══════╝╚═╝░░╚══╝  ╚═════╝░░╚════╝░░╚═════╝░╚═╝░░╚══╝╚═════╝░╚═╝");

    Console.WriteLine(mensagemDeBoasVindas);
}

// Registrar banda
void RegistrarBanda()
{
    Console.Clear();

    ExibirTituloDaOpcao("Registrar Banda");

    Console.Write("Digite o nome da banda: ");

    string nomeDaBanda = Console.ReadLine()?.Trim() ?? "";

    if (string.IsNullOrWhiteSpace(nomeDaBanda))
    {
        Console.WriteLine("\nO nome da banda não pode ficar vazio.");
        AguardarUsuario();
        return;
    }

    if (bandasRegistradas.ContainsKey(nomeDaBanda))
    {
        Console.WriteLine("\nEssa banda já está cadastrada!");
        AguardarUsuario();
        return;
    }

    bandasRegistradas.Add(nomeDaBanda, new List<int>());

    Console.WriteLine($"\nBanda {nomeDaBanda} registrada com sucesso!");

    AguardarUsuario();
}

// Visualizar bandas
void VisualizarBandas()
{
    Console.Clear();

    ExibirTituloDaOpcao("Bandas Registradas");

    if (bandasRegistradas.Count == 0)
    {
        Console.WriteLine("Nenhuma banda cadastrada.");
    }
    else
    {
        foreach (var banda in bandasRegistradas)
        {
            Console.WriteLine($"\nBanda: {banda.Key}");
            Console.WriteLine($"Quantidade de avaliações: {banda.Value.Count}");

            if (banda.Value.Count > 0)
            {
                Console.WriteLine($"Notas: {string.Join(", ", banda.Value)}");
            }

            Console.WriteLine("--------------------------------");
        }
    }

    AguardarUsuario();
}

// Avaliar banda
void AvaliarUmaBanda()
{
    Console.Clear();

    ExibirTituloDaOpcao("Avaliar Banda");

    Console.Write("Digite o nome da banda que deseja avaliar: ");

    string nomeDaBanda = Console.ReadLine()?.Trim() ?? "";

    if (!bandasRegistradas.TryGetValue(nomeDaBanda, out List<int>? notas))
    {
        Console.WriteLine("\nBanda não encontrada!");
        AguardarUsuario();
        return;
    }

    Console.Write($"\nQual nota a banda {nomeDaBanda} merece? ");

    if (!int.TryParse(Console.ReadLine(), out int nota))
    {
        Console.WriteLine("\nDigite uma nota numérica!");
        AguardarUsuario();
        return;
    }

    if (nota < 0 || nota > 10)
    {
        Console.WriteLine("\nA nota deve estar entre 0 e 10!");
        AguardarUsuario();
        return;
    }

    notas.Add(nota);

    Console.WriteLine($"\nA nota {nota} foi registrada com sucesso para {nomeDaBanda}!");

    AguardarUsuario();
}

// Exibir média
void ExibirMediaDaBanda()
{
    Console.Clear();

    ExibirTituloDaOpcao("Média da Banda");

    Console.Write("Digite o nome da banda: ");

    string nomeDaBanda = Console.ReadLine()?.Trim() ?? "";

    if (!bandasRegistradas.TryGetValue(nomeDaBanda, out List<int>? notas))
    {
        Console.WriteLine("\nBanda não encontrada!");
        AguardarUsuario();
        return;
    }

    if (notas.Count == 0)
    {
        Console.WriteLine($"\nA banda {nomeDaBanda} ainda não possui avaliações.");
        AguardarUsuario();
        return;
    }

    double media = notas.Average();

    Console.WriteLine($"\nBanda: {nomeDaBanda}");
    Console.WriteLine($"Quantidade de avaliações: {notas.Count}");
    Console.WriteLine($"Média das avaliações: {media:F2}");

    AguardarUsuario();
}

// Título das opções
void ExibirTituloDaOpcao(string titulo)
{
    Console.WriteLine("****************************************");
    Console.WriteLine(titulo);
    Console.WriteLine("****************************************");
}

// Aguardar usuário
void AguardarUsuario()
{
    Thread.Sleep(2300);
}

ExibirMenu();
