using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace TodoListConsole
{
    class Program
    {
        static List<Tarefa> listaDeTarefas = new List<Tarefa>();
        static int proximoId = 1;
        static string caminhoArquivo = "tarefas.json";

        static void Main(string[] args)
        {
            Console.Clear();

            CarregarTarefasDoArquivo();
            bool executando = true;

            while (executando)
            {
                ExibirMenuPrincipal();
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1": AdicionarTarefa(); break;
                    case "2":
                        ListarTarefas();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("\n  Pressione qualquer tecla para voltar...");
                        Console.ResetColor();
                        Console.ReadKey();
                        break;
                    case "3": ConcluirTarefa(); break;
                    case "4": ExcluirTarefa(); break;
                    case "5":
                        executando = false;
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\n  Saindo do sistema... Até logo! 👋\n");
                        Console.ResetColor();
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n  Opção inválida! Tente novamente.");
                        Console.ResetColor();
                        System.Threading.Thread.Sleep(1000);
                        break;
                }
            }
        }

        static void ExibirMenuPrincipal()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("┌────────────────────────────────────────┐");
            Console.WriteLine("│        GERENCIADOR DE TAREFAS          │");
            Console.WriteLine("└────────────────────────────────────────┘");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("  [1]  Adicionar Nova Tarefa");
            Console.WriteLine("  [2]  Listar Todas as Tarefas");
            Console.WriteLine("  [3]  Marcar como Concluída");
            Console.WriteLine("  [4]  Excluir uma Tarefa");
            Console.WriteLine("  [5]  Sair do Programa");

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("──────────────────────────────────────────");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("  Escolha uma opção: ");
        }

        static void ListarTarefas()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("┌────────────────────────────────────────────────────────┐");
            Console.WriteLine("│                  SUA LISTA DE TAREFAS                  │");
            Console.WriteLine("├──────┬───────────┬─────────────────────────────────────┤");
            Console.WriteLine("│  ID  │  STATUS   │ DESCRIÇÃO                           │");
            Console.WriteLine("├──────┼───────────┼─────────────────────────────────────┤");
            Console.ResetColor();

            if (listaDeTarefas.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("│  --  │  VAZIO    │ Nenhuma tarefa cadastrada ainda.    │");
                Console.ResetColor();
            }
            else
            {
                foreach (var tarefa in listaDeTarefas)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("│ ");
                    Console.ResetColor();

                    Console.Write($"{tarefa.Id,-4}");

                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write(" │ ");
                    Console.ResetColor();

                    if (tarefa.Concluida)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("[FEITO]  ");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("[PEND]   ");
                    }
                    Console.ResetColor();

                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write(" │ ");
                    Console.ResetColor();

                    string desc = tarefa.Descricao.Length > 33 ? tarefa.Descricao.Substring(0, 30) + "..." : tarefa.Descricao;
                    Console.Write($"{desc,-35}");

                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine(" │");
                    Console.ResetColor();
                }
            }

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("└──────┴───────────┴─────────────────────────────────────┘");
            Console.ResetColor();
        }

        static void AdicionarTarefa()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(" Novo Item:");
            Console.ResetColor();

            Console.Write("  » Digite a descrição da tarefa: ");
            string descricao = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(descricao))
            {
                Tarefa novaTarefa = new Tarefa(proximoId, descricao);
                listaDeTarefas.Add(novaTarefa);
                proximoId++;
                SalvarTarefasNoArquivo();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n  ✔ Tarefa adicionada com sucesso!");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n  ⚠ A descrição não pode ser vazia.");
            }
            Console.ResetColor();
            System.Threading.Thread.Sleep(1200);
        }

        static void ConcluirTarefa()
        {
            ListarTarefas();
            if (listaDeTarefas.Count == 0) { System.Threading.Thread.Sleep(1500); return; }

            Console.Write("\n  » Digite o ID da tarefa para concluir: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                Tarefa tarefa = listaDeTarefas.Find(t => t.Id == id);
                if (tarefa != null)
                {
                    tarefa.Concluida = true;
                    SalvarTarefasNoArquivo();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n  Excelente! Tarefa atualizada.");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\n  ID não encontrado.");
                }
            }
            Console.ResetColor();
            System.Threading.Thread.Sleep(1200);
        }

        static void ExcluirTarefa()
        {
            ListarTarefas();
            if (listaDeTarefas.Count == 0) { System.Threading.Thread.Sleep(1500); return; }

            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Write("\n  » Digite o ID da tarefa que deseja apagar: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                Tarefa tarefa = listaDeTarefas.Find(t => t.Id == id);
                if (tarefa != null)
                {
                    listaDeTarefas.Remove(tarefa);
                    SalvarTarefasNoArquivo();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("\n  Tarefa removida do sistema.");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\n  ID não encontrado.");
                }
            }
            Console.ResetColor();
            System.Threading.Thread.Sleep(1200);
        }

        static void SalvarTarefasNoArquivo()
        {
            try
            {
                string json = JsonSerializer.Serialize(listaDeTarefas, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(caminhoArquivo, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao salvar: {ex.Message}");
            }
        }

        static void CarregarTarefasDoArquivo()
        {
            try
            {
                if (File.Exists(caminhoArquivo))
                {
                    string json = File.ReadAllText(caminhoArquivo);
                    listaDeTarefas = JsonSerializer.Deserialize<List<Tarefa>>(json) ?? new List<Tarefa>();
                    foreach (var t in listaDeTarefas)
                    {
                        if (t.Id >= proximoId) proximoId = t.Id + 1;
                    }
                }
            }
            catch { listaDeTarefas = new List<Tarefa>(); }
        }
    }
}