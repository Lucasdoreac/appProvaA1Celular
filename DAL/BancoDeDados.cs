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
                }
                return _database;
            }
        }
    }
}