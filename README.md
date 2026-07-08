# 📝 Gerenciador de Tarefas (To-Do List) em C#

Este é um projeto de um Gerenciador de Tarefas desenvolvido em console (CLI) utilizando a linguagem **C#** e a plataforma **.NET**. O objetivo deste projeto foi consolidar os fundamentos de Programação Orientada a Objetos (POO) e manipulação de arquivos locais.

## 🚀 Funcionalidades
- **Adicionar Tarefas:** Permite criar novas tarefas com ID gerado automaticamente.
- **Listar Tarefas:** Exibe as tarefas em formato de tabela estilizada no console.
- **Concluir Tarefas:** Altera o status da tarefa para concluído com destaque visual em verde.
- **Excluir Tarefas:** Remove tarefas da lista pelo ID.
- **Persistência de Dados (JSON):** Todas as tarefas são guardadas automaticamente num arquivo local chamado `tarefas.json`. Ao fechar e abrir o programa, os dados são recuperados.

## 🛠️ Tecnologias Utilizadas
- **Linguagem:** C#
- **Framework:** .NET (versão 10.0)
- **Biblioteca de Serialização:** `System.Text.Json`
- **I/O de Arquivos:** `System.IO`

## 🎨 Diferenciais do Projeto
- **Interface Estilizada:** Uso de cores no console (`Console.ForegroundColor` e `Console.BackgroundColor`) para melhorar a experiência do utilizador.
- **Estruturação em Tabelas:** Layout desenhado com caracteres ASCII para manter os dados alinhados.
- **Código Limpo:** Divisão de responsabilidades usando classes separadas para o modelo de dados (`Tarefa.cs`) e para a lógica do sistema (`Program.cs`).

---
