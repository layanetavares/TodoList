
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Todolist
{
    public class Tarefa
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public bool Concluida { get; set; }

        public Tarefa() { }

        public Tarefa(int id, string descricao)
        {
            Id = id;
            Descricao = descricao;
            Concluida = false;
        }
    }

    class Program
    {
        private static List<Tarefa> listaDeTarefas = new List<Tarefa>();
        private static int proximoId = 1;
        private static readonly string caminhoArquivo = "tarefas.json";

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
                    case "1":
                        AdicionarTarefa();
                        break;
                    case "2":
                        ListarTarefas();
                        PausarTela();
                        break;
                    case "3":
                        ConcluirTarefa();
                        break;
                    case "4":
                        ExcluirTarefa();
                        break;
                    case "5":
                        executando = false;
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("\n  Saindo do sistema... Até logo! 👋\n");
                        Console.ResetColor();
                        break;
                    default:
                        ExibirMensagemErro("\n  Opção inválida! Tente novamente.");
                        break;
                }
            }
        }

        static void ExibirMenuPrincipal()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Cyan;
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

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("──────────────────────────────────────────");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("  Escolha uma opção: ");
        }

        static void ListarTarefas()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("┌────────────────────────────────────────────────────────┐");
            Console.WriteLine("│                  SUA LISTA DE TAREFAS                  │");
            Console.WriteLine("├──────┬───────────┬─────────────────────────────────────┤");
            Console.WriteLine("│  ID  │  STATUS   │ DESCRIÇÃO                           │");
            Console.WriteLine("├──────┼───────────┼─────────────────────────────────────┤");
            Console.ResetColor();

            if (listaDeTarefas.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("│  --  │  VAZIO    │ Nenhuma tarefa cadastrada ainda.    │");
                Console.ResetColor();
            }
            else
            {
                foreach (var tarefa in listaDeTarefas)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("│ ");
                    Console.ResetColor();

                    Console.Write($"{tarefa.Id,-4}");

                    Console.ForegroundColor = ConsoleColor.Cyan;
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

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(" │ ");
                    Console.ResetColor();

                    string desc = tarefa.Descricao.Length > 33
                        ? tarefa.Descricao.Substring(0, 30) + "..."
                        : tarefa.Descricao;

                    Console.Write($"{desc,-35}");

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(" │");
                    Console.ResetColor();
                }
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("└──────┴───────────┴─────────────────────────────────────┘");
            Console.ResetColor();
        }

        static void AdicionarTarefa()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(" Novo Item:");
            Console.ResetColor();

            Console.Write("  » Digite a descrição da tarefa: ");
            string descricao = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(descricao))
            {
                Tarefa novaTarefa = new Tarefa(proximoId++, descricao.Trim());
                listaDeTarefas.Add(novaTarefa);
                SalvarTarefasNoArquivo();

                ExibirMensagemSucesso("\n  ✔ Tarefa adicionada com sucesso!");
            }
            else
            {
                ExibirMensagemErro("\n  ⚠ A descrição não pode ser vazia.");
            }
        }

        static void ConcluirTarefa()
        {
            ListarTarefas();
            if (listaDeTarefas.Count == 0)
            {
                PausarTela();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("\n  » Digite o ID da tarefa para concluir: ");
            Console.ResetColor();

            if (int.TryParse(Console.ReadLine(), out int id))
            {
                Tarefa tarefa = listaDeTarefas.FirstOrDefault(t => t.Id == id);
                if (tarefa != null)
                {
                    tarefa.Concluida = true;
                    SalvarTarefasNoArquivo();
                    ExibirMensagemSucesso("\n  Excelente! Tarefa atualizada.");
                }
                else
                {
                    ExibirMensagemErro("\n  ID não encontrado.");
                }
            }
            else
            {
                ExibirMensagemErro("\n  Por favor, digite um número de ID válido.");
            }
        }

        static void ExcluirTarefa()
        {
            ListarTarefas();
            if (listaDeTarefas.Count == 0)
            {
                PausarTela();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("\n  » Digite o ID da tarefa que deseja apagar: ");
            Console.ResetColor();

            if (int.TryParse(Console.ReadLine(), out int id))
            {
                Tarefa tarefa = listaDeTarefas.FirstOrDefault(t => t.Id == id);
                if (tarefa != null)
                {
                    listaDeTarefas.Remove(tarefa);
                    SalvarTarefasNoArquivo();
                    ExibirMensagemSucesso("\n  Tarefa removida do sistema.");
                }
                else
                {
                    ExibirMensagemErro("\n  ID não encontrado.");
                }
            }
            else
            {
                ExibirMensagemErro("\n  Por favor, digite um número de ID válido.");
            }
        }

        #region Métodos Utilitários

        static void SalvarTarefasNoArquivo()
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(listaDeTarefas, options);
                File.WriteAllText(caminhoArquivo, json);
            }
            catch (Exception ex)
            {
                ExibirMensagemErro($"\n  Erro ao salvar dados: {ex.Message}");
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

                    if (listaDeTarefas.Count > 0)
                    {
                        proximoId = listaDeTarefas.Max(t => t.Id) + 1;
                    }
                }
            }
            catch
            {
                listaDeTarefas = new List<Tarefa>();
                proximoId = 1;
            }
        }

        static void ExibirMensagemSucesso(string mensagem)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(mensagem);
            Console.ResetColor();
            PausarTela();
        }

        static void ExibirMensagemErro(string mensagem)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(mensagem);
            Console.ResetColor();
            PausarTela();
        }

        static void PausarTela(string mensagem = "\n  Pressione qualquer tecla para continuar...")
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(mensagem);
            Console.ResetColor();
            Console.ReadKey(true);
        }

        #endregion
    }
}