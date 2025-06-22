using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jogo_do_Acertou_perdeu_2versao
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();

            Console.WriteLine("Bem-vindo ao Jogo de Adivinhação!");

            int numJogadores;
            while (true)
            {
                Console.Write("Quantos jogadores irão jogar? ");
                string inputJogadores = Console.ReadLine();
                if (int.TryParse(inputJogadores, out numJogadores) && numJogadores > 0)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Por favor, digite um número válido de jogadores (maior que zero).");
                }
            }

            int tentativas = 5;
            // Este array agora registra quem 'perdeu' ao adivinhar o número
            bool[] jogadorAdivinhouEPertence = new bool[numJogadores];
            int jogadorQueAdivinhouIndex = -1; // Armazena o índice do jogador que adivinhou e perdeu

            int limiteInferior = 1;
            int limiteSuperior = 100;

            int numeroOculto = random.Next(limiteInferior, limiteSuperior + 1);

            Console.WriteLine($"\nO jogo vai começar! O número oculto está entre {limiteInferior} e {limiteSuperior}.");

            bool jogoTerminou = false; // Flag para controlar o término completo do jogo

            for (int tentativaAtual = 0; tentativaAtual < tentativas; tentativaAtual++)
            {
                Console.WriteLine($"\n--- Rodada {tentativaAtual + 1} de {tentativas} ---");

                for (int i = 0; i < numJogadores; i++)
                {
                    // Se o jogo já terminou (alguém adivinhou), não permite mais palpites
                    if (jogoTerminou)
                    {
                        break;
                    }

                    // Se o jogador já perdeu (adivinhou), ele não participa mais
                    if (jogadorAdivinhouEPertence[i])
                    {
                        continue;
                    }

                    Console.WriteLine($"\n--- Jogador {i + 1} ---");
                    Console.WriteLine($"Limite Inferior do Jogo: {limiteInferior}");
                    Console.WriteLine($"Limite Superior do Jogo: {limiteSuperior}");

                    int palpite;
                    while (true)
                    {
                        Console.Write($"Jogador {i + 1}, faça seu palpite (tentativa {tentativaAtual + 1} de {tentativas}): ");
                        string inputPalpite = Console.ReadLine();
                        if (int.TryParse(inputPalpite, out palpite) && palpite >= limiteInferior && palpite <= limiteSuperior)
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine($"Palpite inválido! Digite um número entre {limiteInferior} e {limiteSuperior}.");
                        }
                    }

                    if (palpite == numeroOculto)
                    {
                        Console.WriteLine($"Oh não, Jogador {i + 1}! Você adivinhou o número {numeroOculto} e é o perdedor!");
                        jogadorAdivinhouEPertence[i] = true; // Marca este jogador como o perdedor
                        jogadorQueAdivinhouIndex = i; // Armazena quem adivinhou
                        jogoTerminou = true; // Seta a flag para terminar o jogo
                        break; // Sai do loop interno (jogadores na rodada)
                    }
                    else
                    {
                        Console.WriteLine("Errou!");
                        if (palpite < numeroOculto)
                        {
                            Console.WriteLine("O número oculto é MAIOR.");
                            limiteInferior = palpite;
                        }
                        else
                        {
                            Console.WriteLine("O número oculto é MENOR.");
                            limiteSuperior = palpite;
                        }
                    }
                }

                if (jogoTerminou) // Verifica a flag para sair do loop externo (tentativas)
                {
                    break;
                }
            }

            Console.WriteLine("\n--- Fim do Jogo ---");

            if (jogadorQueAdivinhouIndex != -1) // Se alguém adivinhou o número e, portanto, perdeu
            {
                Console.WriteLine($"O jogador {jogadorQueAdivinhouIndex + 1} é o perdedor, pois adivinhou o número {numeroOculto}!");
                Console.WriteLine("Os outros jogadores são os vencedores por não terem adivinhado:");
                for (int i = 0; i < numJogadores; i++)
                {
                    if (i != jogadorQueAdivinhouIndex)
                    {
                        Console.WriteLine($"- Jogador {i + 1}");
                    }
                }
            }
            else // Ninguém adivinhou o número dentro do limite de tentativas
            {
                Console.WriteLine($"Nenhum jogador adivinhou o número oculto ({numeroOculto}).");
                Console.WriteLine("Como ninguém adivinhou, não há perdedores diretos neste jogo. Todos ganham por não terem adivinhado o número!");
            }

            Console.WriteLine("\nPressione qualquer tecla para sair.");
            Console.ReadKey();
        }
    }
}
