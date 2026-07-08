using System;

namespace TodoListConsole
{
    public class Tarefa
    {
     
        public int Id { get; set; }
        public string Descricao { get; set; }
        public bool Concluida { get; set; }
      
        public Tarefa(int id, string descricao)
        {
            Id = id;
            Descricao = descricao;
            Concluida = false; 
        }
        public override string ToString()
        {
            string status = Concluida ? "[X]" : "[ ]";
            return $"{Id} - {status} {Descricao}";
        }
    }
}