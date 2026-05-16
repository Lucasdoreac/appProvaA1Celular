using SQLite;
using appProvaA1Celular.Model;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace appProvaA1Celular.DAL
{
    public class crudSQLite
    {
        private readonly SQLiteAsyncConnection _connection;

        // REGRA DE OURO DO PROFESSOR: .Wait() no CONSTRUTOR assegura que o banco esteja pronto
        // Conforme Apostila 08 pág. 260, CreateTableAsync com .Wait() deve estar DIRETAMENTE no construtor
        public crudSQLite(string dbPath)
        {
            _connection = new SQLiteAsyncConnection(dbPath);
            _connection.CreateTableAsync<Celular>().Wait(); // ✅ OBRIGATÓRIO NO CONSTRUTOR
        }

        // Create - Inserir novo celular
        public async Task<bool> IncluirCelularAsync(Celular celular)
        {
            await _connection.InsertAsync(celular);
            return true;
        }

        // Read - Obter todos os celulares
        public async Task<ObservableCollection<Celular>> GetCelularesAsync()
        {
            var celulares = await _connection.Table<Celular>().ToListAsync();
            return new ObservableCollection<Celular>(celulares);
        }

        // Update - Alterar celular existente
        public async Task<bool> AlterarCelularAsync(Celular celular)
        {
            await _connection.UpdateAsync(celular);
            return true;
        }

        // Delete - Excluir celular
        public async Task<bool> ExcluirCelularAsync(Celular celular)
        {
            await _connection.DeleteAsync(celular);
            return true;
        }

        // GetByID - Obter celular por ID
        public async Task<Celular> GetCelularByIdAsync(int id)
        {
            return await _connection.Table<Celular>().Where(c => c.celID == id).FirstOrDefaultAsync();
        }
    }
}
