using System;
using System.Collections.Generic;
using System.Threading;

class Programa
{
    static void Main()
    {
        string selecao;
        int dificuldade = 0;
        bool continuarJogando = true;

        // Menu principal
        bool continuarNoMenu = true;

        while (continuarNoMenu)
        {
            Console.Clear();
            Console.WriteLine("===== MENU PRINCIPAL =====");
            Console.WriteLine("1 - Jogar");
            Console.WriteLine("2 - Instruções");
            Console.WriteLine("3 - Créditos");
            Console.WriteLine("4 - Sair");
            Console.Write("Escolha uma opção: ");

            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    continuarNoMenu = false;
                    break;

                case "2":
                    Console.Clear();
                    Console.WriteLine("===== INSTRUÇÕES =====");
                    Console.WriteLine("Este é um jogo de memória com botões coloridos.");
                    Console.WriteLine("Você verá uma sequência de botões (números de 1 a 4) com cores e sons.");
                    Console.WriteLine("Repita a sequência corretamente para avançar!");
                    Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
                    Console.ReadKey();
                    break;

                case "3":
                    Console.Clear();
                    Console.WriteLine("===== CRÉDITOS =====");
                    Console.WriteLine("Desenvolvido por Gustavo Gonzalez e Gabriel Alves");
                    Console.WriteLine("Sistemas para Internet - 2025");
                    Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
                    Console.ReadKey();
                    break;

                case "4":
                    Console.WriteLine("Saindo do jogo... Até logo!");
                    return;

                default:
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine("Opção inválida! Tente novamente.");
                    Console.ResetColor();
                    Thread.Sleep(1000);
                    break;
            }
        }

        // Seletor de dificuldade
        do
        {
            Console.Clear();
            Console.WriteLine("Escolha a dificuldade inicial:");
            Console.WriteLine("1 - Fácil (8 botões)");
            Console.WriteLine("2 - Médio (14 botões)");
            Console.WriteLine("3 - Difícil (20 botões)");
            Console.WriteLine("4 - Muito Difícil (31 botões)");

            selecao = Console.ReadLine();
            if (selecao == "1" || selecao == "2" || selecao == "3" || selecao == "4")
            {
                switch (selecao)
                {
                    case "1":
                        dificuldade = 8;
                        selecao = "valido";
                        break;
                    case "2":
                        dificuldade = 14;
                        selecao = "valido";
                        break;
                    case "3":
                        dificuldade = 20;
                        selecao = "valido";
                        break;
                    case "4":
                        dificuldade = 31;
                        selecao = "valido";
                        break;
                }
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("Selecione uma dificuldade válida");
                Console.ResetColor();
                Thread.Sleep(550);
            }
        } while (selecao != "valido");

        // Início do jogo
        while (continuarJogando)
        {
            continuarJogando = JogoNaDificuldade(dificuldade);

            if (continuarJogando && dificuldade < 31)
            {
                dificuldade = GetProximaDificuldade(dificuldade);
            }
        }

        Console.WriteLine("Obrigado por jogar! Até a próxima!");
    }

    static bool JogoNaDificuldade(int dificuldade)
    {
        List<int> sequencia = new List<int>();
        int tamanhoSequencia = dificuldade;
        int tentativa = 0;

        while (tentativa < tamanhoSequencia)
        {
            Random rnd = new Random();
            int botao = rnd.Next(1, 5); // 1 - Vermelho, 2 - Verde, 3 - Azul, 4 - Amarelo
            sequencia.Add(botao);

            MostrarSequencia(sequencia);
            Console.Clear();

            if (!VerificarSequencia(sequencia))
            {
                Console.WriteLine("Você errou! O jogo acabou.");
                Console.Beep(352, 500);
                Thread.Sleep(125);
                Console.Beep(396, 500);
                Thread.Sleep(125);
                Console.Beep(352, 1000); // Som de derrota
                return false;
            }

            tentativa++;
        }

        if (dificuldade == 31)
        {
            Console.WriteLine("Parabéns! Você completou a dificuldade máxima!");
            Console.Beep(1000, 1000); // Som especial
        }
        else
        {
            Console.Beep(800, 500); // Som de vitória
        }

        return PerguntarAumentarDificuldade();
    }

    static void MostrarSequencia(List<int> sequencia)
    {
        foreach (int botao in sequencia)
        {
            AcenderBotao(botao);
            TocarSom(botao);
            Thread.Sleep(1000);
            Console.Clear();
        }
    }

    static void AcenderBotao(int botao)
    {
        switch (botao)
        {
            case 1:
                Console.ForegroundColor = ConsoleColor.Red;
                break;
            case 2:
                Console.ForegroundColor = ConsoleColor.Green;
                break;
            case 3:
                Console.ForegroundColor = ConsoleColor.Blue;
                break;
            case 4:
                Console.ForegroundColor = ConsoleColor.Yellow;
                break;
        }

        Console.WriteLine($"Botão {botao} aceso!");
        Console.ResetColor();
    }

    static void TocarSom(int botao)
    {
        switch (botao)
        {
            case 1:
                Console.Beep(400, 500);
                break;
            case 2:
                Console.Beep(500, 500);
                break;
            case 3:
                Console.Beep(600, 500);
                break;
            case 4:
                Console.Beep(700, 500);
                break;
        }
    }

    static bool VerificarSequencia(List<int> sequencia)
    {
        for (int i = 0; i < sequencia.Count; i++)
        {
            Console.WriteLine($"Digite o número do botão correspondente à sequência ({i + 1}/{sequencia.Count}):");
            string input = Console.ReadLine();

            if (!int.TryParse(input, out int tentativa) || tentativa != sequencia[i])
            {
                return false;
            }
        }

        return true;
    }

    static bool PerguntarAumentarDificuldade()
    {
        Console.WriteLine("\nVocê completou essa dificuldade!");
        Console.WriteLine("Quer aumentar a dificuldade? (s/n)");

        string resposta = Console.ReadLine().ToLower();
        return resposta == "s";
    }

    static int GetProximaDificuldade(int dificuldadeAtual)
    {
        switch (dificuldadeAtual)
        {
            case 8: return 14;
            case 14: return 20;
            case 20: return 31;
            default: return 31;
        }
    }
}