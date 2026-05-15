using SQLite;
using appProvaA1Celular.Model;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace appProvaA1Celular.DAL
{
    public class crudSQLite
    {
        private readonly SQLiteAsyncConnection _connection;

        public crudSQLite(string dbPath)
        {
            _connection = new SQLiteAsyncConnection(dbPath);
        }

        private bool _isInitialized = false;

        private async Task InitAsync()
        {
            if (_isInitialized) return;
            await _connection.CreateTableAsync<Celular>();
            _isInitialized = true;
        }

        // Create - Inserir novo celular
        public async Task<bool> IncluirCelularAsync(Celular celular)
        {
            await InitAsync();
            await _connection.InsertAsync(celular);
            return true;
        }

        // Read - Obter todos os celulares
        public async Task<ObservableCollection<Celular>> GetCelularesAsync()
        {
            await InitAsync();
            var celulares = await _connection.Table<Celular>().ToListAsync();
            return new ObservableCollection<Celular>(celulares);
        }

        // Update - Alterar celular existente
        public async Task<bool> AlterarCelularAsync(Celular celular)
        {
            await InitAsync();
            await _connection.UpdateAsync(celular);
            return true;
        }

        // Delete - Excluir celular
        public async Task<bool> ExcluirCelularAsync(Celular celular)
        {
            await InitAsync();
            await _connection.DeleteAsync(celular);
            return true;
        }

        // GetByID - Obter celular por ID
        public async Task<Celular> GetCelularByIdAsync(int id)
        {
            await InitAsync();
            return await _connection.Table<Celular>().Where(c => c.celID == id).FirstOrDefaultAsync();
        }
    }
}
