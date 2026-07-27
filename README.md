# 📝 Gerenciador de Tarefas (To-Do List) em C#

Este é um projeto de um Gerenciador de Tarefas desenvolvido em console (CLI) utilizando a linguagem **C#** e a plataforma **.NET**. O objetivo deste projeto foi consolidar os fundamentos de Programação Orientada a Objetos (POO) e manipulação de arquivos locais.

## 🚀 Funcionalidades
Este é um projeto de um Gerenciador de Tarefas desenvolvido em console (CLI) utilizando a linguagem **C#** e a plataforma **.NET**. O objetivo deste projeto foi consolidar os fundamentos de Programação Orientada a Objetos (POO), manipulação de arquivos locais e construção de interfaces de terminal estilizadas.

## 🚀 Funcionalidades

* **Adicionar Tarefas:** Permite criar novas tarefas com ID gerado automaticamente.
* **Listar Tarefas:** Exibe as tarefas em formato de tabela estilizada e alinhada no console.
* **Concluir Tarefas:** Altera o status da tarefa para concluído com destaque visual em verde.
* **Excluir Tarefas:** Remove tarefas da lista pelo ID de forma segura.
* **Persistência de Dados (JSON):** Todas as tarefas são guardadas automaticamente num arquivo local chamado `tarefas.json`. Ao fechar e abrir o programa, os dados são recuperados sem perda de informações.

## 🛠️ Tecnologias Utilizadas

* **Linguagem:** C#
* **Framework:** .NET
* **Biblioteca de Serialização:** `System.Text.Json`
* **I/O de Arquivos e Consultas:** `System.IO` e `System.Linq`

## 🎨 Diferenciais e Melhorias do Projeto

* **Interface Estilizada em Ciano:** Identidade visual moderna com tabela e bordas em tom Ciano (`ConsoleColor.Cyan`), combinando separadores gráficos Unicode (`┌ ─ ┐ │ └ ┘`) para uma experiência de *Dashboard*.
* **Melhoria de UX (Sem Travamentos):** Substituição de rotinas de tempo fixo (`Thread.Sleep`) por navegação fluida baseada na interação do usuário (`Console.ReadKey()`), permitindo ler avisos e mensagens no seu próprio ritmo.
* **Geração Inteligente de IDs:** Cálculo dinâmico de novos identificadores baseado no maior ID existente (`Max()`), evitando duplicação de códigos mesmo após exclusões de tarefas.
* **Código Limpo e Estruturado:** Separação de responsabilidades com modelo de dados dedicado (`Tarefa`), tratamento preventivo de exceções e manipulação consistente de cores do terminal.
