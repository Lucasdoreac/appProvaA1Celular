using System;
using System.IO;
using SQLite;
using appProvaA1Celular.Model;

namespace appProvaA1Celular.DAL
{
    public class BancoDeDados
    {
        private static crudSQLite _database;
        private static readonly string _dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "celulares.db3");

        public static crudSQLite Database
        {
            get
            {
                if (_database == null)
                {
                    _database = new crudSQLite(_dbPath);
                    // REGRA DE OURO DO PROFESSOR: .Wait() assegura que o banco esteja pronto antes do uso
                    // Conforme Apostilas 08-09, o construtor DAL deve usar .Wait() para garantir inicialização
                    Task.Run(async () => await _database.GetCelularesAsync()).Wait();
                }
                return _database;
            }
        }
    }
}